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
            Rectangle seekBarRect = new Rectangle(1, 5, 300, 10);
            seekBorder.AddRectangle(seekBarRect);
            Region outsideBorder = new Region(this.Bounds);
            outsideBorder.Exclude(seekBorder);
            e.Graphics.DrawPath(Pens.Black, seekBorder);

            if (mTotalTime != 0)
            {
                Rectangle seekRectangle = new Rectangle((mTime * 300 / mTotalTime) - 6, 3, 14, 14);
                GraphicsPath seekCircle = new GraphicsPath();
                seekCircle.AddEllipse(seekRectangle);
                Region fillCircle = new Region(seekCircle);
                //fillCircle.Exclude(outsideBorder);

                e.Graphics.FillRegion(Brushes.DarkRed, fillCircle);
            }
            
            
            label1.Text = String.Format("{0:d2}:{1:d2}/{2:d2}:{3:d2}",
                mTime / 60, mTime % 60, mTotalTime / 60, mTotalTime % 60);
        }
    }
}
