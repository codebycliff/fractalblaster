using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FractalBlaster.Core.UI {

    /// <remarks>
    /// Enumeration of different view modes.
    /// </remarks>
    public enum ViewMode {
        Tree,
        Icon,
        List
    }

    /// <remarks>
    /// Base class for collection views. All members under the 'Virtual' 
    /// region were designed to be abstract; however, you can't have
    /// an abstract class extend UserControl, so they contain empty bodies
    /// as a quick fix.
    /// </remarks>
    public partial class CollectionView : UserControl {
        
        /// <summary>
        /// Gets or sets the view mode.
        /// </summary>
        /// <value>
        /// The view mode.
        /// </value>
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

        /// <summary>
        /// Initializes a new instance of the <see cref="CollectionView"/> class.
        /// </summary>
        public CollectionView() {
            InitializeComponent();
            ViewMode = ViewMode.Tree;

            // Hook up the refresh items handler...
            mRefreshButton.Click += new EventHandler(RefreshItems);

            /*
            // If the view has a custom toolstrip...
            if (HasCustomToolStrip) {
                mCustomToolBar.Text = String.Format("{0} ToolBar", Name);
                mCustomToolBar.Visible = true;
                mCustomToolBar.ImageScalingSize = new Size(16, 16);
            }
            else {
                mCustomToolBar.Visible = false;
            }
             */

            // If the view has configuration...
            if (HasConfiguration) {
            }
            else {
                mConfigureButton.Enabled = false;
            }
        }

        #region [ Virtual ]

        /// <summary>
        /// Gets the label for the collection view.
        /// </summary>
        public virtual String Label { get { return null; } }

        /// <summary>
        /// Gets a value indicating whether this instance has custom tool strip.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance has custom tool strip; otherwise, <c>false</c>.
        /// </value>
        public virtual Boolean HasCustomToolStrip { get { return false; } }

        /// <summary>
        /// Gets a value indicating whether this instance has configuration.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance has configuration; otherwise, <c>false</c>.
        /// </value>
        public virtual Boolean HasConfiguration { get { return false; } }

        /// <summary>
        /// Gets the configuration dialog, if this instance has configuration.
        /// </summary>
        public virtual Form ConfigurationDialog { get { return null; } }

        /// <summary>
        /// Event handler that refreshes the items in the collection view.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="args">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        public virtual void RefreshItems(Object sender, EventArgs args) { }

        /// <summary>
        /// Event handler that refreshes the actual collection view.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="args">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        public virtual void RefreshView(Object sender, EventArgs args) { }

        #endregion

        #region [ Private ]

        /// <summary>
        /// Event handler for when the view mode changes.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="args">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void ViewChanged(Object sender, EventArgs args) {
            if (sender is ToolStripItem) {
                ViewMode = (ViewMode)(sender as ToolStripItem).Tag;
            }
            else if (sender is CollectionView) {
                RefreshView(sender, args);
            }
        }

        /// <summary>
        /// Private instance variable containing the current view mode.
        /// </summary>
        private ViewMode mViewMode;
        
        #endregion
    
    }

}
