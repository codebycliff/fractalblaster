using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FractalBlaster.Universe {
    
    public interface IRuntimeKernel {

        IEngine Engine { get; }

        IEnumerable<IPlugin> GetPlugins();

        IEnumerable<IPlugin> GetPlugins(Type type);

        IPlugin GetDefaultPlugin(Type type);
    }

}
