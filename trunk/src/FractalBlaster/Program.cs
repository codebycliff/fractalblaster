using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using FamilyRuntime = FractalBlaster.Core.Runtime;
using FractalBlaster.Universe;
using FractalBlaster.Core;

namespace FractalBlaster.Family {

    static class Program {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() {

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);


            IRuntimeKernel kernel = FamilyRuntime.FamilyKernel.Instance;
            kernel.LoadProduct(new Products.StandardProductModel());

            if (!kernel.IsProductLoaded) {
                throw new Exception("Couldn't load product.");
            }

            Application.Run(kernel.BuildProduct());

        }

    }
}
