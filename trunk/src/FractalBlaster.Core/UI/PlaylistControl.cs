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
                foreach (MediaFile mf in Playlist) {
                    mPlaylistGridView.Rows.Add(new String[] {
                        mf.Metadata["Title"].Value.ToString(),
                        mf.Metadata["Duration"].Value.ToString()
                    });
                    mPlaylistGridView.Rows[mPlaylistGridView.Rows.Count - 1].Tag = mf;
                    playlist.SelectedChanged += (o,ea) => {
                        if(ExternalSelectionChange) {
                            int index = Playlist.SelectedIndex;
                            DataGridViewRow row = mPlaylistGridView.Rows[index];
                            row.Selected = true;
                        }
                    };
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
            DragDrop += (sender, args) => {
                if (sender is MediaFile) {
                    Playlist.AddItem(sender as MediaFile);
                }
            };
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

        private void mPlaylistGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e) {
            Playlist.RequestMediaAt(e.RowIndex);
        }

        private Playlist playlist;
        private DataTable PlaylistData { get; set; }
        private Boolean ExternalSelectionChange { get; set; }

    }
}
