using System;
using System.IO;
using FractalBlaster.Core.IO;

namespace FractalBlaster.Core {

    public interface IEngine {

        MediaFile CurrentMedia { get;  }

        IInputPlugin InputPlugin { get; set; }

        IOutputPlugin OutputPlugin { get; set; }

        IDecoderPlugin DecoderPlugin { get; set; }

        void LoadMedia(MediaFile file);

        void UnloadMedia();

    }

}