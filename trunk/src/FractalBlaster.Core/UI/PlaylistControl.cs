using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FractalBlaster.Universe;

namespace FractalBlaster.Core.UI
{
    public delegate void PlaylistControlSongPlayedEventHandler(PlaylistControl sender,int songIndex);

    public partial class PlaylistControl : UserControl
    {

        public event PlaylistControlSongPlayedEventHandler SongPlayed;

        public Int32 SelectedIndex
        {
            get { return mPlaylistGridView.SelectedRows[0].Index; }
        }

        public Playlist Playlist
        {
            get { return playlist; }
            set
            {
                playlist = value;
                playlist.MediaAdded += new MediaChangeHandler(AddMediaToPlaylistData);
                playlist.SelectedChanged += (o, ea) =>
                {
                    if (ExternalSelectionChange)
                    {
                        int index = Playlist.SelectedIndex;
                        DataGridViewRow row = mPlaylistGridView.Rows[index];
                        row.Selected = true;
                    }
                };
                foreach (MediaFile mf in Playlist)
                {
                    AddMediaToPlaylistData(mf);
                }

                playlist.MediaRequested += new MediaChangeHandler(playlist_MediaRequested);
            }
        }

        public MediaFile SelectedMediaFile
        {
            get
            {
                return mPlaylistGridView.Rows[Playlist.SelectedIndex].Tag as MediaFile;
            }
        }

        public PlaylistControl()
        {
            InitializeComponent();
            PlaylistData = new DataTable();

            AllowDrop = true;

            mPlaylistGridView.DragOver += (o, e) =>
            {
                e.Effect = DragDropEffects.Copy;
            };
            mPlaylistGridView.DragEnter += new DragEventHandler(DragEnterHandler);
            mPlaylistGridView.DragDrop += new DragEventHandler(DragDropHandler);

            bool sortable = Config.getProperty("sortable") == "false" ? false : true;
            if (!sortable)
            {
                foreach (DataGridViewColumn dgvc in mPlaylistGridView.Columns)
                {
                    dgvc.SortMode = DataGridViewColumnSortMode.NotSortable;
                }
            }

            //Handle sorting in a custom manner to maintain synchronization of display and playlist....
            mPlaylistGridView.Sorted += new EventHandler(mPlaylistGridView_Sorted);
            mPlaylistGridView.KeyDown += new KeyEventHandler(mPlaylistGridView_KeyDown);
            mPlaylistGridView.SortCompare += new DataGridViewSortCompareEventHandler(mPlaylistGridView_SortCompare);
            mPlaylistGridView.UserDeletingRow += new DataGridViewRowCancelEventHandler(mPlaylistGridView_UserDeletingRow);

            //Event handlers for dragging items around in the playlist
            mPlaylistGridView.CellMouseDown += new DataGridViewCellMouseEventHandler(mPlaylistGridView_CellMouseDown);
            mPlaylistGridView.CellMouseUp += new DataGridViewCellMouseEventHandler(mPlaylistGridView_CellMouseUp);
            mPlaylistGridView.CellMouseEnter += new DataGridViewCellEventHandler(mPlaylistGridView_CellMouseEnter);
            mPlaylistGridView.MouseMove += new MouseEventHandler(mPlaylistGridView_MouseMove);
        }

        int playingIndex = 0;
        void playlist_MediaRequested(MediaFile file)
        {
            mPlaylistGridView.Rows[playingIndex].DefaultCellStyle.BackColor = Color.White;
            playingIndex = playlist.SelectedIndex;
            mPlaylistGridView.Rows[playlist.SelectedIndex].DefaultCellStyle.BackColor = Color.LightGray;
        }


        void mPlaylistGridView_MouseMove(object sender, MouseEventArgs e)
        {
            if (_draggingRow)
            {
                double timeSinceScroll = DateTime.Now.Subtract(_atScrollTime).TotalSeconds;
                if (timeSinceScroll > 0.05)
                {
                    if (mPlaylistGridView.Rows[_movingRow.Index + 1].Displayed == false)
                    {
                        _atScrollTime = DateTime.Now;
                        mPlaylistGridView.FirstDisplayedScrollingRowIndex++;
                    }

                    if (_movingRow.Index > 0 && mPlaylistGridView.Rows[_movingRow.Index - 1].Displayed == false)
                    {
                        _atScrollTime = DateTime.Now;
                        mPlaylistGridView.FirstDisplayedScrollingRowIndex--;
                    }
                }
            }
        }

        DateTime _atScrollTime;
        void mPlaylistGridView_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (_draggingRow && e.RowIndex >= 0 && e.RowIndex < mPlaylistGridView.RowCount - 1)
            {
                playlist.MoveItem(_movingRow.Index, e.RowIndex);

                mPlaylistGridView.Rows.RemoveAt(_movingRow.Index);
                mPlaylistGridView.Rows.Insert(e.RowIndex, _movingRow);

                _movingRow.Selected = true;
            }
        }

        DataGridViewRow _movingRow;
        bool _draggingRow = false;
        void mPlaylistGridView_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < mPlaylistGridView.RowCount - 1 && e.ColumnIndex >= 0 && e.ColumnIndex < mPlaylistGridView.ColumnCount)
            {
                _movingRow = mPlaylistGridView.Rows[e.RowIndex];
                _draggingRow = true;
            }
        }

        void mPlaylistGridView_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            _movingRow = null;
            _draggingRow = false;
        }

        void mPlaylistGridView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (SongPlayed != null)
                    SongPlayed(this, mPlaylistGridView.CurrentRow.Index);
                e.Handled = true;
            }
        }

        void mPlaylistGridView_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            _draggingRow = false;
            playlist.RemoveMedia(e.Row.Index);
        }

        #region Sorting Methods

        void mPlaylistGridView_SortCompare(object sender, DataGridViewSortCompareEventArgs e)
        {
            switch (e.Column.Name)
            {
                case ("Artist"):
                    e.SortResult = e.CellValue1.ToString().CompareTo(e.CellValue2.ToString());
                    if (e.SortResult == 0)
                    {
                        e.SortResult = System.String.Compare(
                            mPlaylistGridView.Rows[e.RowIndex1].Cells["Title"].Value.ToString(),
                            mPlaylistGridView.Rows[e.RowIndex2].Cells["Title"].Value.ToString());

                    }
                    if (e.SortResult == 0)
                    {
                        e.SortResult = System.TimeSpan.Compare(
                            TimeSpan.Parse(mPlaylistGridView.Rows[e.RowIndex1].Cells["Length"].Value.ToString()),
                            TimeSpan.Parse(mPlaylistGridView.Rows[e.RowIndex2].Cells["Length"].Value.ToString()));
                    }
                    break;
                case ("Title"):
                    e.SortResult = e.CellValue1.ToString().CompareTo(e.CellValue2.ToString());

                    if (e.SortResult == 0)
                    {
                        e.SortResult = System.String.Compare(
                            mPlaylistGridView.Rows[e.RowIndex1].Cells["Artist"].Value.ToString(),
                            mPlaylistGridView.Rows[e.RowIndex2].Cells["Artist"].Value.ToString());
                    }

                    if (e.SortResult == 0)
                    {
                        e.SortResult = System.TimeSpan.Compare(
                            TimeSpan.Parse(mPlaylistGridView.Rows[e.RowIndex1].Cells["Length"].Value.ToString()),
                            TimeSpan.Parse(mPlaylistGridView.Rows[e.RowIndex2].Cells["Length"].Value.ToString()));
                    }
                    break;
                case ("Length"):

                    e.SortResult = System.TimeSpan.Compare(
                        TimeSpan.Parse(mPlaylistGridView.Rows[e.RowIndex1].Cells["Length"].Value.ToString()),
                        TimeSpan.Parse(mPlaylistGridView.Rows[e.RowIndex2].Cells["Length"].Value.ToString()));


                    if (e.SortResult == 0)
                    {
                        e.SortResult = System.String.Compare(
                            mPlaylistGridView.Rows[e.RowIndex1].Cells["Artist"].Value.ToString(),
                            mPlaylistGridView.Rows[e.RowIndex2].Cells["Artist"].Value.ToString());
                    }

                    if (e.SortResult == 0)
                    {
                        e.SortResult = System.String.Compare(
                            mPlaylistGridView.Rows[e.RowIndex1].Cells["Title"].Value.ToString(),
                            mPlaylistGridView.Rows[e.RowIndex2].Cells["Title"].Value.ToString());

                    }


                    break;
            }
            e.Handled = true;

        }


        void mPlaylistGridView_Sorted(object sender, EventArgs e)
        {
            Comparison<MediaFile> comparer;

            SortOrder order = mPlaylistGridView.SortOrder;

            comparer = null;

            switch (mPlaylistGridView.SortedColumn.Name)
            {
                case ("Artist"):
                    if (order == SortOrder.Ascending)
                    {
                        comparer = (a, b) =>
                            {
                                int sortVal = System.String.Compare(a.Metadata.Artist, b.Metadata.Artist);
                                if (sortVal == 0)
                                {
                                    sortVal = System.String.Compare(a.Metadata.Title, b.Metadata.Title);
                                }
                                if (sortVal == 0)
                                {
                                    sortVal = TimeSpan.Compare(a.Metadata.Duration, b.Metadata.Duration);
                                }
                                return sortVal;
                            };
                    }
                    else
                    {
                        comparer = (b, a) =>
                        {
                            int sortVal = System.String.Compare(a.Metadata.Artist, b.Metadata.Artist);
                            if (sortVal == 0)
                            {
                                sortVal = System.String.Compare(a.Metadata.Title, b.Metadata.Title);
                            }
                            if (sortVal == 0)
                            {
                                sortVal = System.TimeSpan.Compare(a.Metadata.Duration, b.Metadata.Duration);
                            }
                            return sortVal;
                        };
                    }
                    break;
                case ("Title"):
                    if (order == SortOrder.Ascending)
                    {
                        comparer = (a, b) =>
                           {
                               int sortVal = System.String.Compare(a.Metadata.Title, b.Metadata.Title);
                               if (sortVal == 0)
                               {
                                   sortVal = System.String.Compare(a.Metadata.Artist, b.Metadata.Artist);
                               }
                               if (sortVal == 0)
                               {
                                   sortVal = System.TimeSpan.Compare(a.Metadata.Duration, b.Metadata.Duration);
                               }
                               return sortVal;
                           };
                    }
                    else
                    {
                        comparer = (b, a) =>
                        {
                            int sortVal = System.String.Compare(a.Metadata.Title, b.Metadata.Title);
                            if (sortVal == 0)
                            {
                                sortVal = System.String.Compare(a.Metadata.Artist, b.Metadata.Artist);
                            }
                            if (sortVal == 0)
                            {
                                sortVal = System.TimeSpan.Compare(a.Metadata.Duration, b.Metadata.Duration);
                            }
                            return sortVal;
                        };
                    }
                    break;
                case ("Length"):
                    if (order == SortOrder.Ascending)
                    {
                        comparer = (a, b) =>
                            {
                                int sortVal = System.TimeSpan.Compare(a.Metadata.Duration, b.Metadata.Duration);
                                if (sortVal == 0)
                                {
                                    sortVal = System.String.Compare(a.Metadata.Artist, b.Metadata.Artist);
                                }
                                if (sortVal == 0)
                                {
                                    sortVal = System.String.Compare(a.Metadata.Title, b.Metadata.Title);
                                }
                                return sortVal;
                            };
                    }
                    else
                    {
                        comparer = (b, a) =>
                        {
                            int sortVal = System.TimeSpan.Compare(a.Metadata.Duration, b.Metadata.Duration);
                            if (sortVal == 0)
                            {
                                sortVal = System.String.Compare(a.Metadata.Artist, b.Metadata.Artist);
                            }
                            if (sortVal == 0)
                            {
                                sortVal = System.String.Compare(a.Metadata.Title, b.Metadata.Title);
                            }
                            return sortVal;
                        };
                    }
                    break;
            }

            playlist.Sort(comparer);
        }

        #endregion

        public PlaylistControl(Playlist playlist)
            : this()
        {
            Playlist = playlist;
        }

        private void mPlaylistGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (mPlaylistGridView.SelectedRows.Count > 0)
            {
                DataGridViewRow row = mPlaylistGridView.SelectedRows[0];
                if (row.Index <= Playlist.Items.Count())
                {
                    //Playlist.SelectedIndex = row.Index;
                    ExternalSelectionChange = false;
                }
            }
        }

        private void AddMediaToPlaylistData(MediaFile media)
        {
            if (media != null)
            {
                mPlaylistGridView.Rows.Add(new String[] {
                media.Metadata.Artist,
                media.Metadata.Title,
                media.Metadata.Duration.ToString()
            });
                mPlaylistGridView.Rows[mPlaylistGridView.Rows.Count - 1].Tag = media;
            }
        }
        private void mPlaylistGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (SongPlayed != null)
                SongPlayed(this, e.RowIndex);
        }

        private void DragEnterHandler(Object sender, DragEventArgs args)
        {
            Object data = args.Data.GetData(typeof(Playlist));
            if (data != null)
            {
                args.Effect = DragDropEffects.Copy;
            }
            else
            {
                args.Effect = DragDropEffects.None;
            }
        }

        private void DragDropHandler(Object sender, DragEventArgs e)
        {
            Playlist playlist = e.Data.GetData(typeof(Playlist)) as Playlist;
            if (playlist != null)
                foreach (MediaFile media in playlist)
                {
                    Playlist.AddItem(media);
                }
        }




        private Playlist playlist;
        private DataTable PlaylistData { get; set; }
        private Boolean ExternalSelectionChange { get; set; }

    }
}
