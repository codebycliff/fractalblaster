using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FractalBlaster.Universe {

    /// <remarks>
    /// Defines the members that must be implemented for all plugins that 
    /// wish to provide an additional user interface element / control.
    /// </remarks>
    public interface IViewPlugin : IPlugin {

        /// <summary>
        /// The main user interface represented as an <see cref=
        /// "System.Windows.Forms.IContainerControl"/> implementation. The
        /// control returned from this property will be added to a dialog
        /// and can be shown or hidden via the 'Views' menu in the main
        /// user interface.
        /// </summary>
        IContainerControl UserInterface { get; }

    }

}
