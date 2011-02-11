using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FractalBlaster.Core;
using FractalBlaster.Core.IO;
using FractalBlaster.Engines;

namespace FractalBlaster.Plugins.Taglib {
    
    public class TaglibAudioMetadata : IMetadataPlugin {

        public string Author {
            get { return "Fractal Blasters"; }
        }

        public Version Version {
            get { return new Version(0, 1); }
        }

        public IEnumerable<Type> SupportedEngineTypes {
            get { return new Type[] { typeof(AudioEngine) }; }
        }

        public IEnumerable<MediaProperty> Analyze(MediaFile file) {
            
            AudioMetadata meta = RetrieveMetadata(file.FileInfo.FullName);

            List<MediaProperty> props = new List<MediaProperty>();
            props.Add(MediaProperty.CreateProperty("Album", meta.Album, meta.Album.GetType()));
            props.Add(MediaProperty.CreateProperty("Artist", meta.Artist, meta.Artist.GetType()));
            props.Add(MediaProperty.CreateProperty("BitRate", meta.BitRate, meta.BitRate.GetType()));
            props.Add(MediaProperty.CreateProperty("Channels", meta.Channels, meta.Channels.GetType()));
            props.Add(MediaProperty.CreateProperty("Codec", meta.Codec, meta.Codec.GetType()));
            props.Add(MediaProperty.CreateProperty("Duration", meta.Duration, meta.Duration.GetType()));
            props.Add(MediaProperty.CreateProperty("SampleRate", meta.SampleRate, meta.SampleRate.GetType()));
            props.Add(MediaProperty.CreateProperty("Title", meta.Title, meta.Title.GetType()));
            props.Add(MediaProperty.CreateProperty("Duration", meta.TrackNumber, meta.TrackNumber.GetType()));
            props.Add(MediaProperty.CreateProperty("Duration", meta.Year, meta.Year.GetType()));

            return props;
        }

        #region [ Legacy ]

        public static AudioMetadata RetrieveMetadata(String filename) {
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
            a.TrackNumber = (int)file.Tag.Track;
            a.Year = (int)file.Tag.Year;

            return a;
        }
 
        #endregion

    }
}
