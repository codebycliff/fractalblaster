using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FractalBlaster.Universe;
using System.IO;
using System.Windows.Forms;

namespace FractalBlaster.Plugins.ChopperEffect {

    [PluginAttribute(Name="Chopper Effect", Author="Fractal Blasters", Description="Chops stuff")]
    public class ChopperEffectPlugin : IEffectPlugin, IViewPlugin {

        public AppContext Context { get; private set; }

        public void ChangeEffect(Int32 i) {
            Effect = i;
        }
        
        #region [ IPlugin ]

        public ChopperEffectPlugin() {
            UI = new ChopperEffectUI();
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

        public void ProcessStream(MemoryStream stream) {
            long l = stream.Length;
            if (Effect != 0) {
                for (int i = 0; i < l; i++) {
                    if ((i % Effect) > Effect / 2)
                        stream.WriteByte(0);
                    else
                        stream.ReadByte();
                }
            }
        }
        
        #endregion

        #region [ Private ]

        private static ChopperEffectPlugin instance { get; set; }
        private ChopperEffectUI UI { get; set; }
        private Int32 Effect { get; set; }

        #endregion

    }
}
