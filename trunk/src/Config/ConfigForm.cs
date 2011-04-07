using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Config
{
    public partial class ConfigForm : Form
    {
        string libraryRoot;
        public ConfigForm()
        {
            InitializeComponent();
            checkBox1.Checked = true;
            checkBox7.Checked = true;
            libraryRoot = System.Environment.GetEnvironmentVariable("userprofile") + "\\Music";
            label4.Text = libraryRoot;
            radioButton2.Checked = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            StreamWriter configFile = new StreamWriter("config.ini");
            
            configFile.Write("fileformats=");
            if (checkBox1.Checked) configFile.Write("*.mp3;");
            if (checkBox2.Checked) configFile.Write("*.wma;");
            if (checkBox3.Checked) configFile.Write("*.aac;");
            if (checkBox4.Checked) configFile.Write("*.mp4;");
            if (checkBox5.Checked) configFile.Write("*.flac;");
            if (checkBox6.Checked) configFile.Write("*.ogg;");
            configFile.WriteLine();

            configFile.Write("playlistformats=");
            if (checkBox7.Checked) configFile.Write("*.m3u;");
            if (checkBox8.Checked) configFile.Write("*.wpl;");
            if (checkBox9.Checked) configFile.Write("*.xspf;");
            configFile.WriteLine();

            configFile.WriteLine("libraryroot=" + libraryRoot);
            
            configFile.Write("saveloadplaylists=");
            if (radioButton1.Checked == true)
            {
                configFile.Write("true");
            }
            else
            {
                configFile.Write("false");
            }
            configFile.WriteLine();

            configFile.Write("sortable=");
            if (radioButton3.Checked == true)
            {
                configFile.Write("true");
            }
            else
            {
                configFile.Write("false");
            }
            configFile.WriteLine();

            configFile.Close();
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult dr = folderBrowserDialog1.ShowDialog();
            if (dr == DialogResult.OK)
            {
                libraryRoot = folderBrowserDialog1.SelectedPath;
                label4.Text = libraryRoot;
            }
        }

    }
}
