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

namespace FractalBlaster.Core.UI
{
    public partial class SeekBar : UserControl
    {
        int mTime;
        int mTotalTime;
        IInputPlugin mInput;
        IOutputPlugin mOutput;
        IPlaybackTimer mPlaybackTimer;

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

        public IInputPlugin Input
        {
            set { mInput = value; }
        }

        public IOutputPlugin Output
        {
            set { mOutput = value; }
        }

        public IPlaybackTimer PlaybackTimer
        {
            set { mPlaybackTimer = value; }
        }


        private void SeekBar_Paint(object sender, PaintEventArgs e)
        {
            GraphicsPath seekBorder = new GraphicsPath();
            Rectangle seekBarRect = new Rectangle(1, 5, 200, 10);
            seekBorder.AddRectangle(seekBarRect);
            Region outsideBorder = new Region(this.Bounds);
            outsideBorder.Exclude(seekBorder);
            e.Graphics.DrawPath(Pens.Black, seekBorder);

            if (mTotalTime != 0)
            {
                Rectangle seekRectangle = new Rectangle((mTime * 200 / mTotalTime) - 6, 3, 14, 14);
                e.Graphics.FillEllipse(Brushes.DarkRed, seekRectangle);

                if (mouseDown)
                {
                    Rectangle newSeekRect = new Rectangle(mouseX - 6, 3, 14, 14);
                    e.Graphics.FillEllipse(Brushes.IndianRed, newSeekRect);
                }
            }
            
            
            label1.Text = String.Format("{0:d2}:{1:d2}/{2:d2}:{3:d2}",
                mTime / 60, mTime % 60, mTotalTime / 60, mTotalTime % 60);
        }

        bool mouseDown = false;
        int mouseX;

        private void SeekBar_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            mouseX = e.X;
        }

        private void SeekBar_MouseUp(object sender, MouseEventArgs e)
        {
            if (mouseDown == true)
            {
                mouseDown = false;
                Debug.printline("seek(" + (e.X * mTotalTime / 200).ToString() + ")");
                mOutput.Stop();
                mInput.Seek(e.X * mTotalTime / 200);
                mOutput.Play();
                mPlaybackTimer.currentTime = e.X * mTotalTime / 200;
            }
        }

        private void SeekBar_MouseMove(object sender, MouseEventArgs e)
        {
            mouseX = e.X;
        }
    }
}
