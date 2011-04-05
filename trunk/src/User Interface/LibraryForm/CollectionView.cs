using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FractalBlaster.LibraryForm {

    public enum ViewMode {
        Tree,
        Icon,
        List
    }

    public partial class CollectionView : UserControl {

        #region [ Abstract ]

        public virtual String Label { get { return null; } }

        public virtual Boolean HasCustomToolStrip { get { return false; } }

        public virtual Boolean HasConfiguration { get { return false; } }

        public virtual Form ConfigurationDialog { get { return null; } }

        public virtual void RefreshItems(Object sender, EventArgs args) { }

        public virtual void RefreshView(Object sender, EventArgs args) { }
        
        #endregion


        public ViewMode ViewMode {
            get {
                return mViewMode;
            }
            set {
                if (value != ViewMode) {
                    mViewMode = value;
                    ViewChanged(this, new EventArgs());
                }
            }
        }

        public CollectionView() {
            InitializeComponent();
            ViewMode = ViewMode.Tree;
            mTreeViewButton.Enabled = false;
            
            // Setup the tags on the view buttons to reference their enum value...
            mIconViewButton.Tag = ViewMode.Icon;
            mTreeViewButton.Tag = ViewMode.Tree;
            mListViewButton.Tag = ViewMode.List;

            mTreeViewButton.Click += new EventHandler(ViewChanged);
            mIconViewButton.Click += new EventHandler(ViewChanged);
            mListViewButton.Click += new EventHandler(ViewChanged);

            // Hook up the refresh items handler...
            mRefreshButton.Click += new EventHandler(RefreshItems);

          
            mTreeViewButton.Click += (sender, args) => {
                
            };

            // If the view has a custom toolstrip...
            if (HasCustomToolStrip) {
                mCustomToolBar.Text = String.Format("{0} ToolBar", Name);
                mCustomToolBar.Visible = true;
                mCustomToolBar.ImageScalingSize = new Size(16, 16);
            }
            else {
                mCustomToolBar.Visible = false;
            }

            // If the view has configuration...
            if (HasConfiguration) {
                mConfigureButton.Click += (sender, args) => {
                    Form form = ConfigurationDialog;
                    form.ShowDialog(this);
                };
            }
            else {
                mConfigureButton.Enabled = false;
            }
        }


        private void ViewChanged(Object sender, EventArgs args) {
            if (sender is ToolStripItem) {
                ViewMode = (ViewMode)(sender as ToolStripItem).Tag;
            }
            else if (sender is CollectionView) {
                switch (ViewMode) {
                case ViewMode.Tree:
                    mTreeViewButton.Enabled = false;
                    mIconViewButton.Enabled = true;
                    mListViewButton.Enabled = true;
                    break;
                case ViewMode.Icon:
                    mIconViewButton.Enabled = false;
                    mTreeViewButton.Enabled = true;
                    mListViewButton.Enabled = true;
                    break;
                case ViewMode.List:
                    mListViewButton.Enabled = false;
                    mTreeViewButton.Enabled = true;
                    mIconViewButton.Enabled = true;
                    break;
                }
                RefreshView(sender, args);
            }
        }

        private ViewMode mViewMode;
        
    }

}
