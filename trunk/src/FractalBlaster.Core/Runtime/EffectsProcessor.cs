using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using FractalBlaster.Universe;

namespace FractalBlaster.Core.Runtime {
    
    class EffectsProcessor : IInputPlugin {

        public AppContext Context { get; private set; }

        public IEnumerable<IEffectPlugin> Effects { get; private set; }

        public IInputPlugin InputStream { get; private set; }

        public EffectsProcessor(IInputPlugin input, AppContext ctx) {
            InputStream = input;
            Effects = ctx.Plugins.OfType<IEffectPlugin>();
        }

        public MediaFile OpenMedia(String path) {
            return InputStream.OpenMedia(path);
        }

        public void CloseMedia() {
            InputStream.CloseMedia();
        }

        public void SeekBeginning() {
            InputStream.SeekBeginning();
        }

        public MemoryStream ReadFrames(int numFramesToRead) {
            MemoryStream frames = InputStream.ReadFrames(numFramesToRead);
            foreach (IEffectPlugin effect in Effects) {
                frames.Seek(0, 0);
                frames = effect.ProcessStream(frames);
            }
            frames.Seek(0, 0);
            return frames;
        }

        public void Initialize(AppContext context) {
            Context = context;
        }

        public string Author {
            get { return "Fractal Blaster"; }
        }

        public Version Version {
            get { return new Version(); }
        }

        public string Id {
            get { return this.GetType().Assembly.FullName; }
        }
    }

}
