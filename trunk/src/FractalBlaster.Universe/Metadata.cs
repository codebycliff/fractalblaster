using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FractalBlaster.Universe {

    /// <remarks>
    /// A class representing the metadata that a song will contain.
    /// Primarily this will be from loaded ID3 tags.
    /// </remarks>
    public class Metadata : IEnumerable<MediaProperty> {

        #region [ Constants ]

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

        /// <summary>
        /// Initializes a new instance of the <see cref="Metadata"/> class.
        /// </summary>
        public Metadata() {
            Properties = new List<MediaProperty>();
        }

        /// <summary>
        /// Gets or sets the <see cref="FractalBlaster.Universe.MediaProperty"/> with the specified key.
        /// </summary>
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

        /// <summary>
        /// Gets the artist.
        /// </summary>
        public String Artist { 
            get { 
                Object value = Properties.Where(
                    p => p.Name == Metadata.ARTIST
                ).FirstOrDefault().Value;
                return value == null ? NOT_AVAILABLE : value.ToString();
            } 
        }

        /// <summary>
        /// Gets the album.
        /// </summary>
        public String Album {
            get {
                Object value = Properties.Where(
                    p => p.Name == Metadata.ALBUM
                ).FirstOrDefault().Value;
                return value == null ? NOT_AVAILABLE : value.ToString();
            }
        }

        /// <summary>
        /// Gets the title.
        /// </summary>
        public String Title {
            get {
                Object value = Properties.Where(
                    p => p.Name == Metadata.TITLE
                ).FirstOrDefault().Value;
                return value == null ? NOT_AVAILABLE : value.ToString();
            }
        }

        /// <summary>
        /// Gets the track.
        /// </summary>
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

        /// <summary>
        /// Gets the bit rate.
        /// </summary>
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

        /// <summary>
        /// Gets the duration.
        /// </summary>
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

        /// <summary>
        /// Gets the channels.
        /// </summary>
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

        /// <summary>
        /// Gets the sample rate.
        /// </summary>
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

        /// <summary>
        /// Gets the year.
        /// </summary>
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

        /// <summary>
        /// Gets the metadata keys.
        /// </summary>
        public IEnumerable<String> Keys { get { return Properties.Select(p => p.Name); } }

        /// <summary>
        /// Gets the metadata values.
        /// </summary>
        public IEnumerable<String> Values { get { return Properties.Select(p => p.Value.ToString()); } }

        /// <summary>
        /// Determines whether the metadata contains the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>
        ///   <c>true</c> if the specified key contains key; otherwise, <c>false</c>.
        /// </returns>
        public Boolean ContainsKey(String key) {
            return Properties.Any(p => p.Name.CompareTo(key) == 0);
        }

        #region [ IEnumerable ]

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Collections.Generic.IEnumerator`1"/> that can be used to iterate through the collection.
        /// </returns>
        public IEnumerator<MediaProperty> GetEnumerator() {
            return Properties.GetEnumerator();
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerator"/> object that can be used to iterate through the collection.
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator() {
            return Properties.GetEnumerator();
        }
        
        #endregion
        
        #region [ Private ]

        /// <summary>
        /// Gets or sets the list of metadata items represented as <see cref="MediaProperty"/> instances.
        /// </summary>
        /// <value>
        /// The properties.
        /// </value>
        private List<MediaProperty> Properties { get; set; }
        
        #endregion
    
    }
}
