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

        public TabPage QueueTab { get; private set; }
        public List<PlaylistControl> PlaylistViews { get; private set; }



        public ProductForm() {
            InitializeComponent();
            Views = new List<Form>();
            QueueTab = mPlaylistTabControl.TabPages[0];
            PlaylistViews = new List<PlaylistControl>();
            PlaylistControl pc = new PlaylistControl();
            pc.Dock = DockStyle.Fill;
            QueueTab.Controls.Add(pc);
            PlaylistViews.Add(pc);
        }

        public void AddViewPlugin(IViewPlugin view) {
            
            ToolStripMenuItem item = new ToolStripMenuItem(view.GetInfo().Name);
            item.CheckOnClick = true;
            Form form = view.UserInterface;
            Views.Add(form);
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

        private void ProductForm_FormClosing(object sender, FormClosingEventArgs e) {
            foreach (Form f in Views) {
                f.Close();
            }
        }

        private void ProductForm_FormClosed(object sender, FormClosedEventArgs e) {
            Application.Exit();
        }

        private void openPlaylistToolStripMenuItem_Click(object sender, EventArgs e) {
            OpenPlaylist();
        }

        private List<Form> Views { get; set; }

        private void toolStripButton6_Click(object sender, EventArgs e) {
            ExitApplication();
        }

        private void mFileMenuExitItem_Click(object sender, EventArgs e) {
            ExitApplication();
        }

        private void OpenPlaylist() {
            Dictionary<String, IPlaylistPlugin> pluginMap = new Dictionary<String, IPlaylistPlugin>();
            String filter = "";
            foreach (IPlaylistPlugin plugin in FamilyKernel.Instance.Context.Plugins.OfType<IPlaylistPlugin>()) {
                foreach (String f in plugin.SupportedFileExtensions) {
                    pluginMap.Add(f, plugin);
                    filter += String.Format("Playlists (*{0})|*{0}", f);
                }
            }
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = filter;
            DialogResult result = ofd.ShowDialog();
            if (result == DialogResult.OK) {
                FileInfo f = new FileInfo(ofd.FileName);
                IPlaylistPlugin plugin = pluginMap[f.Extension];
                Playlist playlist = plugin.Read(f.FullName);
                PlaylistViews.First().Playlist = playlist;
            }
        }
        private void ExitApplication() {
            this.Close();
            Application.Exit();
        }

        private void openPlaylistToolStripMenuItem1_Click(object sender, EventArgs e) {
            OpenPlaylist();
        }

        private void toolStripButton1_Click(object sender, EventArgs e) {
            OpenPlaylist();
        }

        private void PlaySelectedSong() {
            MediaFile media = PlaylistViews.First().SelectedMediaFile;
            IEngine engine = FamilyKernel.Instance.Context.Engine;
            engine.Load(media);
            engine.OutputPlugin.Play();
        }

        private void toolStripButton8_Click(object sender, EventArgs e) {

        }
    }
}
