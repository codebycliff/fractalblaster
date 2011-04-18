using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FractalBlaster.Universe;

namespace FractalBlaster.Plugins.AboutPlugin
{
    [PluginAttribute(Name = "About", Author = "Fractal Blasters", Description = "About us")]
    public class AboutPlugin : IViewPlugin
    {
        AboutForm form;

        public AboutPlugin()
        {
            form = new AboutForm();
        }

        public System.Windows.Forms.Form UserInterface
        {
            get { return form; }
        }

        public void Initialize(AppContext context)
        {
        }
    }
}
