using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace Generator
{
    class Program
    {
        static void Main(string[] args)
        {
            GeneratorForm gf = new GeneratorForm();
            System.Windows.Forms.Application.Run(gf);
            Process configProcess = new Process();
            configProcess.StartInfo.FileName = "Config.exe";
            configProcess.StartInfo.WorkingDirectory = "bin";
            configProcess.Start();
            configProcess.WaitForExit();
            FileInfo info = new FileInfo("bin/config.ini");
            info.CopyTo("Fractal Blasters/config.ini", true);
        }
    }
}
