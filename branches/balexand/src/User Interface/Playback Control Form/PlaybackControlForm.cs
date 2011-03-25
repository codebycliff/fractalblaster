using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FractalBlaster.Universe;
using System.Drawing.Drawing2D;

namespace FractalBlaster.PlaybackControlForm
{
    public partial class PlaybackControlForm : Form , IPlaybackControlForm
    {
        IPlaybackControl mPlaybackControl;

        public PlaybackControlForm()
        {
            Debug.printline("PlaybackControlForm()");
            InitializeComponent();            
        }

        public Form form
        {
            get { return this; }
        }

        public IPlaybackControl playbackControl
        {
            set { mPlaybackControl = value; }
        }

        private void stopButton_Click(object sender, EventArgs e)
        {
            mPlaybackControl.Stop();
        }

        private void previousButton_Click(object sender, EventArgs e)
        {
            mPlaybackControl.Previous();
        }

        private void playButton_Click(object sender, EventArgs e)
        {
            mPlaybackControl.Play();
        }

        private void nextButton_Click(object sender, EventArgs e)
        {
            mPlaybackControl.Next();
        }

        private void shuffleButton_Click(object sender, EventArgs e)
        {

        }

        private void repeatButton_Click(object sender, EventArgs e)
        {

        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void stopButton_Paint(object sender, PaintEventArgs e)
        {
            GraphicsPath buttonPath = new GraphicsPath();
            Rectangle buttonRect = stopButton.ClientRectangle;
            buttonRect.Inflate(-1, -1);
            e.Graphics.DrawEllipse(Pens.Black, buttonRect);
            buttonRect.Inflate(1, 1);
            buttonPath.AddEllipse(buttonRect);

            stopButton.Region = new System.Drawing.Region(buttonPath);
        }


        private void previousButton_Paint(object sender, PaintEventArgs e)
        {
            GraphicsPath buttonPath = new GraphicsPath();
            Rectangle buttonRect = previousButton.ClientRectangle;
            buttonRect.Inflate(-1, -1);
            e.Graphics.DrawEllipse(Pens.Black, buttonRect);
            buttonRect.Inflate(1, 1);
            buttonPath.AddEllipse(buttonRect);

            previousButton.Region = new System.Drawing.Region(buttonPath);
        }

        private void playButton_Paint(object sender, PaintEventArgs e)
        {
            GraphicsPath buttonPath = new GraphicsPath();
            Rectangle buttonRect = playButton.ClientRectangle;
            buttonRect.Inflate(-1, -1);
            e.Graphics.DrawEllipse(Pens.Black, buttonRect);
            buttonRect.Inflate(1, 1);
            buttonPath.AddEllipse(buttonRect);

            playButton.Region = new System.Drawing.Region(buttonPath);
        }

        private void nextButton_Paint(object sender, PaintEventArgs e)
        {
            GraphicsPath buttonPath = new GraphicsPath();
            Rectangle buttonRect = nextButton.ClientRectangle;
            buttonRect.Inflate(-1, -1);
            e.Graphics.DrawEllipse(Pens.Black, buttonRect);
            buttonRect.Inflate(1, 1);
            buttonPath.AddEllipse(buttonRect);

            nextButton.Region = new System.Drawing.Region(buttonPath);
        }

        private void shuffleButton_Paint(object sender, PaintEventArgs e)
        {
            GraphicsPath buttonPath = new GraphicsPath();
            Rectangle buttonRect = shuffleButton.ClientRectangle;
            buttonRect.Inflate(-1, -1);
            e.Graphics.DrawEllipse(Pens.Black, buttonRect);
            buttonRect.Inflate(1, 1);
            buttonPath.AddEllipse(buttonRect);

            shuffleButton.Region = new System.Drawing.Region(buttonPath);
        }

        private void repeatButton_Paint(object sender, PaintEventArgs e)
        {
            GraphicsPath buttonPath = new GraphicsPath();
            Rectangle buttonRect = repeatButton.ClientRectangle;
            buttonRect.Inflate(-1, -1);
            e.Graphics.DrawEllipse(Pens.Black, buttonRect);
            buttonRect.Inflate(1, 1);
            buttonPath.AddEllipse(buttonRect);

            repeatButton.Region = new System.Drawing.Region(buttonPath);
        }

        bool mouseDown;
        Point mouse_offset;
        List<Point> window_offset;

        private void PlaybackControlForm_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            mouse_offset = new Point(-e.X, -e.Y);
            window_offset = new List<Point>();
            for (int i = 0; i < OwnedForms.Length; i++)
            {
                Point subwindowLocation = OwnedForms.ElementAt(i).Location;
                window_offset.Insert(i, new Point(subwindowLocation.X - this.Location.X, subwindowLocation.Y - this.Location.Y));
            }
        }

        private void PlaybackControlForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                Point mousePos = Control.MousePosition;
                mousePos.Offset(mouse_offset);
                for (int i = 0; i < OwnedForms.Length; i++)
                {
                    if (!OwnedForms.ElementAt(i).Location.IsEmpty)
                    {
                        Point subwindowPos = Control.MousePosition;
                        subwindowPos.Offset(mouse_offset);
                        subwindowPos.Offset(window_offset.ElementAt(i));
                        OwnedForms.ElementAt(i).Location = subwindowPos;
                    }
                }
                this.Location = mousePos;
            }
        }

        private void PlaybackControlForm_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }

    }
}
