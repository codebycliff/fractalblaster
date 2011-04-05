using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FractalBlaster.Universe;

namespace FractalBlaster.Universe {
    
    public interface IProductModel {

        String Name { get; }

        IEnumerable<String> SupportedFileExtensions { get; }

        IContainerControl DefaultUI { get; }

    }

}
