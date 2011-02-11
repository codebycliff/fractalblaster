using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;

namespace Engine
{
    public class Metadata
    {
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

            return a;
        }
    }
}
