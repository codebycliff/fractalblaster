using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FractalBlaster.Universe {

    /// <summary>
    /// A class representing the metadata that a song will contain.
    /// Primarily this will be from loaded ID3 tags.
    /// </summary>
    public class Metadata : IEnumerable<MediaProperty> {

        #region [ Keys ]

        public const String NOT_AVAILABLE = "N/A";
        public const String ARTIST = "Artist";
        public const String ALBUM = "Album";
        public const String BITRATE = "BitRate";
        public const String CHANNELS = "Channels";
        public const String CODEC = "Codec";
        public const String DURATION = "Duration";
        public const String SAMPLE_RATE = "SampleRate";
        public const String TITLE = "Title";
        public const String TRACK = "Track";
        public const String YEAR = "Year";

        #endregion

        public Metadata() {
            Properties = new List<MediaProperty>();
        }

        public MediaProperty this[String key] {
            get { 
                var props = Properties.Where(p => p.Name.CompareTo(key) == 0);
                if (props.Count() == 0) {
                    return MediaProperty.Create("Not Available", NOT_AVAILABLE, typeof(String));
                }
                return props.First();
            }
            set { Properties.Add(value);  }
        }

        public String Artist { 
            get { 
                Object value = Properties.Where(
                    p => p.Name == Metadata.ARTIST
                ).FirstOrDefault().Value;
                return value == null ? NOT_AVAILABLE : value.ToString();
            } 
        }

        public String Album {
            get {
                Object value = Properties.Where(
                    p => p.Name == Metadata.ALBUM
                ).FirstOrDefault().Value;
                return value == null ? NOT_AVAILABLE : value.ToString();
            }
        }

        public String Title {
            get {
                Object value = Properties.Where(
                    p => p.Name == Metadata.TITLE
                ).FirstOrDefault().Value;
                return value == null ? NOT_AVAILABLE : value.ToString();
            }
        }

        public Int32 Track {
            get {
                Object value = Properties.Where(
                    p => p.Name == Metadata.TRACK
                ).FirstOrDefault().Value;
                Int32 track;
                if(Int32.TryParse(value.ToString(), out track)) {
                    return track;
                }
                return -1;
            }
        }

        public Int32 BitRate {
            get {
                Object value = Properties.Where(
                    p => p.Name == Metadata.BITRATE
                ).FirstOrDefault().Value;
                Int32 bitrate;
                if(Int32.TryParse(value.ToString(), out bitrate)) {
                    return bitrate;
                }
                return -1;
            }
        }

        public TimeSpan Duration {
            get {
                Object value = Properties.Where(
                    p => p.Name == Metadata.DURATION
                ).FirstOrDefault().Value;
                TimeSpan duration;
                if(TimeSpan.TryParse(value.ToString(), out duration)) {
                    return duration;
                }
                return TimeSpan.Zero;
            }
        }

        public Int32 Channels {
            get {
                Object value = Properties.Where(
                    p => p.Name == Metadata.CHANNELS
                ).FirstOrDefault().Value;
                Int32 channels;
                if(Int32.TryParse(value.ToString(), out channels)) {
                    return channels;
                }
                return -1;
            }
        }

        public Int32 SampleRate {
            get {
                Object value = Properties.Where(
                    p => p.Name == Metadata.SAMPLE_RATE
                ).FirstOrDefault().Value;
                Int32 samplerate;
                if(Int32.TryParse(value.ToString(), out samplerate)) {
                    return samplerate;
                }
                return -1;
            }
        }

        public Int32 Year {
            get {
                Object value = Properties.Where(
                    p => p.Name == Metadata.SAMPLE_RATE
                ).FirstOrDefault().Value;
                Int32 year;
                if(Int32.TryParse(value.ToString(), out year) ){
                    return year;
                }
                return -1;
            }
        }

        public IEnumerable<String> Keys { get { return Properties.Select(p => p.Name); } }

        public IEnumerable<String> Values { get { return Properties.Select(p => p.Value.ToString()); } }

        public Boolean ContainsKey(String key) {
            return Properties.Any(p => p.Name.CompareTo(key) == 0);
        }

        #region [ IEnumerable ]

        public IEnumerator<MediaProperty> GetEnumerator() {
            return Properties.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return Properties.GetEnumerator();
        }
        
        #endregion
        
        #region [ Private ]

        private List<MediaProperty> Properties { get; set; }
        
        #endregion
    
    }
}
