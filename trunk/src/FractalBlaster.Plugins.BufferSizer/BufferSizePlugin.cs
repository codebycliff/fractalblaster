using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FractalBlaster.Universe;
using FractalBlaster;
using System.IO;
using System.Windows.Forms;

namespace FractalBlaster.Plugins.BufferSizer
{
    [PluginAttribute(Name = "Buffer Sizer", Author = "Fractal Blasters", Description = "Adjusts Buffer Size")]
    public class BufferSizePlugin : IEffectPlugin, IViewPlugin 
    {
        public AppContext Context { get; private set; }

        public void ChangeEffect(Int32 i) {
            GlobalVariables.BufferSize = i;
        }
        
        #region [ IPlugin ]

        public BufferSizePlugin()
        {
            UI = new BufferSizeUI();
            UI.setReciever(this);
        }
        
        public void Initialize(AppContext context) {
            Context = context;
        }

        #endregion

        #region [ IViewPlugin ]

        public Form UserInterface { get { return UI; } }

        #endregion

        #region [ IEffectPlugin ]

        public Boolean Enabled { get; set; }

        public void ProcessStream(MemoryStream stream)
        {
            //Recall that this plugin doesn't modify memory stream data
        }

        #endregion

        #region [ Private ]

        private static BufferSizePlugin instance { get; set; }
        private BufferSizeUI UI { get; set; }
        private Int32 Effect { get; set; }

        #endregion
    }
}
