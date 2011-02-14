using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FractalBlaster.Universe {
    
    public interface IRuntimeKernel {

        IRuntimeKernel Instance { get;  }

        IEnumerable<IPlugin> GetPlugins();

        IEnumerable<IPlugin> GetPlugins(Type type);

        IPlugin GetDefaultPlugin(Type type);
    }

}
