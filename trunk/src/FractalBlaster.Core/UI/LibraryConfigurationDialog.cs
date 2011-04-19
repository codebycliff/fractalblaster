using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
using System.Windows.Forms;
using FractalBlaster.Universe;
using FractalBlaster.Core;

namespace FractalBlaster.Core.UI {

    /// <remarks>
    /// Class that provides a configuration dialog for the Library collection 
    /// view.
    /// </summary>
    public partial class LibraryConfigurationDialog : ConfigurationDialog {

        /// <summary>
        /// Initializes a new instance of the <see cref="LibraryConfigurationDialog"/> class.
        /// </summary>
        /// <param name="Library">The Library.</param>
        public LibraryConfigurationDialog(Library library) {
            InitializeComponent();
            this.Library = library;

            this.mAddDirectoryButton.Click += new EventHandler(mAddDirectoryButton_Click);
            this.mSaveButton.Click += new EventHandler(mSaveButton_Click);

            listBox1.KeyDown += new KeyEventHandler(listBox1_KeyDown);

            foreach (DirectoryInfo dir in library.Root) {
                listBox1.Items.Add(dir.FullName);
            }
        }
        
        #region [ Private ]

        /// <summary>
        /// Handles the KeyDown event of the listBox1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void listBox1_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Delete) {
                List<Object> toRemove = new List<Object>();
                foreach (Object item in listBox1.SelectedItems) {
                    toRemove.Add(item);
                }
                foreach (Object item in toRemove) {
                    listBox1.Items.Remove(item);
                }
            }
            e.Handled = true;
            e.SuppressKeyPress = true;
        }

        /// <summary>
        /// Handles the Click event of the mAddDirectoryButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void mAddDirectoryButton_Click(object sender, EventArgs e) {
            var dialog = new FolderBrowserDialog();
            if (dialog.ShowDialog() == DialogResult.OK) {
                listBox1.Items.Add(dialog.SelectedPath);
            }
        }

        /// <summary>
        /// Handles the Click event of the mSaveButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void mSaveButton_Click(object sender, EventArgs e) {
            Library.Root.Clear();
            foreach (Object dir in listBox1.Items) {
                try {
                    Library.Root.Add(new DirectoryInfo(dir.ToString()));
                }
                catch (Exception ex) {
                }
            }
            this.Close();
        }

        /// <summary>
        /// Private property hold the Library for which this configuration
        /// dialog is associated with.
        /// </summary>
        /// <value>
        /// The Library.
        /// </value>
        private Library Library { get; set; }
        
        #endregion
    
    }
}
