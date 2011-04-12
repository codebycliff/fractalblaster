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
            //if (!SupportedFileExtensions.Contains(media.Info.Extension)) {
            //    return new MediaProperty[0];
            //}
            
            TagLib.File file = TagLib.File.Create(media.Info.FullName);
            List<MediaProperty> props = new List<MediaProperty>();
            if (file.Tag.Album == null)
                file.Tag.Album = "-";

            if (file.Tag.Title == null)
                file.Tag.Title = media.Info.Name;

            string performer;
            if (file.Tag.FirstPerformer == null)
                performer = "-";
            else
                performer = file.Tag.FirstPerformer;

            props.Add(MediaProperty.Create(Metadata.ALBUM, file.Tag.Album, file.Tag.Album.GetType()));
            props.Add(MediaProperty.Create(Metadata.ARTIST, performer, performer.GetType()));
            props.Add(MediaProperty.Create(Metadata.BITRATE, file.Properties.AudioBitrate, file.Properties.AudioBitrate.GetType()));
            props.Add(MediaProperty.Create(Metadata.CHANNELS, file.Properties.AudioChannels, file.Properties.AudioChannels.GetType()));
            props.Add(MediaProperty.Create(Metadata.CODEC, file.Properties.Codecs.First().Description, file.Properties.Codecs.First().Description.GetType()));
            props.Add(MediaProperty.Create(Metadata.DURATION, file.Properties.Duration, file.Properties.Duration.GetType()));
            props.Add(MediaProperty.Create(Metadata.SAMPLE_RATE, file.Properties.AudioSampleRate, file.Properties.AudioSampleRate.GetType()));
            props.Add(MediaProperty.Create(Metadata.TITLE, file.Tag.Title, file.Tag.Title.GetType()));
            props.Add(MediaProperty.Create(Metadata.TRACK, (Int32)file.Tag.Track, typeof(Int32)));
            props.Add(MediaProperty.Create(Metadata.YEAR, (Int32)file.Tag.Year, typeof(Int32)));

            return props;
        }

        #endregion

    }

}
