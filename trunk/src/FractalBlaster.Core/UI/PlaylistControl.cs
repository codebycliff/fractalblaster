using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FractalBlaster.Universe;

namespace FractalBlaster.Core.UI {

    /// <summary>
    /// Delegate for handling events associated with the playing of a song.
    /// </summary>
    /// <param name="sender">The sender, in other words, the <see cref="PlaylistControl"/> instance.</param>
    /// <param name="songIndex">Index of the song played.</param>
    public delegate void PlaylistControlSongPlayedEventHandler(PlaylistControl sender, int songIndex);

    /// <remarks>
    /// Class providing a user control for viewing and managing a CurrentPlaylist.
    /// </remarks>
    public partial class PlaylistControl : UserControl {
    
        /// <summary>
        /// Initializes a new instance of the <see cref="PlaylistControl"/> class.
        /// </summary>
        public PlaylistControl() {
            InitializeComponent();
            PlaylistData = new DataTable();

            AllowDrop = true;

            mPlaylistGridView.DragOver += (o, e) => {
                e.Effect = DragDropEffects.Copy;
            };
            mPlaylistGridView.DragEnter += new DragEventHandler(DragEnterHandler);
            mPlaylistGridView.DragDrop += new DragEventHandler(DragDropHandler);

            bool sortable = Config.getProperty("sortable") == "false" ? false : true;
            if (!sortable) {
                foreach (DataGridViewColumn dgvc in mPlaylistGridView.Columns) {
                    dgvc.SortMode = DataGridViewColumnSortMode.NotSortable;
                }
            }

            //Handle sorting in a custom manner to maintain synchronization of display and CurrentPlaylist....
            mPlaylistGridView.Sorted += new EventHandler(mPlaylistGridView_Sorted);
            mPlaylistGridView.KeyDown += new KeyEventHandler(mPlaylistGridView_KeyDown);
            mPlaylistGridView.SortCompare += new DataGridViewSortCompareEventHandler(mPlaylistGridView_SortCompare);
            mPlaylistGridView.UserDeletingRow += new DataGridViewRowCancelEventHandler(mPlaylistGridView_UserDeletingRow);

            //Event handlers for dragging items around in the CurrentPlaylist
            mPlaylistGridView.CellMouseDown += new DataGridViewCellMouseEventHandler(mPlaylistGridView_CellMouseDown);
            mPlaylistGridView.CellMouseUp += new DataGridViewCellMouseEventHandler(mPlaylistGridView_CellMouseUp);
            mPlaylistGridView.CellMouseEnter += new DataGridViewCellEventHandler(mPlaylistGridView_CellMouseEnter);
            mPlaylistGridView.MouseMove += new MouseEventHandler(mPlaylistGridView_MouseMove);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PlaylistControl"/> class
        /// with the specified CurrentPlaylist.
        /// </summary>
        /// <param name="CurrentPlaylist">The CurrentPlaylist to be initialized with.</param>
        public PlaylistControl(Playlist playlist) : this() {
            Playlist = playlist;
        }

        #region [ Properties ]

        /// <summary>
        /// Occurs when [song played].
        /// </summary>
        public event PlaylistControlSongPlayedEventHandler SongPlayed;

        /// <summary>
        /// Gets the index in the CurrentPlaylist of the selected song.
        /// </summary>
        /// <value>
        /// The index of the selected song.
        /// </value>
        public Int32 SelectedIndex {
            get { return mPlaylistGridView.SelectedRows[0].Index; }
        }

        /// <summary>
        /// Gets or sets the CurrentPlaylist for the control.
        /// </summary>
        /// <value>
        /// The CurrentPlaylist.
        /// </value>
        public Playlist Playlist {
            get { return CurrentPlaylist; }
            set {
                CurrentPlaylist = value;
                CurrentPlaylist.MediaAdded += new MediaChangeHandler(AddMediaToPlaylistData);
                CurrentPlaylist.SelectedChanged += (o, ea) => {
                    if (ExternalSelectionChange) {
                        int index = Playlist.SelectedIndex;
                        DataGridViewRow row = mPlaylistGridView.Rows[index];
                        row.Selected = true;
                    }
                };
                foreach (MediaFile mf in Playlist) {
                    AddMediaToPlaylistData(mf);
                }

                CurrentPlaylist.MediaRequested += new MediaChangeHandler(playlist_MediaRequested);
            }
        }

        /// <summary>
        /// Gets the currently selected media file.
        /// </summary>
        public MediaFile SelectedMediaFile {
            get {
                return mPlaylistGridView.Rows[Playlist.SelectedIndex].Tag as MediaFile;
            }
        }

        #endregion
        
        #region [ Private Handlers ]

        /// <summary>
        /// Event handler for a media file being requested.
        /// </summary>
        /// <param name="file">The media file being requested.</param>
        private void playlist_MediaRequested(MediaFile file) {
            mPlaylistGridView.Rows[IndexPlaying].DefaultCellStyle.BackColor = Color.White;
            IndexPlaying = CurrentPlaylist.SelectedIndex;
            mPlaylistGridView.Rows[CurrentPlaylist.SelectedIndex].DefaultCellStyle.BackColor = Color.LightGray;
        }

        /// <summary>
        /// Adds the media to CurrentPlaylist data.
        /// </summary>
        /// <param name="media">The media.</param>
        private void AddMediaToPlaylistData(MediaFile media) {
            if (media != null) {
                mPlaylistGridView.Rows.Add(new String[] {
                media.Metadata.Artist,
                media.Metadata.Title,
                media.Metadata.Duration.ToString()
            });
                mPlaylistGridView.Rows[mPlaylistGridView.Rows.Count - 1].Tag = media;
            }
        }

        /// <summary>
        /// Handles the KeyDown event of the mPlaylistGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void mPlaylistGridView_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter) {
                if (SongPlayed != null)
                    SongPlayed(this, mPlaylistGridView.CurrentRow.Index);
                e.Handled = true;
            }
        }

        /// <summary>
        /// Handles the UserDeletingRow event of the mPlaylistGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.DataGridViewRowCancelEventArgs"/> instance containing the event data.</param>
        private void mPlaylistGridView_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e) {
            IsRowDragging = false;
            CurrentPlaylist.RemoveMedia(e.Row.Index);
        }

        /// <summary>
        /// Handles the SelectionChanged event of the mPlaylistGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void mPlaylistGridView_SelectionChanged(object sender, EventArgs e) {
            if (mPlaylistGridView.SelectedRows.Count > 0) {
                DataGridViewRow row = mPlaylistGridView.SelectedRows[0];
                if (row.Index <= Playlist.Items.Count()) {
                    //Playlist.SelectedIndex = row.Index;
                    ExternalSelectionChange = false;
                }
            }
        }

        #region [ Mouse Handlers ]

        /// <summary>
        /// Handles the MouseMove event of the mPlaylistGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
        private void mPlaylistGridView_MouseMove(object sender, MouseEventArgs e) {
            if (IsRowDragging) {
                double timeSinceScroll = DateTime.Now.Subtract(TimeScrolled).TotalSeconds;
                if (timeSinceScroll > 0.05) {
                    if (mPlaylistGridView.Rows[MovingRow.Index + 1].Displayed == false) {
                        TimeScrolled = DateTime.Now;
                        mPlaylistGridView.FirstDisplayedScrollingRowIndex++;
                    }

                    if (MovingRow.Index > 0 && mPlaylistGridView.Rows[MovingRow.Index - 1].Displayed == false) {
                        TimeScrolled = DateTime.Now;
                        mPlaylistGridView.FirstDisplayedScrollingRowIndex--;
                    }
                }
            }
        }

        /// <summary>
        /// Handles the CellMouseEnter event of the mPlaylistGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void mPlaylistGridView_CellMouseEnter(object sender, DataGridViewCellEventArgs e) {
            if (IsRowDragging && e.RowIndex >= 0 && e.RowIndex < mPlaylistGridView.RowCount - 1) {
                CurrentPlaylist.MoveItem(MovingRow.Index, e.RowIndex);

                mPlaylistGridView.Rows.RemoveAt(MovingRow.Index);
                mPlaylistGridView.Rows.Insert(e.RowIndex, MovingRow);

                MovingRow.Selected = true;
            }
        }

        /// <summary>
        /// Handles the CellMouseDown event of the mPlaylistGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.DataGridViewCellMouseEventArgs"/> instance containing the event data.</param>
        private void mPlaylistGridView_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e) {
            if (e.RowIndex >= 0 && e.RowIndex < mPlaylistGridView.RowCount - 1 && e.ColumnIndex >= 0 && e.ColumnIndex < mPlaylistGridView.ColumnCount) {
                MovingRow = mPlaylistGridView.Rows[e.RowIndex];
                IsRowDragging = true;
            }
        }

        /// <summary>
        /// Handles the CellMouseUp event of the mPlaylistGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.DataGridViewCellMouseEventArgs"/> instance containing the event data.</param>
        private void mPlaylistGridView_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e) {
            MovingRow = null;
            IsRowDragging = false;
        }

        /// <summary>
        /// Handles the CellMouseDoubleClick event of the mPlaylistGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.DataGridViewCellMouseEventArgs"/> instance containing the event data.</param>
        private void mPlaylistGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e) {
            if (SongPlayed != null)
                SongPlayed(this, e.RowIndex);
        }

        #endregion
        
        #region [ D&D Handlers ]

        /// <summary>
        /// Event handler for drag enter for dragging media files to CurrentPlaylist.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="System.Windows.Forms.DragEventArgs"/> instance containing the event data.</param>
        private void DragEnterHandler(Object sender, DragEventArgs args) {
            Object data = args.Data.GetData(typeof(Playlist));
            if (data != null) {
                args.Effect = DragDropEffects.Copy;
            }
            else {
                args.Effect = DragDropEffects.None;
            }
        }

        /// <summary>
        /// Event handler for drag drop for dropping media files to CurrentPlaylist.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.DragEventArgs"/> instance containing the event data.</param>
        private void DragDropHandler(Object sender, DragEventArgs e) {
            Playlist playlist = e.Data.GetData(typeof(Playlist)) as Playlist;
            if (playlist != null)
                foreach (MediaFile media in playlist) {
                    Playlist.AddItem(media);
                }
        }
        
        #endregion

        #region [ Sorting Handlers ]

        /// <summary>
        /// Handles the SortCompare event of the mPlaylistGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.DataGridViewSortCompareEventArgs"/> instance containing the event data.</param>
        private void mPlaylistGridView_SortCompare(object sender, DataGridViewSortCompareEventArgs e) {
            switch (e.Column.Name) {
            case ("Artist"):
                e.SortResult = e.CellValue1.ToString().CompareTo(e.CellValue2.ToString());
                if (e.SortResult == 0) {
                    e.SortResult = System.String.Compare(
                        mPlaylistGridView.Rows[e.RowIndex1].Cells["Title"].Value.ToString(),
                        mPlaylistGridView.Rows[e.RowIndex2].Cells["Title"].Value.ToString());

                }
                if (e.SortResult == 0) {
                    e.SortResult = System.TimeSpan.Compare(
                        TimeSpan.Parse(mPlaylistGridView.Rows[e.RowIndex1].Cells["Length"].Value.ToString()),
                        TimeSpan.Parse(mPlaylistGridView.Rows[e.RowIndex2].Cells["Length"].Value.ToString()));
                }
                break;
            case ("Title"):
                e.SortResult = e.CellValue1.ToString().CompareTo(e.CellValue2.ToString());

                if (e.SortResult == 0) {
                    e.SortResult = System.String.Compare(
                        mPlaylistGridView.Rows[e.RowIndex1].Cells["Artist"].Value.ToString(),
                        mPlaylistGridView.Rows[e.RowIndex2].Cells["Artist"].Value.ToString());
                }

                if (e.SortResult == 0) {
                    e.SortResult = System.TimeSpan.Compare(
                        TimeSpan.Parse(mPlaylistGridView.Rows[e.RowIndex1].Cells["Length"].Value.ToString()),
                        TimeSpan.Parse(mPlaylistGridView.Rows[e.RowIndex2].Cells["Length"].Value.ToString()));
                }
                break;
            case ("Length"):

                e.SortResult = System.TimeSpan.Compare(
                    TimeSpan.Parse(mPlaylistGridView.Rows[e.RowIndex1].Cells["Length"].Value.ToString()),
                    TimeSpan.Parse(mPlaylistGridView.Rows[e.RowIndex2].Cells["Length"].Value.ToString()));


                if (e.SortResult == 0) {
                    e.SortResult = System.String.Compare(
                        mPlaylistGridView.Rows[e.RowIndex1].Cells["Artist"].Value.ToString(),
                        mPlaylistGridView.Rows[e.RowIndex2].Cells["Artist"].Value.ToString());
                }

                if (e.SortResult == 0) {
                    e.SortResult = System.String.Compare(
                        mPlaylistGridView.Rows[e.RowIndex1].Cells["Title"].Value.ToString(),
                        mPlaylistGridView.Rows[e.RowIndex2].Cells["Title"].Value.ToString());

                }


                break;
            }
            e.Handled = true;

        }

        /// <summary>
        /// Handles the Sorted event of the mPlaylistGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void mPlaylistGridView_Sorted(object sender, EventArgs e) {
            Comparison<MediaFile> comparer;

            SortOrder order = mPlaylistGridView.SortOrder;

            comparer = null;

            switch (mPlaylistGridView.SortedColumn.Name) {
            case ("Artist"):
                if (order == SortOrder.Ascending) {
                    comparer = (a, b) => {
                        int sortVal = System.String.Compare(a.Metadata.Artist, b.Metadata.Artist);
                        if (sortVal == 0) {
                            sortVal = System.String.Compare(a.Metadata.Title, b.Metadata.Title);
                        }
                        if (sortVal == 0) {
                            sortVal = TimeSpan.Compare(a.Metadata.Duration, b.Metadata.Duration);
                        }
                        return sortVal;
                    };
                }
                else {
                    comparer = (b, a) => {
                        int sortVal = System.String.Compare(a.Metadata.Artist, b.Metadata.Artist);
                        if (sortVal == 0) {
                            sortVal = System.String.Compare(a.Metadata.Title, b.Metadata.Title);
                        }
                        if (sortVal == 0) {
                            sortVal = System.TimeSpan.Compare(a.Metadata.Duration, b.Metadata.Duration);
                        }
                        return sortVal;
                    };
                }
                break;
            case ("Title"):
                if (order == SortOrder.Ascending) {
                    comparer = (a, b) => {
                        int sortVal = System.String.Compare(a.Metadata.Title, b.Metadata.Title);
                        if (sortVal == 0) {
                            sortVal = System.String.Compare(a.Metadata.Artist, b.Metadata.Artist);
                        }
                        if (sortVal == 0) {
                            sortVal = System.TimeSpan.Compare(a.Metadata.Duration, b.Metadata.Duration);
                        }
                        return sortVal;
                    };
                }
                else {
                    comparer = (b, a) => {
                        int sortVal = System.String.Compare(a.Metadata.Title, b.Metadata.Title);
                        if (sortVal == 0) {
                            sortVal = System.String.Compare(a.Metadata.Artist, b.Metadata.Artist);
                        }
                        if (sortVal == 0) {
                            sortVal = System.TimeSpan.Compare(a.Metadata.Duration, b.Metadata.Duration);
                        }
                        return sortVal;
                    };
                }
                break;
            case ("Length"):
                if (order == SortOrder.Ascending) {
                    comparer = (a, b) => {
                        int sortVal = System.TimeSpan.Compare(a.Metadata.Duration, b.Metadata.Duration);
                        if (sortVal == 0) {
                            sortVal = System.String.Compare(a.Metadata.Artist, b.Metadata.Artist);
                        }
                        if (sortVal == 0) {
                            sortVal = System.String.Compare(a.Metadata.Title, b.Metadata.Title);
                        }
                        return sortVal;
                    };
                }
                else {
                    comparer = (b, a) => {
                        int sortVal = System.TimeSpan.Compare(a.Metadata.Duration, b.Metadata.Duration);
                        if (sortVal == 0) {
                            sortVal = System.String.Compare(a.Metadata.Artist, b.Metadata.Artist);
                        }
                        if (sortVal == 0) {
                            sortVal = System.String.Compare(a.Metadata.Title, b.Metadata.Title);
                        }
                        return sortVal;
                    };
                }
                break;
            }

            CurrentPlaylist.Sort(comparer);
        }

        #endregion

        #endregion
        
        #region [ Private Members ]

        /// <summary>
        /// Private property containing the index of the song current media file in
        /// the current playlist.
        /// </summary>
        /// <value>
        /// The index of playing song in the playlist.
        /// </value>
        private Int32 IndexPlaying { get; set; }

        /// <summary>
        /// Private property containing the date time the control was scrolled.
        /// </summary>
        /// <value>
        /// The time scrolled.
        /// </value>
        private DateTime TimeScrolled { get; set; }

        /// <summary>
        /// Private property containing the data grid view row being moved.
        /// </summary>
        /// <value>
        /// The moving row.
        /// </value>
        private DataGridViewRow MovingRow { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether a row is being dragged.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if a row is being dragged; otherwise, <c>false</c>.
        /// </value>
        private Boolean IsRowDragging { get; set; }

        /// <summary>
        /// Private property containing a reference to the current playlist.
        /// </summary>
        /// <value>
        /// The current playlist.
        /// </value>
        private Playlist CurrentPlaylist { get; set; }

        /// <summary>
        /// Private property containing a data table for playlist which contains
        /// the media files in the playlist and the associated metadata.
        /// </summary>
        /// <value>
        /// The playlist data table.
        /// </value>
        private DataTable PlaylistData { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the selection changed was
        /// externally changed.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if the selection change was external; otherwise, <c>false</c>.
        /// </value>
        private Boolean ExternalSelectionChange { get; set; }
        
        #endregion
    
    }
}
