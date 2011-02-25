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
    public partial class PlaylistControl : UserControl {

        public Playlist Playlist {
            get { return playlist; }
            set {
                playlist = value;
                playlist.MediaAdded += new MediaChangeHandler(AddMediaToPlaylistData);
                playlist.SelectedChanged += (o, ea) => {
                    if (ExternalSelectionChange) {
                        int index = Playlist.SelectedIndex;
                        DataGridViewRow row = mPlaylistGridView.Rows[index];
                        row.Selected = true;
                    }
                };
                foreach (MediaFile mf in Playlist) {
                    AddMediaToPlaylistData(mf);
                }
            }
        }

        public MediaFile SelectedMediaFile {
            get {
                return mPlaylistGridView.Rows[Playlist.SelectedIndex].Tag as MediaFile;
            }
        }

        public PlaylistControl() {
            InitializeComponent();
            PlaylistData = new DataTable();
           
            AllowDrop = true;
            
            mPlaylistGridView.DragOver += (o, e) => {
                e.Effect = DragDropEffects.Copy;
            };
            mPlaylistGridView.DragEnter += new DragEventHandler(DragEnterHandler);
            mPlaylistGridView.DragDrop += new DragEventHandler(DragDropHandler);

        }

        public PlaylistControl(Playlist playlist) : this() {
            Playlist = playlist;
        }

        private void mPlaylistGridView_SelectionChanged(object sender, EventArgs e) {
            DataGridViewRow row = mPlaylistGridView.SelectedRows[0];
            if (row.Index <= Playlist.Items.Count()) {
                Playlist.SelectedIndex = row.Index;
                ExternalSelectionChange = false;
            }
        }

        private void AddMediaToPlaylistData(MediaFile media) {
            mPlaylistGridView.Rows.Add(new String[] {
                media.Metadata.Title,
                media.Metadata.Duration.ToString()
            });
            mPlaylistGridView.Rows[mPlaylistGridView.Rows.Count - 1].Tag = media;
        }
        private void mPlaylistGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e) {
            Playlist.RequestMediaAt(e.RowIndex);
        }

        private void DragEnterHandler(Object sender, DragEventArgs args) {
            Object data = args.Data.GetData(typeof(Playlist));
            if (data != null) {
                args.Effect = DragDropEffects.Copy;
            }
            else {
                args.Effect = DragDropEffects.None;
            }
        }

        private void DragDropHandler(Object sender, DragEventArgs e) {
            Playlist playlist = e.Data.GetData(typeof(Playlist)) as Playlist;
            foreach (MediaFile media in playlist) {
                Playlist.AddItem(media);
            }
        }

        private Playlist playlist;
        private DataTable PlaylistData { get; set; }
        private Boolean ExternalSelectionChange { get; set; }

    }
}
