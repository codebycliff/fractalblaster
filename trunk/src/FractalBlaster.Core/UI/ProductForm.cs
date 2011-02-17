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

namespace FractalBlaster.Core.UI {
    public partial class ProductForm : Form {

        public ProductForm() {
            InitializeComponent();

            PluginViews = new List<Form>();
            PlaylistPluginMap = new Dictionary<String, IPlaylistPlugin>();
            foreach (IPlaylistPlugin plugin in FamilyKernel.Instance.Context.Plugins.OfType<IPlaylistPlugin>()) {
                foreach (String f in plugin.SupportedFileExtensions) {
                    PlaylistPluginMap.Add(f, plugin);
                }
            }

            PlaylistControl control = CreatePlaylistControl(new Playlist());
            mPlaylistTabControl.TabPages[0].Tag = control;
            mPlaylistTabControl.TabPages[0].Controls.Add(control);
                       
        }

        public void AddViewPlugin(IViewPlugin view) {
            
            ToolStripMenuItem item = new ToolStripMenuItem(view.GetInfo().Name);
            item.CheckOnClick = true;
            Form form = view.UserInterface;
            PluginViews.Add(form);
            item.CheckedChanged += (o, e) => {
                ToolStripMenuItem viewItem = o as ToolStripMenuItem;
                if(viewItem.Checked) {
                    form.Show();
                }
                else {
                    form.Hide();
                }
            };
            mViewsMenu.DropDownItems.Add(item);
        }
        
        #region  [ Private ]

        private void PlaySelectedSong(object sender, EventArgs args) {
            MediaFile media = CurrentPlaylistControl.Playlist.Items.ElementAt(CurrentPlaylistControl.Playlist.SelectedIndex);
            FamilyKernel.Instance.Context.Engine.Load(media);
            FamilyKernel.Instance.Context.Engine.OutputPlugin.Play();
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
                    extensionFilters += String.Format("*{0} ", extension);
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
            playlist.MediaRequested += m => {
                FamilyKernel.Instance.Context.Engine.Load(m);
                FamilyKernel.Instance.Context.Engine.OutputPlugin.Play();
            };
            return playlist;
        }

        private PlaylistControl CreatePlaylistControl(Playlist p) {
            PlaylistControl pc = new PlaylistControl();
            pc.Playlist = p;
            pc.Dock = DockStyle.Fill;
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

        private List<Form> PluginViews { get; set; }
        private Dictionary<String, IPlaylistPlugin> PlaylistPluginMap { get; set; }
        private String PlaylistFilterString { get; set; }
        private PlaylistControl CurrentPlaylistControl {
            get {
                return mPlaylistTabControl.SelectedTab.Tag as PlaylistControl;
            }
        }
        
        #endregion
    }
}
