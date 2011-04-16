using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FractalBlaster.Core.UI {
    public partial class ConfigurationDialog : Form {

        public ConfigurationDialog() {
            InitializeComponent();

            mCancelButton.Click += (o, a) => { this.Close(); };
            
            
        }
    }
}
