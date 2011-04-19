using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FractalBlaster.Universe;
using FractalBlaster.Core.Runtime;
using System.Threading;

namespace FractalBlaster.Core.UI {

    /// <remarks>
    /// Class that extends <see cref="CollectionView"/> to provide a user interface
    /// to view and interact with the audio Library.
    /// </remarks>
    public partial class LibraryCollectionView : CollectionView {
       
        /// <summary>
        /// Initializes a new instance of the <see cref="LibraryCollectionView"/> class.
        /// </summary>
        /// <param name="Library">The Library.</param>
        public LibraryCollectionView(Library library) : base() {
            InitializeComponent();
            Library = library;

            ImageList = new ImageList();
            ImageList.Images.Add(Properties.Resources.view_media_artist);
            ImageList.Images.Add(Properties.Resources.media_optical_recordable);
            ImageList.Images.Add(Properties.Resources.text_xmcd);
            ImageList.Images.Add(Properties.Resources.server_database);
            mMediaTreeView.Sorted = true;

            this.mMediaTreeView.KeyDown += new KeyEventHandler(mMediaTreeView_KeyDown);
            this.HandleDestroyed += new EventHandler(LibraryCollectionView_HandleDestroyed);

            mConfigureButton.Click += (sender, args) =>
            {
                LibraryConfigurationDialog form = ConfigurationDialog;
                form.Saved += (sender2, args2) =>
                {
                    this.RefreshItems(sender2, args2);
                };
                form.ShowDialog(this);
            };

            InitializeTreeView();
        }

        /// <summary>
        /// Gets the Library represented by this Library collection view.
        /// </summary>
        public Library Library { get; private set; }

        #region [ CollectionView Overrides ]

        /// <summary>
        /// Gets a value indicating whether this instance has custom tool strip.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance has custom tool strip; otherwise, <c>false</c>.
        /// </value>
        public override bool HasCustomToolStrip { get { return false; } }

        /// <summary>
        /// Gets a value indicating whether this instance has configuration.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance has configuration; otherwise, <c>false</c>.
        /// </value>
        public override bool HasConfiguration { get { return true; } }

        /// <summary>
        /// Gets the label for the collection view.
        /// </summary>
        public override string Label { get { return "Library"; } }

        /// <summary>
        /// Gets the configuration dialog for this Library collection view.
        /// </summary>
        public LibraryConfigurationDialog ConfigurationDialog { get { return new LibraryConfigurationDialog(Library); } }

        /// <summary>
        /// Event handler that refreshes the actual collection view.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="args">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        public override void RefreshView(object sender, EventArgs args) {
            base.RefreshView(sender, args);
        }

        /// <summary>
        /// Event handler that refreshes the items in the collection view.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="args">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        public override void RefreshItems(object sender, EventArgs args) {
            base.RefreshItems(sender, args);
            this.mRefreshButton.Enabled = false;

            mMediaTreeView.Nodes.Clear();
            LocalThread = new Thread(RefreshItems_Threaded);
            LocalThread.Priority = ThreadPriority.Lowest;
            LocalThread.Start();
        }


        #endregion

        #region [ Private ]

        /// <summary>
        /// Private delegate to handle the adding of the tree node.
        /// </summary>
        /// <param name="n">The tree node.</param>
        private delegate void Add(TreeNode n);

        /// <summary>
        /// Private delegate to handle the changing of text.
        /// </summary>
        /// <param name="text">The text.</param>
        private delegate void ChangeText(string text);

        /// <summary>
        /// Handles the HandleDestroyed event of the LibraryCollectionView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void LibraryCollectionView_HandleDestroyed(object sender, EventArgs e) {
            LocalThread.Abort();
        }

        /// <summary>
        /// Mouses down on tree view.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
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

        /// <summary>
        /// Private helper that changes the status strip1 text.
        /// </summary>
        /// <param name="text">The text.</param>
        private void ChangeStatusStrip1Text(string text) {
            label1.Text = text;
        }

        /// <summary>
        /// Enables the refresh button.
        /// </summary>
        private void EnableRefreshButton() {
            mRefreshButton.Enabled = true;
        }

        /// <summary>
        /// Adds a tree node.
        /// </summary>
        /// <param name="n">The tree node.</param>
        private void AddNode(TreeNode n) {
            mMediaTreeView.Nodes[0].Nodes.Add(n);
        }

        
        /// <summary>
        /// Private helper that initializes the tree view.
        /// </summary>
        private void InitializeTreeView() {
            TreeNode root = new TreeNode("Library Collection", 3, 3);
            root.Tag = Library.AllMedia;
            foreach (String artist in Library.Artists) {
                TreeNode artistNode = new TreeNode(artist);
                artistNode.Tag = Library.MediaForArtist(artist);
                Library.AlbumCollection albums = Library[artist];
                foreach (Library.Album album in albums) {
                    TreeNode albumNode = new TreeNode(album.AlbumName, 1, 1);
                    albumNode.Tag = album.ToArray();
                    foreach (MediaFile file in album) {
                        TreeNode fileNode = new TreeNode(file.Metadata.Title, 2, 2);
                        fileNode.Tag = new MediaFile[] { file }.AsEnumerable();
                        albumNode.Nodes.Add(fileNode);
                    }
                    artistNode.Nodes.Add(albumNode);
                }
                root.Nodes.Add(artistNode);
            }
            mMediaTreeView.Nodes.Add(root);
        }

        /// <summary>
        /// Refreshes the items in threaded fashion.
        /// </summary>
        private void RefreshItems_Threaded() {
            TreeNode root = new TreeNode("Library Collection", 3, 3);
            
            mMediaTreeView.Invoke(new ChangeText(ChangeStatusStrip1Text), new object[] { "Scanning Library Directory..." });
            Library.Refresh();
            mMediaTreeView.Invoke(new ChangeText(ChangeStatusStrip1Text), new object[] { "Adding music to library..." });
            root.Tag = Library.AllMedia;
            foreach (String artist in Library.Artists) {
                TreeNode artistNode = new TreeNode(artist, 0, 0);
                artistNode.Tag = Library.MediaForArtist(artist);
                Library.AlbumCollection albums = Library[artist];
                foreach (Library.Album album in albums) {
                    TreeNode albumNode = new TreeNode(album.AlbumName, 1, 1);
                    albumNode.Tag = album.ToArray();
                    foreach (MediaFile file in album) {
                        TreeNode fileNode = new TreeNode(file.Metadata.Title, 2, 2);
                        fileNode.Tag = new MediaFile[] { file }.AsEnumerable();
                        albumNode.Nodes.Add(fileNode);
                    }
                    artistNode.Nodes.Add(albumNode);
                }
                root.Nodes.Add(artistNode);
            }
            mMediaTreeView.Invoke((Action)(() => { mMediaTreeView.Nodes.Add(root); }));
            mMediaTreeView.Invoke(new ChangeText(ChangeStatusStrip1Text), new object[] { "" });
            mMediaTreeView.Invoke(new MethodInvoker(EnableRefreshButton));
        }

        /// <summary>
        /// Handles the DoubleClick event of the mMediaTreeView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void mMediaTreeView_DoubleClick(object sender, EventArgs e) {
            if (mMediaTreeView.SelectedNode == null)
                return;

            MediaFile[] m = (MediaFile[])mMediaTreeView.SelectedNode.Tag;
            if (m != null && m.Count() == 1) {
                FamilyKernel.Instance.Context.Engine.CurrentPlaylist.AddItem(m[0]);
            }
        }

        /// <summary>
        /// Handles the KeyDown event of the mMediaTreeView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void mMediaTreeView_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter) {
                if (mMediaTreeView.SelectedNode == null) {
                    e.Handled = true;
                    e.SuppressKeyPress = true;
                    return;
                }

                MediaFile[] m = (MediaFile[])mMediaTreeView.SelectedNode.Tag;
                if (m != null) {
                    foreach (MediaFile mediaFile in m) {
                        FamilyKernel.Instance.Context.Engine.CurrentPlaylist.AddItem(mediaFile);
                    }
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        /// <summary>
        /// Gets or sets the image list containing 
        /// </summary>
        /// <value> The image list. </value>
        private ImageList ImageList { get; set; }

        /// <summary>
        /// Gets or sets the local thread.
        /// </summary>
        /// <value>
        /// The local thread.
        /// </value>
        private Thread LocalThread { get; set; }
        
        #endregion
    
    }
}
