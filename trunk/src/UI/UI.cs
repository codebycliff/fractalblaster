using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Engine;
using System.Threading;

namespace UI
{
    public partial class UI : Form
    {
        private Point mouse_offset;
        List<Point> window_offset;
        List<Form> subwindows;
        PlaylistForm playlistForm;

        public UI()
        {
            subwindows = new List<Form>();
            InitializeComponent();
        }

        public void setPlaylistForm(PlaylistForm p)
        {
            playlistForm = p;
        }

        public void addSubwindow(Form f)
        {
            subwindows.Add(f);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {         
                string[] filenames = openFileDialog1.FileNames;
                Common.AudioMetadata am;
                string displayInfo;

                for (int i = 0; i < filenames.Length; i++)
                {
                    am = Metadata.RetrieveMetadata(filenames[i]);
                    if (am.Artist.Equals("") && am.Title.Equals(""))
                    {
                        displayInfo = openFileDialog1.FileName;
                    }
                    else if (am.Artist.Equals("") && !am.Title.Equals(""))
                    {
                        displayInfo = am.Title;
                    }
                    else
                    {
                        displayInfo = am.Artist + " - " + am.Title;
                    }

                    playlistForm.addFile(filenames[i], displayInfo);
                }                
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
            for (int i = 0; i < subwindows.Count; i++)
            {
                subwindows.ElementAt(i).Show(this);
            }
            //Common.SettingsLoader.LoadIcons(ref btnPlay, ref btnStop, ref btnPause, ref btnBack, ref btnForward);
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            if (PlaybackStateMachine.isPlaying() || PlaybackStateMachine.SongPlaying() != playlistForm.getFilename())
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
        
        public void PlaylistDoubleClicked()
        {
            if (PlaybackStateMachine.isPlaying() || PlaybackStateMachine.SongPlaying() != playlistForm.getFilename())
            {
                PlaybackStateMachine.Open(playlistForm.getFilename());
            }
            PlaybackStateMachine.Play();
        }

        private void btnForward_Click(object sender, EventArgs e)
        {
            if (PlaybackStateMachine.isPlaying())
            {
                PlaybackStateMachine.Stop();
                playlistForm.gotoNext();
                PlaybackStateMachine.Open(playlistForm.getFilename());
                PlaybackStateMachine.Play();
            }
            else
            {
                playlistForm.gotoNext();
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            if (PlaybackStateMachine.isPlaying())
            {
                PlaybackStateMachine.Stop();
                playlistForm.gotoPrevious();
                PlaybackStateMachine.Open(playlistForm.getFilename());
                PlaybackStateMachine.Play();
            }
            else
            {
                playlistForm.gotoPrevious();
            }
        }
    }
}
