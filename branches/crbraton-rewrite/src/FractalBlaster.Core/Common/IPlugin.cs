using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FractalBlaster.Core {
    
    public interface IPlugin {

        String Author { get; }

        Version Version { get; }

        IEnumerable<Type> SupportedEngineTypes { get; }

    }

}
