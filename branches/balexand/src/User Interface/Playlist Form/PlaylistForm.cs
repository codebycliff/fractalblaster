using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FractalBlaster.Universe;
using System.Resources;

namespace FractalBlaster.PlaylistForm
{
    public partial class PlaylistForm : Form, IPlaylistForm
    {
        IPlaylist mPlaylist;

        public PlaylistForm()
        {
            InitializeComponent();

            ImageList tabImages = new ImageList();
            tabImages.Images.Add(FractalBlaster.PlaylistForm.Properties.Resources.add);
            tabControl1.ImageList = tabImages;
            tabPage2.ImageIndex = 0;
            Control c = new PlaylistDisplay();
            c.Name = "playlistdisplay";    
            tabPage1.Controls.Add(c);
        }

        public Form form
        {
            get { return this; }
        }

        public IPlaylist playlist
        {
            set { mPlaylist = value; }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int i = tabControl1.SelectedIndex;
            if (i == tabControl1.TabCount - 1)
            {
                Debug.printline("new playlist");
                TabPage newTabPage = new TabPage("New Playlist");
                Control c = new PlaylistDisplay();
                c.Name = "playlistdisplay";
                newTabPage.Controls.Add(c);
                tabControl1.TabPages.Insert(i, newTabPage);
                
                tabControl1.SelectTab(i);
            }
        }

        private void PlaylistForm_Paint(object sender, PaintEventArgs e)
        {
            List<MediaFile> currentList = mPlaylist.getList();
            PlaylistDisplay pd = (PlaylistDisplay) tabControl1.SelectedTab.Controls["playlistdisplay"];
            pd.clearRows();
            foreach (MediaFile mf in currentList)
            {
                pd.addRow(mf.Metadata.Artist, mf.Metadata.Title);
            }
            pd.selectIndex(mPlaylist.getCurrentIndex());
        }

    }
}
