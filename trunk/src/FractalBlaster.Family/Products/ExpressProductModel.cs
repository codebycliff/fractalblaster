using System;
using System.Windows.Forms;
using System.Configuration;
using System.Resources;
using FractalBlaster.Core;
using FractalBlaster.Core.UI;
using System.Collections.Generic;
using FractalBlaster.Universe;

namespace FractalBlaster.Family.Products {

    public class ExpressProductModel : IProductModel {

        public String Name {
            get { return "Express";  }
        }

        public IContainerControl DefaultUI {
            get { return new ContainerControl(); }
        }

        public IEnumerable<string> SupportedFileExtensions {
            get { return new String[] { "mp3" }; }
        }

    }

}