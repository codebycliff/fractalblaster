using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FractalBlaster.Plugins.FourierView
{
    public partial class FourierPluginUI : Form
    {
        public FourierPluginUI()
        {
            InitializeComponent();
        }

        Point mouse_offset;

        private void FourierPluginUI_MouseDown(object sender, MouseEventArgs e)
        {
            mouse_offset = new Point(-e.X, -e.Y);
        }

        private void FourierPluginUI_MouseMove(object sender, MouseEventArgs e)
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
