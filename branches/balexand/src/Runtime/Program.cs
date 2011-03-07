using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
//using FractalBlaster.Core.Runtime;
using FractalBlaster.Universe;
using FractalBlaster.PlaybackControlForm;
//using FractalBlaster.Core;
//using log4net.Config;

namespace FractalBlaster.Runtime {

    static class Program {
        
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(String[] args) {

            //BasicConfigurator.Configure();
            //FamilyRuntime.FamilyKernel.Log = log4net.LogManager.GetLogger(typeof(Program));

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new PlaybackControlForm.PlaybackControlForm());
        }
    }
}
