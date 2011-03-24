using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
        }

        static public void printline(string s)
        {
            myForm.printline(s);
        }

    }
}
