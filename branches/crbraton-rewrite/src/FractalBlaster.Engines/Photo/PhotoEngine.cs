using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FractalBlaster.Core;

namespace FractalBlaster.Engines {

    public class PhotoEngine : IEngine {


        public static PhotoEngine Instance {
            get {
                if (mInstance == null) {
                    mInstance = new PhotoEngine();
                }
                return mInstance;
            }
        }

        public Core.IO.MediaFile CurrentMedia {
            get { throw new NotImplementedException(); }
        }

        public IInputPlugin InputPlugin {
            get {
                throw new NotImplementedException();
            }
            set {
                throw new NotImplementedException();
            }
        }

        public IOutputPlugin OutputPlugin {
            get {
                throw new NotImplementedException();
            }
            set {
                throw new NotImplementedException();
            }
        }

        public IDecoderPlugin DecoderPlugin {
            get {
                throw new NotImplementedException();
            }
            set {
                throw new NotImplementedException();
            }
        }

        public void LoadMedia(Core.IO.MediaFile file) {
            throw new NotImplementedException();
        }

        public void UnloadMedia() {
            throw new NotImplementedException();
        }

        public static PhotoEngine mInstance;
    }

}
