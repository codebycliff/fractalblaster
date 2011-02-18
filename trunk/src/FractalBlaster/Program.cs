using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using FractalBlaster.Core.Runtime;
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
            //FamilyRuntime.FamilyKernel.Log = log4net.LogManager.GetLogger(typeof(Program));

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            FamilyKernel.Instance.LoadProduct(new Products.StandardProductModel());
            if (!FamilyKernel.Instance.IsProductLoaded) {
                throw new Exception("Couldn't load product.");
            }
            Application.Run(FamilyKernel.Instance.BuildContext());

        }

    }
}
