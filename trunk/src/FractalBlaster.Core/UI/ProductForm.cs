using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FractalBlaster.Universe;
using FractalBlaster.Core.Runtime;
using System.IO;
using System.Drawing.Drawing2D;

namespace FractalBlaster.Core.UI {
    public partial class ProductForm : Form {

        SeekBar mSeekBar;

        public ProductForm() {
            InitializeComponent();
            Context = FamilyKernel.Instance.Context;
            Engine = Context.Engine;
            PluginViews = new List<Form>();
            PlaylistPluginMap = new Dictionary<String, IPlaylistPlugin>();

            mSeekBar = new SeekBar();
            mSeekBar.Input = Engine.InputPlugin;
            mSeekBar.Output = Engine.OutputPlugin;
            mSeekBar.PlaybackTimer = Engine.Timer;
            SeekBarPanel.Controls.Add(mSeekBar);
            seekBarRefreshTimer.Start();

            foreach (IPlaylistPlugin plugin in FamilyKernel.Instance.Context.Plugins.OfType<IPlaylistPlugin>()) {
                foreach (String f in plugin.SupportedFileExtensions) {
                    PlaylistPluginMap.Add(f, plugin);
                }
            }

            PlaylistControl control = CreatePlaylistControl();
            mPlaylistTabControl.TabPages[0].Tag = control;
            mPlaylistTabControl.TabPages[0].Controls.Add(control);

            SetupCollectionTabs();
            mLibraryCollectionTabPage.MouseDown += new MouseEventHandler(MouseDownOnTreeView);

            foreach (TabPage tp in mPlaylistTabControl.TabPages) {
                tp.AllowDrop = true;
                tp.DragOver += (o, e) => {
                    e.Effect = DragDropEffects.Copy;
                };
            }
            mPlaylistTabControl.DragEnter += (s,e) => {
                if(e.Data.GetData(typeof(IEnumerable<MediaFile>)) != null) {
                    e.Effect = DragDropEffects.Copy;
                }
                else {
                    e.Effect = DragDropEffects.Link;
                }
            };
            mPlaylistTabControl.DragDrop += (s, e) => {
                IEnumerable<MediaFile> items = e.Data.GetData(typeof(IEnumerable<MediaFile>)) as IEnumerable<MediaFile>;
                foreach(MediaFile media in items) {
                    CurrentPlaylistControl.Playlist.AddItem(media);
                }
            };
        }

        private void MouseDownOnTreeView(Object sender, MouseEventArgs args) {
            LibraryCollectionView view = (sender as LibraryCollectionView);
            IEnumerable<MediaFile> items = view.Tree.GetNodeAt(args.X, args.Y).Tag as IEnumerable<MediaFile>;
            if (items != null) {
                //MessageBox.Show(String.Format("Name: {0}\nTag null?: {1}\nCount: {2}", node.Text, node.Tag == null ? "Yes" : "No", items.Count().ToString()));
                DoDragDrop(items, DragDropEffects.Copy);
            }
        }

        int newViewYOffset;

        public void AddViewPlugin(IViewPlugin view) {

            ToolStripMenuItem item = new ToolStripMenuItem(view.GetInfo().Name);
            item.CheckOnClick = true;
            Form form = view.UserInterface;
            PluginViews.Add(form);
            form.Owner = this;
            item.CheckedChanged += (o, e) => {
                ToolStripMenuItem viewItem = o as ToolStripMenuItem;
                if(viewItem.Checked) {
                    form.Show();
                    if (form.Location.IsEmpty)
                    {
                        Point nextLocation = this.Location;
                        nextLocation.Offset(this.Width, newViewYOffset);
                        newViewYOffset += form.Height;
                        form.Location = nextLocation;
                    }
                }
                else {
                    form.Hide();
                }
            };
            form.FormClosed += (o, ea) => {
                item.CheckState = CheckState.Unchecked;
            };
            mViewsMenu.DropDownItems.Add(item);
        }

        public void AddEffectPlugin(IEffectPlugin plugin) {
            ToolStripMenuItem item = new ToolStripMenuItem(plugin.GetInfo().Name);
            item.CheckOnClick = true;
            item.CheckStateChanged += (o, ea) => {
                plugin.Enabled = item.CheckState == CheckState.Checked ? true : false;
            };
            mEffectsMenu.DropDownItems.Add(item);
        }

        public void AddPlaylistPlugin(IPlaylistPlugin plugin) {
            ToolStripMenuItem item = new ToolStripMenuItem(String.Format("Export as {0} ...", plugin.GetInfo().Name));
            item.Click += new EventHandler(SavePlaylist);
            mSaveMenuItem.DropDownItems.Add(item);
        }

        #region  [ Private ]

       private void SetupCollectionTabs() {
            Library lib = Library.Load(new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.MyMusic)));
            LibraryCollectionView libraryView = new LibraryCollectionView(lib);
            libraryView.Dock = DockStyle.Fill;
            mLibraryCollectionTabPage.Controls.Add(libraryView);
            mLibraryCollectionTabPage.Text = libraryView.Label;

            FileSystemCollectionView fsView = new FileSystemCollectionView();
            fsView.Dock = DockStyle.Fill;
            mFileSystemCollectionTabPage.Controls.Add(fsView);
            mFileSystemCollectionTabPage.Text = fsView.Label;
        }

        private void PlayMedia(object sender, EventArgs args) {
            MediaFile media = CurrentPlaylistControl.Playlist.Items.ElementAt(CurrentPlaylistControl.Playlist.SelectedIndex);
            if (Engine.IsMediaLoaded)
            {
                if (!Engine.CurrentMedia.Info.FullName.Equals(media.Info.FullName))
                {
                    Engine.Unload();
                    Engine.Load(media);
                }
                Engine.OutputPlugin.Play();
            }
            else
            {
                Engine.Load(media);
                Engine.OutputPlugin.Play();
            }
        }

        private void PauseMedia(object sender, EventArgs args) {
            Engine.OutputPlugin.Pause();
        }

        private void StopMedia(object sender, EventArgs args) {
            Engine.OutputPlugin.Stop();
        }

        private void SkipMediaForward(object sender, EventArgs args) {
            if (Engine.IsMediaLoaded) {
                CurrentPlaylistControl.Playlist.RequestMediaAt(CurrentPlaylistControl.Playlist.SelectedIndex + 1);
            }
            else {
                CurrentPlaylistControl.Playlist.SelectedIndex++;
            }
        }

        private void SkipMediaBackward(object sender, EventArgs args) {
            if (Engine.IsMediaLoaded) {
                CurrentPlaylistControl.Playlist.RequestMediaAt(CurrentPlaylistControl.Playlist.SelectedIndex - 1);
            }
            else {
                CurrentPlaylistControl.Playlist.SelectedIndex--;
            }
        }

        private void OpenPlaylist(object sender, EventArgs args) {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = GetFilterString();
            if (ofd.ShowDialog() == DialogResult.OK) {
                FileInfo f = new FileInfo(ofd.FileName);
                IPlaylistPlugin plugin = PlaylistPluginMap[f.Extension];
                CurrentPlaylistControl.Playlist = CreatePlaylist(plugin.Read(f.FullName));
            }
        }

        private void SavePlaylist(object sender, EventArgs args) {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = GetFilterString();
            if (sfd.ShowDialog() == DialogResult.OK) {
                FileInfo f = new FileInfo(sfd.FileName);
                IPlaylistPlugin plugin = PlaylistPluginMap[f.Extension];
                plugin.Write(CurrentPlaylistControl.Playlist, f.FullName);
            }
        }

        private void ExitApplication(object sender, EventArgs args) {
            foreach (Form f in PluginViews) {
                f.Close();
            }
            Application.ExitThread();
            Application.Exit();
        }

        private String GetFilterString() {
            if (PlaylistFilterString == null) {
                IEnumerable<String> extensions = PlaylistPluginMap.Keys;
                String extensionFilters = "";
                foreach (String extension in extensions) {
                    extensionFilters += String.Format("*{0};", extension);
                }
                PlaylistFilterString = String.Format("Playlists ({0}) | {0}", extensionFilters.Remove(extensionFilters.Length - 1));
            }
            return PlaylistFilterString;
        }

        private Playlist CreatePlaylist(Playlist playlist = null) {
            if (playlist == null) {
                playlist = new Playlist();
            }
            playlist.SelectedChanged += (o, ea) => {
                if (playlist.SelectedIndex <= playlist.Items.Count() - 1) {
                    MediaFile media = playlist.Items.ElementAt(playlist.SelectedIndex);
                    if (media != null) {
                        mCurrentSelectedMediaLabel.Text = String.Format("{0} - {1}",
                        media.Metadata["Artist"].Value.ToString(),
                        media.Metadata["Title"].Value.ToString());
                    }
                }
            };
            playlist.MediaRequested += m => { PlayMedia(null, new EventArgs()); };
            return playlist;
        }

        private PlaylistControl CreatePlaylistControl(Playlist p = null) {
            if (p == null) {
                p = CreatePlaylist();
            }
            PlaylistControl pc = new PlaylistControl();
            pc.Playlist = p;
            pc.Dock = DockStyle.Fill;
            pc.Playlist.MediaRequested += (m) => {

            };
            return pc;
        }

        private TabPage CreateNewPlaylistTab(Playlist playlist = null) {
            PlaylistControl pc = CreatePlaylistControl(CreatePlaylist(playlist));
            TabPage tab = new TabPage(String.Format("New {0}", mPlaylistTabControl.TabCount));
            tab.Tag = pc;
            tab.Controls.Add(pc);
            return tab;
        }

        private void AddNewPlaylistTab(object sender, EventArgs args) {
            mPlaylistTabControl.TabPages.Add(CreateNewPlaylistTab());
        }

        private AppContext Context { get; set; }
        private IEngine Engine { get; set; }
        private List<Form> PluginViews { get; set; }
        private Dictionary<String, IPlaylistPlugin> PlaylistPluginMap { get; set; }
        private String PlaylistFilterString { get; set; }
        private PlaylistControl CurrentPlaylistControl {
            get {
                return mPlaylistTabControl.SelectedTab.Tag as PlaylistControl;
            }
        }
        
        Point mouse_offset;
        List<Point> window_offset = new List<Point>();
        bool mouseDown = false;

        private void ProductForm_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            mouse_offset = new Point(-e.X, -e.Y);
            window_offset = new List<Point>();
            for (int i = 0; i < OwnedForms.Length; i++)
            {
                Point subwindowLocation = OwnedForms.ElementAt(i).Location;
                window_offset.Insert(i, new Point(subwindowLocation.X - this.Location.X, subwindowLocation.Y - this.Location.Y));
            }
        }

        private void ProductForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                Point mousePos = Control.MousePosition;
                mousePos.Offset(mouse_offset);
                for (int i = 0; i < OwnedForms.Length; i++)
                {
                    if (!OwnedForms.ElementAt(i).Location.IsEmpty)
                    {
                        Point subwindowPos = Control.MousePosition;
                        subwindowPos.Offset(mouse_offset);
                        subwindowPos.Offset(window_offset.ElementAt(i));
                        OwnedForms.ElementAt(i).Location = subwindowPos;
                    }
                }
                this.Location = mousePos;
            }
        }

        private void ProductForm_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }

        #endregion

        private void openFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = true;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                Playlist p = new Playlist();
                foreach (string s in ofd.FileNames)
                {
                    p.AddItem(new MediaFile(s));
                }
                CurrentPlaylistControl.Playlist = p;
            }
        }

        private void addFileToPlaylistToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = true;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                foreach (string s in ofd.FileNames)
                {
                    CurrentPlaylistControl.Playlist.AddItem(new MediaFile(s));
                }
                
            }
        }

        private void seekBarRefreshTimer_Tick(object sender, EventArgs e)
        {
            if (CurrentPlaylistControl.Playlist.SelectedIndex < CurrentPlaylistControl.Playlist.Count())
            {
                mSeekBar.time = Engine.Timer.currentTime;
                mSeekBar.totalTime = (int)CurrentPlaylistControl.Playlist.Items.ElementAt(CurrentPlaylistControl.Playlist.SelectedIndex).Metadata.Duration.TotalSeconds;
                mSeekBar.Refresh();
            }
        }

        private void VolumeControl_Paint(object sender, PaintEventArgs e)
        {
            int volume = Engine.OutputPlugin.Volume;
            GraphicsPath outline = new GraphicsPath();
            outline.AddLine(0, 39, 100, 0);
            outline.AddLine(100, 0, 100, 39);
            outline.AddLine(100, 39, 0, 39);
            e.Graphics.DrawPath(Pens.Black, outline);

            GraphicsPath fill = new GraphicsPath();
            
            fill.AddLine(0, 39, volume, 39);
            fill.AddLine(volume, 39, volume, 39 - 39 * volume / 100);
            fill.AddLine(volume, 39 - 39 * volume / 100, 0, 39);
            e.Graphics.FillPath(Brushes.Green, fill);
        }

        private bool volumeMouseDown;

        private void VolumeControl_MouseUp(object sender, MouseEventArgs e)
        {
            volumeMouseDown = false;
        }

        private void VolumeControl_MouseMove(object sender, MouseEventArgs e)
        {
            if (volumeMouseDown)
            {
                changeVolume(e);
            }
        }

        private void VolumeControl_MouseDown(object sender, MouseEventArgs e)
        {
            if ((39 - e.Y) <= e.X * 39 / 100)
            {
                volumeMouseDown = true;
                changeVolume(e);
            }
        }

        private void changeVolume(MouseEventArgs e)
        {
            int volume;
            if ((e.X >= 0) && (e.X <= 100))
            {
                volume = e.X;
            }
            else if (e.X < 0)
            {
                volume = 0;
            }
            else
            {
                volume = 100;
            }
            Engine.OutputPlugin.Volume = volume;
            VolumeControl.Refresh();
        }

    }
}
