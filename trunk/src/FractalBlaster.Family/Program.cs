using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using FamilyApplication = FractalBlaster.Core.Runtime.Application;

namespace FractalBlaster.Family {

    static class Program {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            FamilyApplication.LoadProduct(new Products.StandardProductModel());
            FamilyApplication.Start();
        }
    }
}
