using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FractalBlaster.Universe {
    
    public interface IRuntimeKernel {

        IProductModel Product { get; }

        Boolean IsProductLoaded { get; }

        void LoadProduct(IProductModel model);

        ApplicationContext BuildContext();
        
    }

}
