using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FractalBlaster.Core;
using FractalBlaster.Core.IO;

namespace FractalBlaster.Engines {

    public class AudioEngine : IEngine {

        public static AudioEngine Instance {
            get {
                if (mInstance == null) {
                    mInstance = new AudioEngine();
                }
                return mInstance;
            }
        }

        public MediaFile CurrentMedia {
            get { return mMediaFile; }
        }

        public IInputPlugin InputPlugin { get; set; }

        public IOutputPlugin OutputPlugin { get; set; }
        
        public IDecoderPlugin DecoderPlugin { get; set; }

        public void LoadMedia(MediaFile file) {
            mMediaFile = file;
            
        }

        public void UnloadMedia() {
            mMediaFile = null;
        }


        private static AudioEngine mInstance;
        private MediaFile mMediaFile;
        
    }

}
