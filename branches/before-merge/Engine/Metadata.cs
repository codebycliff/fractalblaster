using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;

using System.Collections;

namespace Engine
{
    public class Metadata
    {
        public static Dictionary<String, Type> TAGS = new Dictionary<String, Type>();

        static Metadata()
        {
            TAGS.Add("Album", Type.GetType("System.String"));
            TAGS.Add("Artist", Type.GetType("System.String"));
            TAGS.Add("BitRate", Type.GetType("System.Int32"));
            TAGS.Add("Channels", Type.GetType("System.Int32"));
            TAGS.Add("Codec", Type.GetType("System.String"));
            TAGS.Add("Duration", Type.GetType("System.TimeSpan"));
            TAGS.Add("SampleRate", Type.GetType("System.Int32"));
            TAGS.Add("Title", Type.GetType("System.String"));
            TAGS.Add("TrackNum", Type.GetType("System.Int32"));
            TAGS.Add("Year", Type.GetType("System.Int32"));
            TAGS.Add("File", Type.GetType("System.String"));
        }

        public static AudioMetadata RetrieveMetadata(string filename)
        {
            AudioMetadata a = new AudioMetadata();
            TagLib.File file = TagLib.File.Create(filename);

            a.Album = file.Tag.Album;
            a.Artist = file.Tag.FirstPerformer;
            a.BitRate = file.Properties.AudioBitrate;
            a.Channels = file.Properties.AudioChannels;
            a.Codec = file.Properties.Codecs.ElementAt(0).Description;
            a.Duration = file.Properties.Duration;
            a.SampleRate = file.Properties.AudioSampleRate;
            a.Title = file.Tag.Title;
            a.TrackNum = (int)file.Tag.Track;
            a.Year = (int)file.Tag.Year;
            a.File = filename;

            return a;
        }
    }
}
