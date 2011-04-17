using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FractalBlaster.Universe;
using FractalBlaster.Core.Runtime;
using System.IO;
using System.Drawing.Drawing2D;

namespace FractalBlaster.Core.UI
{
    public partial class ProductForm : Form
    {
        #region Media Playback Control
        private void PlayMedia(object sender, EventArgs args)
        {
            Playlist pl = PlayingPlaylistControl.Playlist;
            if (pl.SelectedIndex >= pl.Items.Count())
            {
                return;
            }

            MediaFile media = pl.Items.ElementAt(pl.SelectedIndex);

            mCurrentSelectedMediaLabel.Text = String.Format("{0} - {1}",
            media.Metadata["Artist"].Value.ToString(),
            media.Metadata["Title"].Value.ToString());

            Engine.OutputPlugin.Stop();
            if (Engine.IsMediaLoaded)
            {
                if (!Engine.CurrentMedia.Info.FullName.Equals(media.Info.FullName))
                {
                    Engine.Unload();
                    Engine.Load(media);
                }
            }
            else
            {
                Engine.Load(media);
            }
            Engine.OutputPlugin.Play();
        }

        private void PauseMedia(object sender, EventArgs args)
        {
            Engine.OutputPlugin.Pause();
        }

        private void StopMedia(object sender, EventArgs args)
        {
            Engine.OutputPlugin.Stop();
        }

        public void SkipMediaForward(object sender, EventArgs args)
        {
            if (PlayingPlaylistControl != null)
            {
                if (PlayingPlaylistControl.Playlist.SelectedIndex + 1 >= PlayingPlaylistControl.Playlist.Count())
                {
                    return;
                }
                else if (Engine.IsMediaLoaded)
                {
                    PlayingPlaylistControl.Playlist.RequestMediaAt(++PlayingPlaylistControl.Playlist.SelectedIndex);
                }
            }
        }

        private void SkipMediaBackward(object sender, EventArgs args)
        {
            if (PlayingPlaylistControl != null)
            {
                if (PlayingPlaylistControl.Playlist.SelectedIndex == 0)
                {
                    return;
                }
                else if (Engine.IsMediaLoaded)
                {
                    PlayingPlaylistControl.Playlist.RequestMediaAt(--PlayingPlaylistControl.Playlist.SelectedIndex);
                }
            }
        }

        private void mPlayToolBarButton_Click(object sender, EventArgs e)
        {
            PlayingPlaylistControl = CurrentPlaylistControl;
            pc_SongPlayed(PlayingPlaylistControl, PlayingPlaylistControl.SelectedIndex);
        }

        #endregion

        #region Playlist Manipulation

        private void OpenPlaylist(object sender, EventArgs args)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = GetFilterString();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                FileInfo f = new FileInfo(ofd.FileName);
                try
                {
                    IPlaylistPlugin plugin = PlaylistPluginMap[f.Extension];
                    Playlist blarg = plugin.Read(f.FullName);

                    foreach (MediaFile mediaFile in blarg)
                    {
                        CurrentPlaylistControl.Playlist.AddItem(mediaFile);
                    }
                }
                catch (KeyNotFoundException)
                {
                    Console.WriteLine("KeyNotFoundException");
                }
            }
        }

        private void SavePlaylist(object sender, EventArgs args)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = GetFilterString();
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                FileInfo f = new FileInfo(sfd.FileName);
                try
                {
                    IPlaylistPlugin plugin = PlaylistPluginMap[f.Extension];
                    plugin.Write(CurrentPlaylistControl.Playlist, f.FullName);
                }
                catch (KeyNotFoundException e)
                {

                }
            }
        }

        private void addFileToPlaylistToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = true;
            ofd.Filter = "Supported Formats|" + Config.getProperty("fileformats");
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                foreach (string s in ofd.FileNames)
                {
                    CurrentPlaylistControl.Playlist.AddItem(new MediaFile(s));
                }

            }
        }

        void mOpenToolBarDropDown_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = true;
            ofd.Filter = GetAllFilesFilterString();

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                foreach (string filename in ofd.FileNames)
                {
                    FileInfo f = new FileInfo(filename);
                    IPlaylistPlugin playlist_plugin;
                    if (PlaylistPluginMap.TryGetValue(f.Extension, out playlist_plugin))
                    {
                        IPlaylistPlugin plugin = PlaylistPluginMap[f.Extension];
                        Playlist blarg = plugin.Read(f.FullName);

                        foreach (MediaFile mediaFile in blarg)
                        {
                            CurrentPlaylistControl.Playlist.AddItem(mediaFile);
                        }
                    }
                    else
                    {
                        try
                        {
                            MediaFile media = new MediaFile(f.FullName);
                            CurrentPlaylistControl.Playlist.AddItem(media);
                        }
                        catch
                        {
                        }
                    }
                }
            }
        }

        void pc_SongPlayed(PlaylistControl sender, int songIndex)
        {
            PlayingPlaylistControl = sender;
            sender.Playlist.RequestMediaAt(songIndex);
        }

        #endregion

        #region Application Exit
        private void ExitApplication(object sender, EventArgs args)
        {
            foreach (Form f in PluginViews)
            {
                f.Close();
            }
            library.Save();
            Application.ExitThread();
            Application.Exit();
        }

        #endregion

        #region Seek Bar Event Handlers

        private void seekBarRefreshTimer_Tick(object sender, EventArgs e)
        {
            if (Engine.IsMediaLoaded)
            {
                mSeekBar.time = Engine.Timer.currentTime;
                mSeekBar.totalTime = (int)Engine.CurrentMedia.Metadata.Duration.TotalSeconds;
                mSeekBar.Refresh();
            }
        }

        #endregion

        #region Volume Control

        private void VolumeControl_Paint(object sender, PaintEventArgs e)
        {
            int volume = Engine.OutputPlugin.Volume;
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

        private bool volumeMouseDown;

        private void VolumeControl_MouseUp(object sender, MouseEventArgs e)
        {
            volumeMouseDown = false;
        }

        private void VolumeControl_MouseMove(object sender, MouseEventArgs e)
        {
            if (volumeMouseDown)
            {
                changeVolume(e);
            }
        }

        private void VolumeControl_MouseDown(object sender, MouseEventArgs e)
        {
            if ((39 - e.Y) <= e.X * 39 / 100)
            {
                volumeMouseDown = true;
                changeVolume(e);
            }
        }

        private void changeVolume(MouseEventArgs e)
        {
            int volume;
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
            Engine.OutputPlugin.Volume = volume;
            VolumeControl.Refresh();
        }

        #endregion

        #region Playlist Tab Event Handlers

        struct closeButton
        {
            public Rectangle rect;
            public int index;
        }

        closeButton[] tabCloseButtons = new closeButton[0];

        private void mPlaylistTabControl_DrawItem(object sender, DrawItemEventArgs e)
        {
            closeButton[] newArray = new closeButton[mPlaylistTabControl.TabCount];
            for (int i = 0; (i < tabCloseButtons.Length) && (i < newArray.Length); i++)
            {
                newArray[i] = tabCloseButtons[i];
            }
            tabCloseButtons = newArray;
            e.Graphics.DrawString(mPlaylistTabControl.TabPages[e.Index].Text, e.Font, Brushes.Black, e.Bounds.Left + 4, e.Bounds.Top + 4);
            closeButton c;
            Rectangle r = new Rectangle(e.Bounds.Right - 17, e.Bounds.Top + 5, 12, 12);
            c.rect = r;
            c.index = e.Index;
            tabCloseButtons[e.Index] = c;
            e.Graphics.DrawImage(FractalBlaster.Core.Properties.Resources.application_exit_12x12, r);
        }

        private void mPlaylistTabControl_MouseClick(object sender, MouseEventArgs e)
        {
            foreach (closeButton c in tabCloseButtons)
            {
                if (c.rect.Contains(e.Location))
                {
                    if (mPlaylistTabControl.TabPages.Count != 1)
                    {
                        mPlaylistTabControl.TabPages.RemoveAt(c.index);
                    }
                };
            }
        }

        private void mPlaylistTabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            Context.Engine.CurrentPlaylist = (mPlaylistTabControl.SelectedTab.Tag as PlaylistControl).Playlist;
        }

        private void AddNewPlaylistTab(object sender, EventArgs args)
        {
            mPlaylistTabControl.TabPages.Add(CreateNewPlaylistTab());
        }

        #endregion

        #region Product Form Mouse Event Handlers
        private void ProductForm_MouseMove(object sender, MouseEventArgs e)
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

        private void ProductForm_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }

        private void ProductForm_MouseDown(object sender, MouseEventArgs e)
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

        #endregion
    }
}
