using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FractalBlaster.Universe;
using System.IO;
using System.Windows.Forms;

namespace FractalBlaster.Plugins.ChopperEffect {

    public class Receiver : IEffectPlugin, IViewPlugin {
        
        #region [ IPlugin ]

        public static IPlugin Initialize(IEngine engine) {
            Instance = new Receiver();
            Instance.Engine = engine;
            Instance.UI = new Form1();
            return Instance;
        }

        public String Author { get { return "Fractal Blasters"; } }

        public Version Version { get { return new Version(); } }

        public String Id { get { return this.GetType().Assembly.FullName; } }
        
        #endregion

        #region [ IViewPlugin ]

        public IContainerControl UserInterface { get { return UI; } }
        
        #endregion
        
        #region [ IEffectPlugin ]
        
        public void ProcessStream(ref MemoryStream stream) {
            long l = stream.Length;
            if (effect != 0) {
                for (int i = 0; i < l; i++) {
                    if ((i % effect) > effect / 2)
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
        }
        
        #endregion

        public void ChangeEffect(Int32 i) {
            Effect = i;
        }

        private static Receiver Instance { private get; private set; }
        private IEngine Engine { private get; private set; }
        private Int32 Effect { private get; private set; }
        private Form1 UI { private get; private set; }

    }
}
