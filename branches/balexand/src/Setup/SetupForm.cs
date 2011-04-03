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
        public SetupForm()
        {
            InitializeComponent();
            checkBox1.Checked = true;
            checkBox6.Checked = true;
            radioButton1.Checked = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            StreamWriter configFile = new StreamWriter("config.ini");
            configFile.Write("fileformats=Supported Files|");
            if (checkBox1.Checked) configFile.Write("*.wav;");
            if (checkBox2.Checked) configFile.Write("*.ogg;");
            if (checkBox4.Checked) configFile.Write("*.mpc;");
            if (checkBox3.Checked) configFile.Write("*.flac;");
            if (checkBox8.Checked) configFile.Write("*.TTA;");
            if (checkBox7.Checked) configFile.Write("*.aiff;");
            if (checkBox6.Checked) configFile.Write("*.raw;");
            if (checkBox5.Checked) configFile.Write("*.au;");
            if (checkBox16.Checked) configFile.Write("*.gsm;");
            if (checkBox15.Checked) configFile.Write("*.dct;");
            if (checkBox14.Checked) configFile.Write("*.vox;");
            if (checkBox13.Checked) configFile.Write("*.mmf;");
            if (checkBox12.Checked) configFile.Write("*.mp3;");
            if (checkBox11.Checked) configFile.Write("*.aac;");
            if (checkBox10.Checked) configFile.Write("*.mp4;");
            if (checkBox9.Checked) configFile.Write("*.wma;");
            if (checkBox32.Checked) configFile.Write("*.artac;");
            if (checkBox31.Checked) configFile.Write("*.ra;");
            if (checkBox30.Checked) configFile.Write("*.rm;");
            if (checkBox29.Checked) configFile.Write("*.dss;");
            if (checkBox28.Checked) configFile.Write("*.msv;");
            if (checkBox27.Checked) configFile.Write("*.dvf;");
            if (checkBox26.Checked) configFile.Write("*.m4p;");
            if (checkBox25.Checked) configFile.Write("*.3gp;");
            if (checkBox24.Checked) configFile.Write("*.amr;");
            if (checkBox23.Checked) configFile.Write("*.awb;");
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
            configFile.Close();
            Close();
        }
    }
}
