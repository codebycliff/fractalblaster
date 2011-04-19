using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Windows.Forms;

namespace FractalBlaster.Universe {

    /// <remarks>
    /// Class containing properties representing a limited view of the current 
    /// application context.
    /// </remarks>
    public class AppContext {

        /// <summary>
        /// Gets or sets the engine.
        /// </summary>
        /// <value>
        /// The engine.
        /// </value>
        public IEngine Engine { get; set; }

        /// <summary>
        /// Gets or sets the default plugins.
        /// </summary>
        /// <value>
        /// The default plugins.
        /// </value>
        public IEnumerable<IPlugin> DefaultPlugins { get; set; }

        /// <summary>
        /// Gets or sets all plugins.
        /// </summary>
        /// <value>
        /// All plugins.
        /// </value>
        public IEnumerable<IPlugin> AllPlugins { get; set; }

        /// <summary>
        /// Gets or sets the app settings reader.
        /// </summary>
        /// <value>
        /// The app settings reader.
        /// </value>
        public AppSettingsReader Settings { get; set; }

    }

}
