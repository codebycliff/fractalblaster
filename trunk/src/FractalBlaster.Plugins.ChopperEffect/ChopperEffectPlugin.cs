using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FractalBlaster.Universe;
using System.IO;
using System.Windows.Forms;

namespace FractalBlaster.Plugins.ChopperEffect {

    public class ChopperEffectPlugin : IEffectPlugin, IViewPlugin {

        public AppContext Context { get; private set; }

        public void ChangeEffect(Int32 i) {
            Effect = i;
        }
        
        #region [ IPlugin ]

        public static IPlugin Instance { 
            get{
                if (instance == null) {
                    instance = new ChopperEffectPlugin();
                    instance.UI = new ChopperEffectUI();
                }
                return instance;
            }
        }

        public String Author { get { return "Fractal Blasters"; } }

        public Version Version { get { return new Version(); } }

        public String Id { get { return this.GetType().Assembly.FullName; } }

        public void Initialize(AppContext context) {
            Context = context;
        }

        #endregion

        #region [ IViewPlugin ]

        public IContainerControl UserInterface { get { return UI; } }
        
        #endregion
        
        #region [ IEffectPlugin ]
        
        public MemoryStream ProcessStream(MemoryStream stream) {
            long l = stream.Length;
            if (Effect != 0) {
                for (int i = 0; i < l; i++) {
                    if ((i % Effect) > Effect / 2)
                        stream.WriteByte(0);
                    else
                        stream.ReadByte();
                }
            }
            stream.Seek(0, System.IO.SeekOrigin.Begin);
            UI.lockBuffer();
            UI.rewindBuffer();
            for (int b = 0; b != -1; b = stream.ReadByte()) {
                UI.addByte((byte)b);
            }
            UI.rewindBuffer();
            UI.unlockBuffer();
            return stream;
        }
        
        #endregion

        #region [ Private ]

        private static ChopperEffectPlugin instance { get; set; }
        private ChopperEffectUI UI { get; set; }
        private Int32 Effect { get; set; }

        #endregion

    }
}
