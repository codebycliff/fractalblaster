using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using System.Resources;
using FractalBlaster.Core;
using FractalBlaster.Core.Runtime;
using FractalBlaster.Universe;

namespace FractalBlaster.Products {
    
    public class EnterpriseProductModel : IProductModel {

        public String Name {
            get { return "Enterprise";  }
        }

        public IContainerControl DefaultUI {
            get { return new ContainerControl(); }
        }

        public IEnumerable<string> SupportedFileExtensions {
            get { return new String[] { "*" }; }
        }
    }

}
