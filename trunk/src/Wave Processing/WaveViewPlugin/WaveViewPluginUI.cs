using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using FractalBlaster.Universe;

namespace FractalBlaster.Plugins.WaveView
{
    public partial class WaveViewPluginUI : Form
    {
        public WaveViewPluginUI()
        {
            InitializeComponent();
        }
        
        Point mouse_offset;

        private void WaveViewPluginUI_MouseDown(object sender, MouseEventArgs e)
        {
            mouse_offset = new Point(-e.X, -e.Y);
        }

        private void WaveViewPluginUI_MouseMove(object sender, MouseEventArgs e)
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
