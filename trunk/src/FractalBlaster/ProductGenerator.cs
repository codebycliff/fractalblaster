using System;
using System.Collections.Generic;
using FractalBlaster.Core;
using FractalBlaster.Core.UI;
using System.Windows.Forms;

namespace FractalBlaster {

    public class ProductGenerator {

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(String[] args) {
            Application.EnableVisualStyles();
            Application.Run(new ProductSelectionForm());
        }

    }

}