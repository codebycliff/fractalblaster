using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using FamilyRuntime = FractalBlaster.Core.Runtime;
using FractalBlaster.Universe;
namespace FractalBlaster.Family {

    static class Program {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            IProductModel product = new Products.StandardProductModel();
            FamilyRuntime.Application.LoadProduct(product);
            Application.Run(FamilyRuntime.Application.Start());
        }
    }
}
