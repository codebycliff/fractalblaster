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
        /// The author of the plugin.
        /// </summary>
        String Author { get; }

        /// <summary>
        /// The current version of the plugin.
        /// </summary>
        Version Version { get; }

        /// <summary>
        /// The unique id for the plugin. A typical implementation of this
        /// property is to return the Assembly's full name. For example:
        /// <code>
        /// class CustomPlugin : IViewPlugin {
        ///     public String Id {
        ///         get {
        ///             return this.GetType().Assembly.FullName;
        ///         }
        ///     }
        ///     // Rest of implementation ...
        /// }
        /// </code>
        /// </summary>
        String Id { get; }

        /// <summary>
        /// Method where initialization operations are performed. The engine
        /// the plugin will being running under is passed as a parameter.
        /// This allows for the plugin to register itself with the 
        /// appropriate event handlers defined in the <see cref=
        /// "FractalBlaster.Universe.IEngine"/> interface.
        /// </summary>
        /// <param name="engine">
        /// The engine the plugin will be running under.
        /// </param>
        /// <returns>
        /// The instance of the IPlugin after initialization.
        /// </returns>
        IPlugin Initialize(IEngine engine);

    }

}
