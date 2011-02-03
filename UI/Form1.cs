using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Engine;

namespace UI
{
    public partial class Form1 : Form
    {
        Engine.Engine prog;

        public Form1()
        {
            InitializeComponent();
            prog = new Engine.Engine();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                prog.openFile(openFileDialog1.FileName);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            prog.playFile();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
            Engine.Engine.Pause();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Engine.Engine.UnPause();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Engine.Engine.Stop();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
