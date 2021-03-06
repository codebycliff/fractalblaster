﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FractalBlaster.Universe;

namespace FractalBlaster.Plugins.Taglib {
    
    [PluginAttribute(Name="TagLib Metadata Plugin", Description="Reads metadata from audio files using TagLib")]
    public class TaglibAudioMetadata : IMetadataPlugin {

        #region [ IMetadataPlugin ]
     
        public IEnumerable<String> SupportedFileExtensions { get { return new String[] {".*"};  } }

        public IEnumerable<MediaProperty> Analyze(MediaFile media) {
            //if (!SupportedFileExtensions.Contains(media.Info.Extension)) {
            //    return new MediaProperty[0];
            //}
            
            TagLib.File file = TagLib.File.Create(media.Info.FullName);
            List<MediaProperty> props = new List<MediaProperty>();
            props.Add(MediaProperty.Create(Metadata.ALBUM, file.Tag.Album, file.Tag.Album.GetType()));
            props.Add(MediaProperty.Create(Metadata.ARTIST, file.Tag.FirstPerformer, file.Tag.FirstPerformer.GetType()));
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
