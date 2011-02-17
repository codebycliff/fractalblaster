using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Windows.Forms;

namespace FractalBlaster.Universe {

    public class AppContext {


        public IEngine Engine { get; set; }

        public IEnumerable<IPlugin> DefaultPlugins { get; set; }

        public IEnumerable<IPlugin> Plugins { get; set; }

        public AppSettingsReader Settings { get; set; }
    }

}
