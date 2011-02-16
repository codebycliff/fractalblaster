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
                        mf.Metadata["Artist"].Value.ToString(),
                        mf.Metadata["Album"].Value.ToString(),
                        mf.Metadata["Duration"].Value.ToString()
                    });
                }
            }
        }

        public MediaFile SelectedMediaFile {
            get {
                DataGridViewRow row = mPlaylistGridView.SelectedRows[0];
                return Playlist.Where(m => 
                    m.Metadata["Title"].Value.ToString().CompareTo(row.Cells[0].Value.ToString()) == 0
                    && m.Metadata["Album"].Value.ToString().CompareTo(row.Cells[2].Value.ToString()) == 0
                    && m.Metadata["Artist"].Value.ToString().CompareTo(row.Cells[1].Value.ToString()) == 0
                    ).First();
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
    }
}
