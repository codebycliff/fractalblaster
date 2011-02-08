using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Engine;
using FractalBlaster;
using System.Threading;

namespace UI
{
    public partial class UI : Form
    {
        public Engine.Engine prog;
        private Point mouse_offset;
        List<Point> window_offset;
        PlaylistForm playlistForm;

        public UI()
        {
            InitializeComponent();
            prog = new Engine.Engine();
            PlaybackStateMachine.engine = prog;
        }

        public void setPlaylistForm(PlaylistForm p)
        {
            playlistForm = p;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                prog.openFile(openFileDialog1.FileName);
                AudioMetadata am = prog.getMetadata();
                String displayTitle;
                if (am.Artist.Equals("") && am.Title.Equals(""))
                {
                    displayTitle = openFileDialog1.FileName;
                }
                else if (am.Artist.Equals("") && !am.Title.Equals(""))
                {
                    displayTitle = am.Title;
                }
                else
                {
                   displayTitle = am.Artist + " - " + am.Title;
                }
                playlistForm.addFile(openFileDialog1.FileName, displayTitle);
            }
        }

      
        private void button6_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            mouse_offset = new Point(-e.X, -e.Y);
            window_offset = new List<Point>();
            for (int i = 0; i < OwnedForms.Length; i++)
            {
                Point subwindowLocation = OwnedForms.ElementAt(i).Location;
                window_offset.Insert(i, new Point(subwindowLocation.X - this.Location.X, subwindowLocation.Y - this.Location.Y));
            }
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                Point mousePos = Control.MousePosition;
                mousePos.Offset(mouse_offset);
                for (int i = 0; i < OwnedForms.Length; i++)
                {
                    Point subwindowPos = Control.MousePosition;
                    subwindowPos.Offset(mouse_offset);
                    subwindowPos.Offset(window_offset.ElementAt(i));
                    OwnedForms.ElementAt(i).Location = subwindowPos;
                }
                this.Location = mousePos;
            }
        }

        private void Form1_Close(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void UI_Load(object sender, EventArgs e)
        {
            playlistForm.SetDesktopLocation(this.Location.X + this.Width, this.Location.Y);
            playlistForm.Show(this);
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            if (PlaybackStateMachine.isPlaying())
            {
                PlaybackStateMachine.Open(playlistForm.getFilename());
            }
            PlaybackStateMachine.Play();
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            PlaybackStateMachine.Pause();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            PlaybackStateMachine.Stop();
        }
        

    }
}
