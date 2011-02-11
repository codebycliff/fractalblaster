using System;
using System.IO;
using FractalBlaster.Core.IO;

namespace FractalBlaster.Core {
    
    public interface IDecoderPlugin {
        
        void Open(MediaFile file);
        
        void Close();
        
        void SeekBeginning();
        
        MemoryStream ReadFrame(Int32 numFramesToRead);

    }

}