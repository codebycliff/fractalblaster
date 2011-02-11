using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FractalBlaster.Core {
    
    public interface IProductModel {

        String Name { get; }

        IEnumerable<Type> EngineTypes { get; }

        IContainerControl DefaultUI { get; }

        Boolean AllowsCustomEngines { get; }

        // REMOVED: SettingsBase Preferences { get; }

        // REMOVED: ResourceSet Bundle { get; }

    }

}
