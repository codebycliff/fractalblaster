using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FractalBlaster.Universe
{
    public interface ILibraryForm : IPlugin
    {
        Form form
        {
            get;
        }
    }
}
