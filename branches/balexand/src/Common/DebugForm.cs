using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FractalBlaster.Universe
{
    public partial class DebugForm : Form
    {
        public DebugForm()
        {
            InitializeComponent();
        }

        public void print(string s)
        {
            textBox1.Text += s;
            textBox1.ScrollToCaret();
            textBox1.Refresh();
        }

        public void printline(string s)
        {
            textBox1.Text += s + "\r\n";
            textBox1.Select(textBox1.Text.Length - 1, 0);
            textBox1.ScrollToCaret();
            textBox1.Refresh();
        }
    }
}
