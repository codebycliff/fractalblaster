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
        private Point mouse_offset;

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

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            mouse_offset = new Point(-e.X, -e.Y);
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                Point mousePos = Control.MousePosition;
                mousePos.Offset(mouse_offset.X, mouse_offset.Y);
                this.Location = mousePos; //move the form to the desired location
            }
        }
    }
}
