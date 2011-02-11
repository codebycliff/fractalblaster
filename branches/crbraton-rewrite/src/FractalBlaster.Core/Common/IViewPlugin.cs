using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FractalBlaster.Core {

    public interface IViewPlugin {

        IContainerControl UserInterface { get; }

        IEnumerable<Type> SupportedEngineTypes { get; }

    }

}
