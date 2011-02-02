using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace FractalBlaster
{
    public class AudioFile
    {

        const string OUTPUT_FILE = @"C:\Output.raw";
        const string INPUT_FILE = @"C:\1.mp3";


        private string file;
        private FFMPEG.DecoderInterop Decoder;
        public AudioMetadata info;
       
        public AudioFile(string filepath)
        {
            file = filepath;
            
        }

        public bool Open()
        {
            if (!File.Exists(file))
            {
                // Error Log - File doesn't exist
                return false;
            }

            // FFMPEG Open
            Decoder = new FFMPEG.DecoderInterop(file);
            if (Decoder.OpenAndAnalyze())
            {
                info = Decoder.RetrieveMetadata();
            }
            else
            {
                // Error Log - FFMPEG failed to open the file
                return false;
            }

            return true;
        }

        public void ReadData()
        {

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
        }

        public MemoryStream ReadFrames(int numFrames)
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

        public static void Main(String[] args)
        {
            AudioFile a = new AudioFile(INPUT_FILE);
            if (a.Open())
            {
                a.ReadData();
            }
        }
        
    }

    public struct AudioMetadata
    {
        public string Title;
        public string Artist;
        public string Album;
        public int Year;
        public int Duration;
        public int Channels;
        public int SampleRate;   
    };
}
