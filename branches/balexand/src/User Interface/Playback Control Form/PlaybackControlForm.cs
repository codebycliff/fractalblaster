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
        int volume;

        public PlaybackControlForm()
        {
            Debug.printline("PlaybackControlForm()");
            volume = 100;
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


        private void openFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog myOpenFileDialog = new OpenFileDialog();

            if (myOpenFileDialog.ShowDialog() == DialogResult.OK)
            {
                label2.Text = myOpenFileDialog.FileName;
                label2.Refresh();
                mPlaybackControl.Open(myOpenFileDialog.FileName);
            }

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

        private void VolumeControl_Paint(object sender, PaintEventArgs e)
        {
            GraphicsPath outline = new GraphicsPath();
            outline.AddLine(0, 39, 100, 0);
            outline.AddLine(100, 0, 100, 39);
            outline.AddLine(100, 39, 0, 39);
            e.Graphics.DrawPath(Pens.Black, outline);

            GraphicsPath fill = new GraphicsPath();
            fill.AddLine(0, 39, volume, 39);
            fill.AddLine(volume, 39, volume, 39 - 39 * volume / 100);
            fill.AddLine(volume, 39 - 39 * volume / 100, 0, 39);
            e.Graphics.FillPath(Brushes.Green, fill);
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

        private bool volumeMouseDown;

        private void VolumeControl_MouseDown(object sender, MouseEventArgs e)
        {
            if ((39 - e.Y) <= e.X * 39 / 100)
            {
                volumeMouseDown = true;
                changeVolume(e);
            }
        }

        private void VolumeControl_MouseMove(object sender, MouseEventArgs e)
        {
            if (volumeMouseDown)
            {
                changeVolume(e);
            }
        }

        private void VolumeControl_MouseUp(object sender, MouseEventArgs e)
        {
            volumeMouseDown = false;
        }

        private void changeVolume(MouseEventArgs e)
        {
            if ((e.X >= 0) && (e.X <= 100))
            {
                volume = e.X;
            }
            else if (e.X < 0)
            {
                volume = 0;
            }
            else
            {
                volume = 100;
            }
            mPlaybackControl.volume = volume;
            VolumeControl.Refresh();
        }

        private void debugToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            if (debugToolStripMenuItem.Checked)
            {
                Debug.show();
            }
            else
            {
                Debug.hide();
            }
        }

        private void debugToolStripMenuItem_Paint(object sender, PaintEventArgs e)
        {
            debugToolStripMenuItem.Checked = Debug.Visible;
        }

    }
}
