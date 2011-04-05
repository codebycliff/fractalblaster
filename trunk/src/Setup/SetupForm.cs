using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Setup
{
    public partial class SetupForm : Form
    {
        string libraryRoot;
        public SetupForm()
        {
            InitializeComponent();
            checkBox1.Checked = true;
            radioButton1.Checked = true;
            libraryRoot = System.Environment.GetEnvironmentVariable("userprofile") + "\\Music";
            label4.Text = libraryRoot;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            StreamWriter configFile = new StreamWriter("config.ini");
            configFile.Write("fileformats=Supported Files|");
            if (checkBox1.Checked) configFile.Write("*.mp3;");
            if (checkBox2.Checked) configFile.Write("*.wma;");
            if (checkBox3.Checked) configFile.Write("*.aac;");
            if (checkBox4.Checked) configFile.Write("*.mp4;");
            if (checkBox5.Checked) configFile.Write("*.flac;");
            if (checkBox6.Checked) configFile.Write("*.ogg;");
            configFile.WriteLine();
            configFile.Write("multipleplaylists=");
            if (radioButton1.Checked)
            {
                configFile.Write("true");
            }
            else
            {
                configFile.Write("false");
            }
            configFile.WriteLine();
            configFile.WriteLine("libraryroot=" + libraryRoot);
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
