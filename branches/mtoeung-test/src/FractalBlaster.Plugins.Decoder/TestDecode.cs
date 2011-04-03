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
            MediaFile song = new MediaFile("C:\\Users\\Mato-San\\Downloads\\Tank.mp3"); //obviously this is only on my computer
            DecoderInterop dI = new DecoderInterop();                           //From what I understand, this is what I'm doing.
            dI.OpenMedia(song);                                                 //I'm making a decoder, and opening up the mediafile.
            byte[] databuffer = new byte[1024];                                 //I then make a Memorystream to store the data.
            MemoryStream data = new MemoryStream(databuffer);                   //I read it, and while it doesn't return null, it writes it to the output window.
            data = dI.ReadFrames(10);                                           //The way this is set up, can I not just make a main method that uses the decoder class?
            while (data != null)
            {
                Console.WriteLine(data);
            }
            dI.CloseMedia();
        }
    }
}
