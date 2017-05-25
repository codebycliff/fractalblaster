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
                if (f.Name != "Config.exe")
                    File.Copy(f.FullName, destinationDirectory.FullName + "\\" + f.Name, true);
            }

            sourceDirectory = new DirectoryInfo("bin\\Plugins");
            destinationDirectory = Directory.CreateDirectory("Fractal Blasters\\Plugins");

            string[] commonPlugins = {
                "AudioOut",
                "Decoder",
                "M3UPlaylist",
                "TagLib",
                "WPLPlaylist",
                "XSPFPlaylist"
            };

            foreach (string s in commonPlugins)
            {
                DirectoryInfo source = new DirectoryInfo(sourceDirectory.FullName + "\\" + s);
                DirectoryInfo dest = new DirectoryInfo(destinationDirectory.FullName + "\\" + s);
                CopyDirectory(source, dest);

            }

            foreach (int i in checkedListBox1.CheckedIndices)
            {
                string s = mPluginPathList[i];
                DirectoryInfo source = new DirectoryInfo(sourceDirectory.FullName + "\\" + s);
                DirectoryInfo dest = new DirectoryInfo(destinationDirectory.FullName + "\\" + s);
                CopyDirectory(source, dest);
            }

            Close();
        }

        private static void CopyDirectory(DirectoryInfo source, DirectoryInfo target)
        {
            if (!Directory.Exists(target.FullName))
            {
                Directory.CreateDirectory(target.FullName);
            }

            foreach (FileInfo fileInfo in source.GetFiles())
            {
                fileInfo.CopyTo(Path.Combine(target.ToString(),fileInfo.Name),true);
            }

            foreach (DirectoryInfo dir in source.GetDirectories())
            {
                DirectoryInfo subdir = target.CreateSubdirectory(dir.Name);
                CopyDirectory(dir, subdir);
            }
        }
    }
}
