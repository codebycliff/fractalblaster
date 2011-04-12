using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FractalBlaster.Universe;
using System.IO;
using System.Diagnostics;

namespace Generator
{
    public partial class GeneratorForm : Form
    {
        List<IPlugin> mPluginList;
        List<string> mPluginPathList;

        public GeneratorForm()
        {
            InitializeComponent();
            mPluginList = new List<IPlugin>();
            mPluginPathList = new List<string>();

            foreach (IPlugin p in PluginManager.GetInterfaces(typeof(IEffectPlugin)))
            {
                if (!mPluginList.Contains(p))
                {
                    mPluginList.Add((IEffectPlugin)p);
                    mPluginPathList.Add(PluginManager.GetPathsForPlugin(p));
                    checkedListBox1.Items.Add(p.GetInfo().Name);
                }
            }
            foreach (IPlugin p in PluginManager.GetInterfaces(typeof(IViewPlugin)))
            {
                if (!mPluginList.Contains(p))
                {
                    mPluginList.Add((IViewPlugin)p);
                    mPluginPathList.Add(PluginManager.GetPathsForPlugin(p));
                    checkedListBox1.Items.Add(p.GetInfo().Name);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DirectoryInfo sourceDirectory = new DirectoryInfo("bin");
            DirectoryInfo destinationDirectory = Directory.CreateDirectory("Fractal Blasters");

            foreach (FileInfo f in sourceDirectory.GetFiles())
            {
                File.Copy(f.FullName, destinationDirectory.FullName + "\\" + f.Name, true);
            }

            sourceDirectory = new DirectoryInfo("bin\\Plugins");
            destinationDirectory = Directory.CreateDirectory("Fractal Blasters\\Plugins");

            string[] commonDLLs = {
                "avcodec.dll",
                "avformat.dll",
                "avutil.dll",
                "FractalBlaster.Plugins.AudioOut.dll",
                "FractalBlaster.Plugins.Decoder.dll",
                "FractalBlaster.Plugins.M3UPlaylist.dll",
                "FractalBlaster.Plugins.Taglib.dll",
                "FractalBlaster.Plugins.WPLPlaylist.dll",
                "FractalBlaster.Plugins.XSPFPlaylist.dll",
                "FractalBlaster.Plugins.XSPFPlaylist.xml",
                "FractalBlaster.Universe.dll",
                "taglib-sharp.dll"
            };

            foreach (string s in commonDLLs)
            {
                File.Copy(sourceDirectory.FullName + "\\" + s, destinationDirectory.FullName + "\\" + s, true);
            }

            foreach (int i in checkedListBox1.CheckedIndices)
            {
                string s = mPluginPathList[i];
                File.Copy(sourceDirectory.FullName + "\\" + s, destinationDirectory.FullName + "\\" + s, true);
            }

            Close();
        }
    }
}
