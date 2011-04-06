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

        static IProductModel Product { get; set; }
        
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(String[] args) {

            //BasicConfigurator.Configure();
            //FamilyRuntime.FamilyKernel.Log = log4net.LogManager.GetLogger(typeof(Program));

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(ParseArguments(args));

        }

        static ApplicationContext ParseArguments(String[] args) {
                
            foreach (String arg in args) {
                if (arg.CompareTo("--generator") == 0 || arg.CompareTo("-g") == 0) {
                    ApplicationContext context = new ApplicationContext(new ProductSelectionForm());
                    return context;
                }
            }

            Product = new Products.StandardProductModel();
            FamilyKernel.Instance.LoadProduct(Product);
            if (!FamilyKernel.Instance.IsProductLoaded) {
                throw new Exception("Couldn't load product.");
            }
            return FamilyKernel.Instance.BuildContext();
        }

    }
}
