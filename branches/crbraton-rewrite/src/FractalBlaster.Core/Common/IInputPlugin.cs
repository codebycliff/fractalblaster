using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FractalBlaster.Core.IO;

namespace FractalBlaster.Core {

    public interface IInputPlugin {

        MediaFile OpenMedia(String path);

        void CloseMedia();

    }
}
