using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using System.Resources;
using FractalBlaster.Core;
using FractalBlaster.Runtime;

namespace FractalBlaster.Products {
    
    public class EnterpriseProductModel : IProductModel {

        public EnterpriseProductModel() {
            mEngineTypes = new List<Type> () {
                typeof(AudioEngine),
                typeof(VideoEngine),
                typeof(PhotoEngine)
            };
        }

        public String Name {
            get { return "Enterprise";  }
        }

        public  IEnumerable<Type> EngineTypes {
            get { return mEngineTypes; } 
        }

        public IContainerControl DefaultUI {
            get { return new ContainerControl(); }
        }

        public Boolean AllowsCustomEngines {
            get { return true; }
        }

        public Boolean AddCustomEngine(Type engine) {
            if (AllowsCustomEngines) {
                mEngineTypes.Add(engine);
                return true;
            }
            return false;
        }

        #region [ Private ]

        private List<Type> mEngineTypes;
        
        #endregion

    }

}
