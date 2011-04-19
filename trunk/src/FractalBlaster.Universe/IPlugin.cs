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

        /// <summary>
        /// Initializes the plugin with the specified application context.
        /// </summary>
        /// <param name="context">The context.</param>
        void Initialize(AppContext context);

    }

    /// <remarks>
    /// Attribute-derived class that should be used on all plugin 
    /// implementations that provides basic information about the plugin.
    /// </remarks>
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class PluginAttribute : System.Attribute {

        /// <summary>
        /// Gets or sets the author of the plugin.
        /// </summary>
        /// <value>
        /// The author.
        /// </value>
        public String Author { get; set; }

        /// <summary>
        /// Gets or sets the name of the plugin.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public String Name { get; set; }

        /// <summary>
        /// Gets or sets the description of the plugin.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public String Description { get; set; }

        /// <summary>
        /// Gets or sets the version of the plugin.
        /// </summary>
        /// <value>
        /// The version.
        /// </value>
        public String Version { get; set; }
    
    }

}
