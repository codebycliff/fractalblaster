using System;
using FractalBlaster.Core;
using FractalBlaster.Core.UI;
using System.Windows.Forms;
using System.Configuration;
using FractalBlaster.Runtime;
using System.Resources;
using System.Collections.Generic;

namespace FractalBlaster.Products {

    public class StandardProductModel : IProductModel {

        public String Name {
            get { return "Standard"; }
        }

        public IContainerControl DefaultUI { get { return new ContainerControl(); } }

        public  IEnumerable<Type> EngineTypes { get { return new Type[] { typeof(AudioEngine), typeof(VideoEngine), typeof(PhotoEngine) }; } }

        public Boolean AllowsCustomEngines { get { return false; } }

    }

}
