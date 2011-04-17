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

namespace FractalBlaster.Core.UI
{
    public partial class ProductForm : Form
    {
        SeekBar mSeekBar;

        public ProductForm()
        {
            InitializeComponent();
            Context = FamilyKernel.Instance.Context;
            Engine = Context.Engine;
            PluginViews = new List<Form>();
            PlaylistPluginMap = new Dictionary<String, IPlaylistPlugin>();

            //Create new SeekBar
            mSeekBar = new SeekBar();
            mSeekBar.Input = Engine.InputPlugin;
            mSeekBar.Output = Engine.OutputPlugin;
            mSeekBar.PlaybackTimer = Engine.Timer;
            mSeekBar.UI = this;
            SeekBarPanel.Controls.Add(mSeekBar);
            seekBarRefreshTimer.Start();

            //Get references to plugins
            foreach (IPlaylistPlugin plugin in FamilyKernel.Instance.Context.Plugins.OfType<IPlaylistPlugin>())
            {
                foreach (String f in plugin.SupportedFileExtensions)
                {
                    if (Config.getProperty("playlistformats").Contains(f))
                    {
                        PlaylistPluginMap.Add(f, plugin);
                    }
                }
            }

            //Create first playlist
            PlaylistControl control = CreatePlaylistControl();
            mPlaylistTabControl.TabPages[0].Tag = control;
            mPlaylistTabControl.TabPages[0].Controls.Add(control);

            //Setup playlist tabs
            SetupCollectionTabs();
            foreach (TabPage tp in mPlaylistTabControl.TabPages)
            {
                tp.AllowDrop = true;
                tp.DragOver += (o, e) =>
                {
                    e.Effect = DragDropEffects.Copy;
                };
            }

            //Disable buttons as specified by config file.
            if (Config.getProperty("saveloadplaylists") == "false" ||
                Config.getProperty("playlistformats") == "")
            {
                mSaveMenuItem.Enabled = false;
                mSaveToolBarButton.Enabled = false;
            }

            //Setup engine playlist.
            Context.Engine.CurrentPlaylist = (mPlaylistTabControl.SelectedTab.Tag as PlaylistControl).Playlist;

            //Hook up some events
            mSaveToolBarButton.Click += SavePlaylist;
            mNewPlaylistMenuItem.Click += AddNewPlaylistTab;
            mPlayToolBarButton.Click += mPlayToolBarButton_Click;
        }

        int newViewYOffset;

        public void AddViewPlugin(IViewPlugin view)
        {
            ToolStripMenuItem item = new ToolStripMenuItem(view.GetInfo().Name);
            item.CheckOnClick = true;
            Form form = view.UserInterface;
            PluginViews.Add(form);
            form.Owner = this;
            form.FormClosing += (o, e) =>
                {
                    item.Checked = false;
                };
            item.CheckedChanged += (o, e) =>
            {
                ToolStripMenuItem viewItem = o as ToolStripMenuItem;
                if (viewItem.Checked)
                {
                    form.Show();
                    if (form.Location.IsEmpty)
                    {
                        Point nextLocation = this.Location;
                        nextLocation.Offset(this.Width, newViewYOffset);
                        newViewYOffset += form.Height;
                        form.Location = nextLocation;
                    }
                }
                else
                {
                    form.Hide();
                }
            };
            form.FormClosed += (o, ea) =>
            {
                item.CheckState = CheckState.Unchecked;
            };
            mViewsMenu.DropDownItems.Add(item);
        }

        public void AddEffectPlugin(IEffectPlugin plugin)
        {
            ToolStripMenuItem item = new ToolStripMenuItem(plugin.GetInfo().Name);
            item.CheckOnClick = true;
            Form form = plugin.UserInterface;

            form.FormClosing += (o, ea) =>
                {
                    item.Checked = false;
                };

            item.CheckStateChanged += (o, ea) =>
            {
                plugin.Enabled = item.CheckState == CheckState.Checked ? true : false;
                ToolStripMenuItem viewItem = o as ToolStripMenuItem;
                if (viewItem.Checked)
                {
                    form.Show();
                    if (form.Location.IsEmpty)
                    {
                        Point nextLocation = this.Location;
                        nextLocation.Offset(this.Width, newViewYOffset);
                        newViewYOffset += form.Height;
                        form.Location = nextLocation;
                    }
                }
                else
                {
                    newViewYOffset -= form.Height;
                    form.Hide();
                }
            };
            mEffectsMenu.DropDownItems.Add(item);
        }

        public void AddPlaylistPlugin(IPlaylistPlugin plugin)
        {
            ToolStripMenuItem item = new ToolStripMenuItem(String.Format("Export as {0} ...", plugin.GetInfo().Name));
            item.Click += new EventHandler(SavePlaylist);
            mSaveMenuItem.DropDownItems.Add(item);
        }

        #region  [ Private ]

        private Library library;
        private void SetupCollectionTabs()
        {
            Library lib = Library.Load(new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.MyMusic)));
            library = lib;
            LibraryCollectionView libraryView = new LibraryCollectionView(lib);
            libraryView.Dock = DockStyle.Fill;
            mLibraryCollectionTabPage.Controls.Add(libraryView);
            mLibraryCollectionTabPage.Text = libraryView.Label;

            FileSystemCollectionView fsView = new FileSystemCollectionView();
            fsView.Dock = DockStyle.Fill;
            mFileSystemCollectionTabPage.Controls.Add(fsView);
            mFileSystemCollectionTabPage.Text = fsView.Label;
        }

        private String GetFilterString()
        {
            if (PlaylistFilterString == null)
            {
                IEnumerable<String> extensions = PlaylistPluginMap.Keys;
                String extensionFilters = "";
                foreach (String extension in extensions)
                {
                    extensionFilters += String.Format("*{0};", extension);
                }
                PlaylistFilterString = String.Format("Playlists ({0}) | {0}", extensionFilters.Remove(extensionFilters.Length - 1));
            }
            return PlaylistFilterString;
        }

        private String GetAllFilesFilterString()
        {
            List<string> file_extensions = Config.getProperty("fileformats").Split(';').ToList<string>();
            IEnumerable<string> pl_extensions = Config.getProperty("playlistformats").Split(';').ToList<string>();

            file_extensions.AddRange(pl_extensions);
            string filters = "";
            foreach (string extension in file_extensions)
            {
                filters += String.Format("{0};", extension);
            }
            return String.Format("Audio Files ({0}) | {0}", filters);
        }

        private Playlist CreatePlaylist(Playlist playlist = null)
        {
            if (playlist == null)
            {
                playlist = new Playlist();
            }

            playlist.MediaRequested += m => { PlayMedia(null, new EventArgs()); };
            return playlist;
        }

        private PlaylistControl CreatePlaylistControl(Playlist p = null)
        {
            if (p == null)
            {
                p = CreatePlaylist();
            }
            PlaylistControl pc = new PlaylistControl();
            pc.Playlist = p;
            pc.Dock = DockStyle.Fill;
            pc.SongPlayed += new PlaylistControlSongPlayedEventHandler(pc_SongPlayed);
            return pc;
        }

        private TabPage CreateNewPlaylistTab(Playlist playlist = null)
        {
            PlaylistControl pc = CreatePlaylistControl(CreatePlaylist(playlist));
            TabPage tab = new TabPage(String.Format("New {0}", mPlaylistTabControl.TabCount));
            tab.Tag = pc;
            tab.Controls.Add(pc);
            return tab;
        }

        private AppContext Context { get; set; }
        private IEngine Engine { get; set; }
        private List<Form> PluginViews { get; set; }
        private Dictionary<String, IPlaylistPlugin> PlaylistPluginMap { get; set; }
        private String PlaylistFilterString { get; set; }

        private PlaylistControl CurrentPlaylistControl
        {
            get
            {
                return mPlaylistTabControl.SelectedTab.Tag as PlaylistControl;
            }
        }
        private PlaylistControl PlayingPlaylistControl
        {
            get;
            set;
        }

        Point mouse_offset;
        List<Point> window_offset = new List<Point>();
        bool mouseDown = false;

        #endregion



    }
}
