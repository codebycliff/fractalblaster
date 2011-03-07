using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FractalBlaster.Universe {
    
    /// <remarks>
    /// Base interface that defines the members for which all derived plugins
    /// must implement at the bare minimum.
    /// </remarks>
    public interface IPlugin {

        void Initialize();

    }

    [AttributeUsage(AttributeTargets.Class)]
    public sealed class PluginAttribute : System.Attribute {
        public String Author { get; set; }
        public String Name { get; set; }
        public String Description { get; set; }
        public String Version { get; set; }
    }

}
