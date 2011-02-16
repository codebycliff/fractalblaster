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

        void Initialize(AppContext context);
    }

}
