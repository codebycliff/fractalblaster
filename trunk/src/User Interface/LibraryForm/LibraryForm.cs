using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FractalBlaster.Universe;

namespace FractalBlaster.LibraryForm
{
    public partial class LibraryForm : Form, ILibraryForm
    {
        LibraryCollectionView mLibraryCollectionView;
        ILibrary mLibrary;

        public LibraryForm()
        {
            InitializeComponent();
        }

        #region ILibraryForm Members

        public Form form
        {
            get { return this; }
        }

        public ILibrary library
        {
            set
            {
                mLibrary = value;
                mLibraryCollectionView = new LibraryCollectionView(value);
                this.Controls.Add(mLibraryCollectionView);
                mLibraryCollectionView.Show();
            }
        }

        #endregion
    }
}
