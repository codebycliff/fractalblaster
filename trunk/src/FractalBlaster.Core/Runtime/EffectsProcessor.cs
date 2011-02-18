using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using FractalBlaster.Universe;

namespace FractalBlaster.Core.Runtime {
    
    class EffectsProcessor : IInputPlugin {

        public IEnumerable<IEffectPlugin> Effects { get; private set; }
        
        public EffectsProcessor(IInputPlugin input) {
            InputStream = input;
        }
        
        #region [ IInputPlugin ]

        public void Initialize(AppContext context) {
            Context = context;
            Effects = Context.Plugins.OfType<IEffectPlugin>();
        }

        public void OpenMedia(MediaFile media) {
            InputStream.OpenMedia(media);
        }

        public void CloseMedia() {
            InputStream.CloseMedia();
        }

        public void SeekBeginning() {
            InputStream.SeekBeginning();
        }

        public MemoryStream ReadFrames(int numFramesToRead) {
            MemoryStream frames = InputStream.ReadFrames(numFramesToRead);
            foreach (IEffectPlugin effect in Effects.Where(e=>e.Enabled)) {
                frames.Seek(0, 0);
                frames = effect.ProcessStream(frames);
            }
            frames.Seek(0, 0);
            return frames;
        }
        
        #endregion

        #region [ Private ]

        public AppContext Context { get; private set; }
        public IInputPlugin InputStream { get; private set; }
        
        #endregion
    }

}
