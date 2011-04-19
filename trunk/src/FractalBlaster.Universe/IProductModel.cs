using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FractalBlaster.Universe;

namespace FractalBlaster.Universe {

    /// <remarks>
    /// Interface the specifies the variabilities among product implementations.
    /// </remarks>
    public interface IProductModel {

        /// <summary>
        /// Gets the name of the product.
        /// </summary>
        String Name { get; }

        /// <summary>
        /// Gets the product's supported file extensions.
        /// </summary>
        IEnumerable<String> SupportedFileExtensions { get; }

        /// <summary>
        /// Gets the default user interface for the product.
        /// </summary>
        IContainerControl DefaultUI { get; }

    }

}
