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
            Views = new List<Form>();
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
            mViewMenu.DropDownItems.Add(item);
        }

        private void ProductForm_FormClosing(object sender, FormClosingEventArgs e) {
            foreach (Form f in Views) {
                f.Close();
            }
        }

        private List<Form> Views { get; set; }

        private void ProductForm_FormClosed(object sender, FormClosedEventArgs e) {
            Application.Exit();
        }

        private void openPlaylistToolStripMenuItem_Click(object sender, EventArgs e) {
            Dictionary<String, IPlaylistPlugin> pluginMap = new Dictionary<String,IPlaylistPlugin>();
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
                PlaylistForm pform = new PlaylistForm(playlist);
                pform.Show();
            }
            
        }
    }
}
