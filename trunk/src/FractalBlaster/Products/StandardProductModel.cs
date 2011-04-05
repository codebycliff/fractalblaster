using System;
using FractalBlaster.Core;
using FractalBlaster.Core.UI;
using System.Windows.Forms;
using System.Configuration;
using FractalBlaster.Core.Runtime;
using System.Resources;
using FractalBlaster.Universe;
using System.Collections.Generic;

namespace FractalBlaster.Products {

    public class StandardProductModel : IProductModel {

        public String Name {
            get { return "Standard"; }
        }

        public IContainerControl DefaultUI { get { return new ContainerControl(); } }

        public IEnumerable<string> SupportedFileExtensions {
            get { return new String[] { "mp3", "mp4" }; }
        }
    }

}
