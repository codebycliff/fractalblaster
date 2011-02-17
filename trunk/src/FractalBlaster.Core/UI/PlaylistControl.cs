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
        }

        public PlaylistControl(Playlist playlist) : this() {
            Playlist = playlist;
        }

        private Playlist playlist;
        private DataTable PlaylistData { get; set; }

        private void mPlaylistGridView_SelectionChanged(object sender, EventArgs e) {
            DataGridViewRow row = mPlaylistGridView.SelectedRows[0];
            if (row.Index <= Playlist.Items.Count()) {
                Playlist.SelectedIndex = row.Index;
            }
        }

        private void mPlaylistGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e) {
            Playlist.RequestMediaAt(e.RowIndex);
        }
    }
}
