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
            configProcess.StartInfo.WorkingDirectory = "Fractal Blasters";
            configProcess.Start();
            configProcess.WaitForExit();

        }
    }
}
