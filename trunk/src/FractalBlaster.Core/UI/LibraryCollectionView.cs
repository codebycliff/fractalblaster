using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FractalBlaster.Universe;

namespace FractalBlaster.Core.UI {

    public partial class LibraryCollectionView : CollectionView {

        public override bool HasCustomToolStrip { get { return false; } }
        
        public override bool HasConfiguration { get { return true; } }

        public override string Label { get { return "Library"; } }
        
        public override Form ConfigurationDialog { get { return new LibraryConfigurationDialog(); } }

        public Library Library { get; private set; }

        public TreeView Tree { get { return mMediaTreeView; } }

        public LibraryCollectionView(Library library) : base() {
            InitializeComponent();
            Library = library;
            
            ImageList = new ImageList();
            ImageList.Images.Add(Properties.Resources.view_media_artist);
            ImageList.Images.Add(Properties.Resources.media_optical_recordable);
            ImageList.Images.Add(Properties.Resources.text_xmcd);
            ImageList.Images.Add(Properties.Resources.server_database);
            mMediaTreeView.ImageList = ImageList;
            RefreshItems(this, new EventArgs());
            mMediaTreeView.MouseDown += new MouseEventHandler(MouseDownOnTreeView);
        }

        private void MouseDownOnTreeView(Object sender, MouseEventArgs args) {
            TreeView view = sender as TreeView;
            if (view != null) {
                TreeNode node = view.GetNodeAt(args.X, args.Y) ?? null;
                if (node == null) {
                    return;
                }
                
                IEnumerable<MediaFile> items = node.Tag as IEnumerable<MediaFile>;
                
                Playlist p = new Playlist();
                foreach (MediaFile m in items) {
                    p.AddItem(m);
                }
                //MessageBox.Show(String.Format("Name: {0}\nTag null?: {1}\nCount: {2}", node.Text, node.Tag == null ? "Yes" : "No", items.Count().ToString()));
                DoDragDrop(p, DragDropEffects.Copy);
            }
        }

        public override void RefreshItems(object sender, EventArgs args) {
            base.RefreshItems(sender, args);
            Library.Refresh();
            
            TreeNode root = new TreeNode("Library Collection", 3,3);
            root.Tag = Library.AllMedia;
            foreach (String artist in Library.Artists) {
                TreeNode artistNode = new TreeNode(artist, 0,0);
                artistNode.Tag = Library.MediaForArtist(artist);
                Dictionary<String, List<MediaFile>> albums = Library[artist];
                foreach (String album in albums.Keys) {
                    TreeNode albumNode = new TreeNode(album, 1, 1);
                    albumNode.Tag = Library.MediaForAlbum(artist, album);
                    foreach (MediaFile file in albums[album]) {
                        TreeNode fileNode = new TreeNode(file.Metadata.Title, 2, 2);
                        fileNode.Tag = new MediaFile[] { file }.AsEnumerable();
                        albumNode.Nodes.Add(fileNode);
                    }
                    artistNode.Nodes.Add(albumNode);
                }
                root.Nodes.Add(artistNode);
            }
            
            mMediaTreeView.Nodes.Clear();
            mMediaTreeView.Nodes.Add(root);
            mMediaTreeView.Refresh();
        }

        public override void RefreshView(object sender, EventArgs args) {
            base.RefreshView(sender, args);
            switch (ViewMode) {
            case ViewMode.Tree:
                break;
            case ViewMode.Icon:
                break;
            case ViewMode.List:
                break;
            }
        }

        private ImageList ImageList { get; set; }
    }
}
