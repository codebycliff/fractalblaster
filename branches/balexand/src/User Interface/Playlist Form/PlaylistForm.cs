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
        bool multiplePlaylists;

        public PlaylistForm()
        {
            if (Config.getProperty("multipleplaylists").Equals("true"))
            {
                multiplePlaylists = true;
           }
            else
            {
                multiplePlaylists = false;
            }
            
            InitializeComponent();

            if (multiplePlaylists)
            {
                ImageList tabImages = new ImageList();
                tabImages.Images.Add(FractalBlaster.PlaylistForm.Properties.Resources.add);
                tabControl1.ImageList = tabImages;
                TabPage tabPage2 = new TabPage();
                tabControl1.Controls.Add(tabPage2);
                tabPage2.ImageIndex = 0;
                
            }
            PlaylistDisplay c = new PlaylistDisplay();
            c.ParentForm = this;
            c.Name = "playlistdisplay";
            c.Paint += new PaintEventHandler(paintPlaylistDisplay);
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

        public void selectIndex(int i)
        {
            mPlaylist.selectIndex(i);
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (multiplePlaylists)
            {
                int i = tabControl1.SelectedIndex;
                if (i == tabControl1.TabCount - 1)
                {
                    Debug.printline("new playlist");
                    mPlaylist.newList();
                    TabPage newTabPage = new TabPage("New Playlist");
                    PlaylistDisplay c = new PlaylistDisplay();
                    c.ParentForm = this;
                    c.Name = "playlistdisplay";
                    c.Paint += new PaintEventHandler(paintPlaylistDisplay);
                    newTabPage.Controls.Add(c);
                    tabControl1.TabPages.Insert(i, newTabPage);
                    tabControl1.SelectTab(i);
                }
                else
                {
                    mPlaylist.selectList(i);
                }
            }
        }

        private void paintPlaylistDisplay(object sender, PaintEventArgs e)
        {
            List<MediaFile> currentList = mPlaylist.getList();
            PlaylistDisplay pd = (PlaylistDisplay) tabControl1.SelectedTab.Controls["playlistdisplay"];
            if (pd == null)
            {
                return;
            }
            pd.clearRows();
            foreach (MediaFile mf in currentList)
            {
                pd.addRow(mf.Metadata.Artist, mf.Metadata.Title);
            }
            pd.selectIndex(mPlaylist.getCurrentIndex());
        }

    }
}
