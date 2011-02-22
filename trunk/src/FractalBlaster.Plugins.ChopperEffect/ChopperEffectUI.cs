using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.IO;

namespace FractalBlaster.Plugins.ChopperEffect
{
    public partial class ChopperEffectUI : Form
    {
        ChopperEffectPlugin rec;

        public ChopperEffectUI()
        {
            InitializeComponent();
        }

        public void setReciever(ChopperEffectPlugin r)
        {
            this.rec = r;
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            rec.ChangeEffect(trackBar1.Value);
        }

        Point mouse_offset;

        private void ChopperEffectUI_MouseDown(object sender, MouseEventArgs e)
        {
            mouse_offset = new Point(-e.X, -e.Y);
        }

        private void ChopperEffectUI_MouseMove(object sender, MouseEventArgs e)
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
