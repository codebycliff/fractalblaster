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

namespace FractalBlaster.Core.UI {

    /// <remarks>
    /// This partial class contains event handlers and other private 
    /// members for the <see cref="ProductForm"/> class.
    /// </remarks>
    public partial class ProductForm : Form {
        
        #region [ Media Playback Control Event Handlers ]

        /// <summary>
        /// Event handler that plays a media file.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void PlayMedia(object sender, EventArgs args) {
            Playlist pl = PlayingPlaylistControl.Playlist;
            if (pl.SelectedIndex >= pl.Items.Count()) {
                return;
            }

            MediaFile media = pl.Items.ElementAt(pl.SelectedIndex);

            mCurrentSelectedMediaLabel.Text = String.Format("{0} - {1}",
            media.Metadata["Artist"].Value.ToString(),
            media.Metadata["Title"].Value.ToString());

            Engine.OutputPlugin.Stop();
            if (Engine.IsMediaLoaded) {
                if (!Engine.CurrentMedia.Info.FullName.Equals(media.Info.FullName)) {
                    Engine.Unload();
                    Engine.Load(media);
                }
            }
            else {
                Engine.Load(media);
            }
            Engine.OutputPlugin.Play();
        }

        /// <summary>
        /// Pauses the media.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void PauseMedia(object sender, EventArgs args) {
            Engine.OutputPlugin.Pause();
        }

        /// <summary>
        /// Stops the media.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void StopMedia(object sender, EventArgs args) {
            Engine.OutputPlugin.Stop();
        }

        /// <summary>
        /// Skips the media forward.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        public void SkipMediaForward(object sender, EventArgs args) {
            if (PlayingPlaylistControl != null) {
                if (PlayingPlaylistControl.Playlist.SelectedIndex + 1 >= PlayingPlaylistControl.Playlist.Count()) {
                    return;
                }
                else if (Engine.IsMediaLoaded) {
                    PlayingPlaylistControl.Playlist.RequestMediaAt(++PlayingPlaylistControl.Playlist.SelectedIndex);
                }
            }
        }

        /// <summary>
        /// Skips the media backward.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void SkipMediaBackward(object sender, EventArgs args) {
            if (PlayingPlaylistControl != null) {
                if (PlayingPlaylistControl.Playlist.SelectedIndex == 0) {
                    return;
                }
                else if (Engine.IsMediaLoaded) {
                    PlayingPlaylistControl.Playlist.RequestMediaAt(--PlayingPlaylistControl.Playlist.SelectedIndex);
                }
            }
        }

        /// <summary>
        /// Handles the Click event of the mPlayToolBarButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void mPlayToolBarButton_Click(object sender, EventArgs e) {
            PlayingPlaylistControl = CurrentPlaylistControl;
            pc_SongPlayed(PlayingPlaylistControl, PlayingPlaylistControl.SelectedIndex);
        }

        #endregion

        #region [ Playlist Manipulation Event Handlers ]

        /// <summary>
        /// Opens the playlist.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void OpenPlaylist(object sender, EventArgs args) {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = GetFilterString();
            if (ofd.ShowDialog() == DialogResult.OK) {
                FileInfo f = new FileInfo(ofd.FileName);
                try {
                    IPlaylistPlugin plugin = PlaylistPluginMap[f.Extension];
                    Playlist blarg = plugin.Read(f.FullName);

                    foreach (MediaFile mediaFile in blarg) {
                        CurrentPlaylistControl.Playlist.AddItem(mediaFile);
                    }
                }
                catch (KeyNotFoundException) {
                    Console.WriteLine("KeyNotFoundException");
                }
            }
        }

        /// <summary>
        /// Saves the playlist.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void SavePlaylist(object sender, EventArgs args) {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = GetFilterString();
            if (sfd.ShowDialog() == DialogResult.OK) {
                FileInfo f = new FileInfo(sfd.FileName);
                try {
                    IPlaylistPlugin plugin = PlaylistPluginMap[f.Extension];
                    plugin.Write(CurrentPlaylistControl.Playlist, f.FullName);
                }
                catch (KeyNotFoundException e) {

                }
            }
        }

        /// <summary>
        /// Handles the Click event of the addFileToPlaylistToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void addFileToPlaylistToolStripMenuItem_Click(object sender, EventArgs e) {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = true;
            ofd.Filter = "Supported Formats|" + Config.getProperty("fileformats");
            if (ofd.ShowDialog() == DialogResult.OK) {
                foreach (string s in ofd.FileNames) {
                    CurrentPlaylistControl.Playlist.AddItem(new MediaFile(s));
                }

            }
        }

        /// <summary>
        /// Handles the Click event of the mOpenToolBarDropDown control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        void mOpenToolBarDropDown_Click(object sender, EventArgs e) {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = true;
            ofd.Filter = GetAllFilesFilterString();

            if (ofd.ShowDialog() == DialogResult.OK) {
                foreach (string filename in ofd.FileNames) {
                    FileInfo f = new FileInfo(filename);
                    IPlaylistPlugin playlist_plugin;
                    if (PlaylistPluginMap.TryGetValue(f.Extension, out playlist_plugin)) {
                        IPlaylistPlugin plugin = PlaylistPluginMap[f.Extension];
                        Playlist blarg = plugin.Read(f.FullName);

                        foreach (MediaFile mediaFile in blarg) {
                            CurrentPlaylistControl.Playlist.AddItem(mediaFile);
                        }
                    }
                    else {
                        try {
                            MediaFile media = new MediaFile(f.FullName);
                            CurrentPlaylistControl.Playlist.AddItem(media);
                        }
                        catch {
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Handles the song played event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="songIndex">Index of the song.</param>
        void pc_SongPlayed(PlaylistControl sender, int songIndex) {
            PlayingPlaylistControl = sender;
            sender.Playlist.RequestMediaAt(songIndex);
        }

        #endregion

        #region [ Application Exit Event Handler ]

        /// <summary>
        /// Exits the application.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void ExitApplication(object sender, EventArgs args) {
            foreach (Form f in PluginViews) {
                f.Close();
            }
            Library.Save();
            Application.ExitThread();
            Application.Exit();
        }

        #endregion

        #region [ Seek Bar Event Handlers ]

        /// <summary>
        /// Handles the Tick event of the seekBarRefreshTimer control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void seekBarRefreshTimer_Tick(object sender, EventArgs e) {
            if (Engine.IsMediaLoaded) {
                SeekBar.Time = Engine.Timer.CurrentTime;
                SeekBar.TotalTime = (int)Engine.CurrentMedia.Metadata.Duration.TotalSeconds;
                SeekBar.Refresh();
            }
        }

        #endregion

        #region [ Volume Control Event Handlers ]

        /// <summary>
        /// Handles the Paint event of the VolumeControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.PaintEventArgs"/> instance containing the event data.</param>
        private void VolumeControl_Paint(object sender, PaintEventArgs e) {
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

        /// <summary>
        /// Handles the MouseUp event of the VolumeControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
        private void VolumeControl_MouseUp(object sender, MouseEventArgs e) {
            IsVolumeMouseDown = false;
        }

        /// <summary>
        /// Handles the MouseMove event of the VolumeControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
        private void VolumeControl_MouseMove(object sender, MouseEventArgs e) {
            if (IsVolumeMouseDown) {
                changeVolume(e);
            }
        }

        /// <summary>
        /// Handles the MouseDown event of the VolumeControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
        private void VolumeControl_MouseDown(object sender, MouseEventArgs e) {
            if ((39 - e.Y) <= e.X * 39 / 100) {
                IsVolumeMouseDown = true;
                changeVolume(e);
            }
        }

        /// <summary>
        /// Changes the volume.
        /// </summary>
        /// <param name="e">The <see cref="System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
        private void changeVolume(MouseEventArgs e) {
            int volume;
            if ((e.X >= 0) && (e.X <= 100)) {
                volume = e.X;
            }
            else if (e.X < 0) {
                volume = 0;
            }
            else {
                volume = 100;
            }
            Engine.OutputPlugin.Volume = volume;
            VolumeControl.Refresh();
        }

        #endregion

        #region [ Playlist Tab Event Handlers ]

        /// <summary>
        /// Struct representing a close button on a tab.
        /// </summary>
        private struct CloseButton {

            /// <summary>
            /// The rectangle occupied by the close button.
            /// </summary>
            public Rectangle rect;

            /// <summary>
            /// The index of the tab for this close button?
            /// </summary>
            public int index;

        }

        /// <summary>
        /// Private member array of tab close buttons.
        /// </summary>
        CloseButton[] TabCloseButtons = new CloseButton[0];

        /// <summary>
        /// Handles the DrawItem event of the mPlaylistTabControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.DrawItemEventArgs"/> instance containing the event data.</param>
        private void mPlaylistTabControl_DrawItem(object sender, DrawItemEventArgs e) {
            CloseButton[] newArray = new CloseButton[mPlaylistTabControl.TabCount];
            for (int i = 0; (i < TabCloseButtons.Length) && (i < newArray.Length); i++) {
                newArray[i] = TabCloseButtons[i];
            }
            TabCloseButtons = newArray;
            e.Graphics.DrawString(mPlaylistTabControl.TabPages[e.Index].Text, e.Font, Brushes.Black, e.Bounds.Left + 4, e.Bounds.Top + 4);
            CloseButton c;
            Rectangle r = new Rectangle(e.Bounds.Right - 17, e.Bounds.Top + 5, 12, 12);
            c.rect = r;
            c.index = e.Index;
            TabCloseButtons[e.Index] = c;
            e.Graphics.DrawImage(FractalBlaster.Core.Properties.Resources.application_exit_12x12, r);
        }

        /// <summary>
        /// Handles the MouseClick event of the mPlaylistTabControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
        private void mPlaylistTabControl_MouseClick(object sender, MouseEventArgs e) {
            foreach (CloseButton c in TabCloseButtons) {
                if (c.rect.Contains(e.Location)) {
                    if (mPlaylistTabControl.TabPages.Count != 1) {
                        mPlaylistTabControl.TabPages.RemoveAt(c.index);
                    }
                };
            }
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the mPlaylistTabControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void mPlaylistTabControl_SelectedIndexChanged(object sender, EventArgs e) {
            Context.Engine.CurrentPlaylist = (mPlaylistTabControl.SelectedTab.Tag as PlaylistControl).Playlist;
        }

        /// <summary>
        /// Adds the new playlist tab.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void AddNewPlaylistTab(object sender, EventArgs args) {
            mPlaylistTabControl.TabPages.Add(CreateNewPlaylistTab());
        }

        #endregion

        #region [ Product Form Mouse Event Handlers ]

        /// <summary>
        /// Handles the MouseMove event of the ProductForm control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
        private void ProductForm_MouseMove(object sender, MouseEventArgs e) {
            if (IsMouseDown) {
                Point mousePos = Control.MousePosition;
                mousePos.Offset(MouseOffset);
                for (int i = 0; i < OwnedForms.Length; i++) {
                    if (!OwnedForms.ElementAt(i).Location.IsEmpty) {
                        Point subwindowPos = Control.MousePosition;
                        subwindowPos.Offset(MouseOffset);
                        subwindowPos.Offset(WindowOffset.ElementAt(i));
                        OwnedForms.ElementAt(i).Location = subwindowPos;
                    }
                }
                this.Location = mousePos;
            }
        }

        /// <summary>
        /// Handles the MouseUp event of the ProductForm control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
        private void ProductForm_MouseUp(object sender, MouseEventArgs e) {
            IsMouseDown = false;
        }

        /// <summary>
        /// Handles the MouseDown event of the ProductForm control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
        private void ProductForm_MouseDown(object sender, MouseEventArgs e) {
            IsMouseDown = true;
            MouseOffset = new Point(-e.X, -e.Y);
            WindowOffset = new List<Point>();
            for (int i = 0; i < OwnedForms.Length; i++) {
                Point subwindowLocation = OwnedForms.ElementAt(i).Location;
                WindowOffset.Insert(i, new Point(subwindowLocation.X - this.Location.X, subwindowLocation.Y - this.Location.Y));
            }
        }

        #endregion
    
    }
}
