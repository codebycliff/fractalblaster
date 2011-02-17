using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using FamilyRuntime = FractalBlaster.Core.Runtime;
using FractalBlaster.Universe;
using FractalBlaster.Core;
//using log4net.Config;

namespace FractalBlaster.Family {

    static class Program {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() {

            //BasicConfigurator.Configure();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //FamilyRuntime.FamilyKernel.Log = log4net.LogManager.GetLogger(typeof(Program));
            FamilyRuntime.FamilyKernel.Instance.LoadProduct(new Products.StandardProductModel());
            if (!FamilyRuntime.FamilyKernel.Instance.IsProductLoaded) {
                throw new Exception("Couldn't load product.");
            }

            Application.Run(FamilyRuntime.FamilyKernel.Instance.BuildProduct());

        }

    }
}
