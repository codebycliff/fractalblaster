using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FractalBlaster.Universe;

namespace FractalBlaster.Plugins.Taglib {
    
    [PluginAttribute(Name="TagLib Metadata Plugin", Description="Reads metadata from audio files using TagLib")]
    public class TaglibAudioMetadata : IMetadataPlugin {

        public AppContext Context { get; private set; }

        #region [ IMetadataPlugin ]
     
        public void Initialize(AppContext context) {
            Context = context;
        }

        public IEnumerable<String> SupportedFileExtensions { get { return new String[] {".*"};  } }

        public IEnumerable<MediaProperty> Analyze(MediaFile media) {
            if (!SupportedFileExtensions.Contains(media.Info.Extension)) {
                return new MediaProperty[0];
            }
            
            TagLib.File file = TagLib.File.Create(media.Info.FullName);
            List<MediaProperty> props = new List<MediaProperty>();
            props.Add(MediaProperty.Create("Album", file.Tag.Album, file.Tag.Album.GetType()));
            props.Add(MediaProperty.Create("Artist", file.Tag.FirstPerformer, file.Tag.FirstPerformer.GetType()));
            props.Add(MediaProperty.Create("BitRate", file.Properties.AudioBitrate, file.Properties.AudioBitrate.GetType()));
            props.Add(MediaProperty.Create("Channels", file.Properties.AudioChannels, file.Properties.AudioChannels.GetType()));
            props.Add(MediaProperty.Create("Codec", file.Properties.Codecs.First().Description, file.Properties.Codecs.First().Description.GetType()));
            props.Add(MediaProperty.Create("Duration", file.Properties.Duration, file.Properties.Duration.GetType()));
            props.Add(MediaProperty.Create("SampleRate", file.Properties.AudioSampleRate, file.Properties.AudioSampleRate.GetType()));
            props.Add(MediaProperty.Create("Title", file.Tag.Title, file.Tag.Title.GetType()));
            props.Add(MediaProperty.Create("Duration", (Int32)file.Tag.Track, typeof(Int32)));
            props.Add(MediaProperty.Create("Duration", (Int32)file.Tag.Year, typeof(Int32)));

            return props;
        }

        #endregion

    }

}
