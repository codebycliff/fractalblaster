using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FractalBlaster.Plugins.AboutPlugin
{
    public partial class AboutForm : Form
    {
        public AboutForm()
        {
            InitializeComponent();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            //If we're shutting down windows...
            if (e.CloseReason == CloseReason.WindowsShutDown) return;

            //Else, just hide it.
            this.Hide();
            e.Cancel = true;
        }
    }
}
