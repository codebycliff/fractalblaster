using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FractalBlaster.Universe {

    /// <remarks>
    /// Interface the specifies the methods and properties that need to 
    /// be implemented by any runtime kernel.
    /// </remarks>
    public interface IRuntimeKernel {

        /// <summary>
        /// Gets the product model being used.
        /// </summary>
        IProductModel Product { get; }

        /// <summary>
        /// Gets a value indicating whether a product model is currently loaded.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if a product model is loaded; otherwise, <c>false</c>.
        /// </value>
        Boolean IsProductLoaded { get; }

        /// <summary>
        /// Loads the specified product model.
        /// </summary>
        /// <param name="model">The model.</param>
        void LoadProduct(IProductModel model);

        /// <summary>
        /// Builds the Windows Form's ApplicationContext to be used to run
        /// the application.
        /// <seealso cref="System.Windows.Forms.ApplicationContext"/>
        /// </summary>
        /// <returns></returns>
        ApplicationContext BuildContext();
        
    }

}
