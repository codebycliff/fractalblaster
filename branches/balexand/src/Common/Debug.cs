using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace FractalBlaster.Universe
{
    static public class Debug
    {
        static DebugForm myForm;
        static Debug()
        {
            myForm = new DebugForm();
            myForm.Show();
        }

        static public void print(string s)
        {
            myForm.print(s);
            TextWriter logOutput = new StreamWriter("debugLog.txt", true);
            logOutput.Write(s);
            logOutput.Close();
        }

        static public void printline(string s)
        {
            myForm.printline(s);
            TextWriter logOutput = new StreamWriter("debugLog.txt", true);
            logOutput.WriteLine(s);
            logOutput.Close();
        }
    }
}
