using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FractalBlaster.Universe;

namespace FractalBlaster.PlaylistForm
{
    public partial class PlaylistForm : Form, IPlaylistForm
    {
        public PlaylistForm()
        {
            InitializeComponent();
        }

        public Form form
        {
            get { return this; }
        }

    }
}
