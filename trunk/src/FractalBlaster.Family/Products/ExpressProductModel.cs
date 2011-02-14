using System;
using System.Windows.Forms;
using System.Configuration;
using System.Resources;
using FractalBlaster.Core;
using FractalBlaster.Core.UI;
using System.Collections.Generic;

namespace FractalBlaster.Products {

    public class ExpressProductModel : IProductModel {

        public ExpressProductModel(IEngine engine) {
            Engine = engine;
        }

        public String Name {
            get { return "Express";  }
        }

        public IEnumerable<Type> EngineTypes {
            get { return new Type[] { Engine.GetType() }; }
        }

        public IContainerControl DefaultUI {
            get { return new ContainerControl(); }
        }

        public bool AllowsCustomEngines {
            get { return false; }
        }

        public IEngine Engine { get; set; }

    }

}