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
            DirectoryInfo sourceDirectory = new DirectoryInfo("bin");
            DirectoryInfo destinationDirectory = Directory.CreateDirectory("Fractal Blasters");

            foreach (FileInfo f in sourceDirectory.GetFiles())
            {
                File.Copy(f.FullName, destinationDirectory.FullName + "\\" + f.Name, true);
            }

            sourceDirectory = new DirectoryInfo("bin\\Plugins");
            destinationDirectory = Directory.CreateDirectory("Fractal Blasters\\Plugins");

            foreach (FileInfo f in sourceDirectory.GetFiles())
            {
                File.Copy(f.FullName, destinationDirectory.FullName + "\\" + f.Name, true);
            }

            Process configProcess = new Process();
            configProcess.StartInfo.FileName = "Config.exe";
            configProcess.StartInfo.WorkingDirectory = "Fractal Blasters";
            configProcess.Start();
            configProcess.WaitForExit();
        }
    }
}
