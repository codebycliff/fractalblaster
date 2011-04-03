using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;
using System.Security;
using FractalBlaster.Universe;
using FractalBlaster.Plugins.Decoder.FFMPEG;

namespace FractalBlaster.Plugins.Decoder
{
    class TestDecode
    {
        public static void main()
        {
            MediaFile song = new MediaFile("C:\\Users\\Mato-San\\Downloads\\Tank.mp3");
            DecoderInterop dI = new DecoderInterop();
            dI.OpenMedia(song);
            byte[] databuffer = new byte[1024];
            MemoryStream data = new MemoryStream(databuffer);
            data = dI.ReadFrames(20);
            while (data != null)
            {
                Console.WriteLine(data);
            }
            dI.CloseMedia();
        }
    }
}
