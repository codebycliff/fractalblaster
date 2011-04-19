using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Xna.Framework.Graphics;

namespace FractalBlaster.Plugins.BasicVisualizer
{
    /// <summary>
    /// Form to display visualizer art.
    /// </summary>
    public partial class VisualizerForm : Form
    {
        /// <summary>
        /// The plugin to which this form belongs.
        /// </summary>
        private BasicVisualizerPlugin _owner;

        /// <summary>
        /// Accessor for VisualizerGraphicsControl object.
        /// </summary>
        public VisualizerGraphicsControl GraphicsControl
        {
            get { return this.visualizerGraphicsControl1;}
        }

        /// <summary>
        /// Constructs a new VisualizerForm.
        /// </summary>
        /// <param name="owner">The plugin to which this form belongs.</param>
        public VisualizerForm(BasicVisualizerPlugin owner)
        {
            _owner = owner;
            InitializeComponent();
            this.Shown += new EventHandler(VisualizerForm_Shown);
        }

        private void VisualizerForm_Shown(object sender, EventArgs e)
        {
            this.ShowInTaskbar = true;
        }

        /// <summary>
        /// Unfinished, does nothing.
        /// </summary>
        /// <param name="SongTitle"></param>
        public void DisplaySongTitle(string SongTitle)
        {
        }

        private void VisualizerForm_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Overrides form closing event.
        /// </summary>
        /// <param name="e">FormClosingEventArgs provides information about the event.</param>
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            //If we're shutting down windows...
            if (e.CloseReason == CloseReason.WindowsShutDown) return;

            //Else, just hide it.
            this.Hide();
            _owner.Enabled = false;
            e.Cancel = true;
        }

    }
}
