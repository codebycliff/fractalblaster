using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Decoder
{
    public class AudioFile : Common.Decoder
    {

        private string file;
        private FFMPEG.DecoderInterop Decoder;

        public AudioFile(string filepath) : base(filepath)
        {
            file = filepath;
        }

        public override void Open()
        {
            if (!File.Exists(file))
            {
                // Error Log - File doesn't exist
                return;
            }

            // FFMPEG Open
            Decoder = new FFMPEG.DecoderInterop(file);
            if (Decoder.OpenAndAnalyze())
            {
                // Do nothing?
            }
            else
            {
                // Error Log - FFMPEG failed to open the file
                return;
            }

            return;
        }

        public void ReadData()
        {
            /*
            Console.WriteLine(info.Artist + " - " + info.Title);
            Console.WriteLine(info.Album + " - " + info.Year);
            Console.WriteLine("Duration - " + info.Duration / 60 + ":" + ("" + info.Duration % 60).PadLeft(2,'0'));
            Console.WriteLine(info.Channels + " Channel - " + info.SampleRate + " Hz");

            System.IO.FileStream fs = System.IO.File.Create(OUTPUT_FILE);
            while (true)
            {
                byte[] vals = Decoder.RetrieveNextFrame();
                if (vals == null)
                    break;
                fs.Write(vals, 0, vals.Length);
            }
            */
        }

        public override MemoryStream ReadFrame(int numFrames)
        {
            MemoryStream rval = new MemoryStream();
            for (int i = 0; i < numFrames; i++)
            { 
                byte[] vals = Decoder.RetrieveNextFrame();
                if (vals == null)
                {
                    if (rval.Length == 0)
                    {
                        return null;
                    }
                    break;
                }
                rval.Write(vals,0,vals.Length);
            }
            return rval;
        }

        public override void Close()
        {
            Decoder.Close();
        }

        public override void SeekBeginning()
        {
            Decoder.SeekBeginning();
        }

    }
}
