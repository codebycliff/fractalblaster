using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FractalBlaster.Core.UI {

    /// <remarks>
    /// Base class that can be extended to provide configuration options
    /// for a collection view.
    /// </remarks>
    public partial class ConfigurationDialog : Form {

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigurationDialog"/> class.
        /// </summary>
        public ConfigurationDialog() {
            InitializeComponent();
            mCancelButton.Click += (o, a) => { this.Close(); };
        }

    }

}
