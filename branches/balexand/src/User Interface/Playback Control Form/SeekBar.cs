using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using FractalBlaster.Universe;
using System.Timers;

namespace FractalBlaster.PlaybackControlForm
{
    public partial class SeekBar : UserControl
    {
        int mTime;
        int mTotalTime;

        public SeekBar()
        {
            mTime = 0;
            InitializeComponent();
        }

        public int time
        {
            set
            {
                mTime = value;
            }
        }

        public int totalTime
        {
            set { mTotalTime = value; }
        }

        private void SeekBar_Paint(object sender, PaintEventArgs e)
        {
            GraphicsPath seekBorder = new GraphicsPath();
            Rectangle seekBarRect = new Rectangle(1, 1, 300, 20);
            seekBorder.AddEllipse(seekBarRect);

            if (mTotalTime != 0)
            {
                Region outsideRegion = new Region(this.Bounds);
                outsideRegion.Exclude(seekBorder);
                Region seekCircle = new Region(new Rectangle((mTime * 300 / mTotalTime), 1, 20, 20));
                seekCircle.Exclude(outsideRegion);
                e.Graphics.FillRegion(Brushes.DarkRed, seekCircle);
            }
            
            e.Graphics.DrawPath(Pens.Black, seekBorder);
            label1.Text = String.Format("{0:d2}:{1:d2}/{2:d2}:{3:d2}",
                mTime / 60, mTime % 60, mTotalTime / 60, mTotalTime % 60);
        }
    }
}
