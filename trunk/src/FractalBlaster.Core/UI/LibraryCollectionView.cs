using System;
using System.Collections.Generic;
using System.ComponentModel;
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
            mMediaTreeView.ItemDrag += (sender, args) => {
                TreeNode node = (TreeNode)args.Item;
                if (node.Tag != null) {
                    MediaFile file = node.Tag as MediaFile;
                    
                }
            };
        }



        public override void RefreshItems(object sender, EventArgs args) {
            base.RefreshItems(sender, args);
            Library.Refresh();
            
            TreeNode root = new TreeNode("Library Collection", 3,3);
            foreach (String artist in Library.Artists) {
                TreeNode artistNode = new TreeNode(artist, 0,0);
                Dictionary<String, List<MediaFile>> albums = Library[artist];
                foreach (String album in albums.Keys) {
                    TreeNode albumNode = new TreeNode(album, 1, 1);
                    foreach (MediaFile file in albums[album]) {
                        TreeNode fileNode = new TreeNode(file.Metadata.Title, 2, 2);
                        fileNode.Tag = file;
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
