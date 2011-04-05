using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FractalBlaster.Universe;

namespace FractalBlaster.Plugins.AvailablePluginsView {
    public partial class PluginsView : Form {
        public PluginsView() {
            InitializeComponent();

        }

        public void AddPlugin(IPlugin plugin) {
            PluginNode node = new PluginNode(plugin);
            foreach (String category in node.Categories) {
                foreach (TreeNode n in mPluginTree.Nodes) {
                    foreach (TreeNode pn in n.Nodes) {
                        if (pn.Name.CompareTo(category) == 0) {
                            pn.Nodes.Add(new PluginNode(plugin));
                        }
                    }
                }
            }
        }
    }

    public class PluginNode : TreeNode {
        public PluginNode(IPlugin plugin)  : base(plugin.GetInfo().Name) {
            
            this.Nodes.Add(new TreeNode(String.Format("Description: {0}", plugin.GetInfo().Description)));
            this.Nodes.Add(new TreeNode(String.Format("Author: {0}", plugin.GetInfo().Author)));
            this.Nodes.Add(new TreeNode(String.Format("Version: {0}", plugin.GetInfo().Version)));
            this.Nodes.Add(new TreeNode(String.Format("Assembly: {0}", plugin.GetType().Assembly.FullName)));
            Categories = new List<String>();
            if (plugin is IViewPlugin) {
                Categories.Add("View");
            }
            if (plugin is IOutputPlugin) {
                Categories.Add("Output");
            }
            if (plugin is IInputPlugin) {
                Categories.Add("Input");
            }
            if (plugin is IMetadataPlugin) {
                Categories.Add("Metadata");
            }
            if (plugin is IPlaylistPlugin) {
                Categories.Add("Playlist");
            }
            if (plugin is IEffectPlugin) {
                Categories.Add("Effect");
            }
        }
        public List<String> Categories { get; private set; }
    }
}
