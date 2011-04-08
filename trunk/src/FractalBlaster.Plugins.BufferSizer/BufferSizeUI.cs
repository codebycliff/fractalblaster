using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FractalBlaster.Plugins.BufferSizer
{
    public partial class BufferSizeUI : Form
    {
        BufferSizePlugin bsp;
        Point mouse_offset;


        public BufferSizeUI()
        {
            InitializeComponent();
        }

        public void setReciever(BufferSizePlugin r)
        {
            this.bsp = r;
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            bsp.ChangeEffect(trackBar1.Value);
        }

        private void BufferSizeUI_MouseDown(object sender, MouseEventArgs e)
        {
            mouse_offset = new Point(-e.X, -e.Y);
        }

        private void BufferSizeUI_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                Point mousePos = Control.MousePosition;
                mousePos.Offset(mouse_offset);
                this.Location = mousePos;
            }
        }
    }
}
