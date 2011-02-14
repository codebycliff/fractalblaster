using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;

namespace FractalBlaster.Universe {

    /// <remarks>
    /// Class that represents a list (or collection) of media files and 
    /// provides operations for working with the collection as a whole,
    /// including serialization support.
    /// </remarks>
    public class Playlist {

        /// <summary>
        /// Enumeration of media files that represents the playlist's items.
        /// </summary>
        public IEnumerable<MediaFile> Items { get { return MediaList.AsEnumerable();  } }

        /// <summary>
        /// Constructor that creates an empty playlist initialized with an
        /// empty collection of media files.
        /// </summary>
        public Playlist() {
            MediaData = new DataTable();

            DataColumn dc;
            foreach (KeyValuePair<String, Type> pair in new AudioMetadata().GetType().GetFields().Select(i =>
                new KeyValuePair<String,Type>(i.Name, i.FieldType))
                ) {
                dc = new DataColumn();

                dc.ColumnName = pair.Key;
                dc.DataType = pair.Value;

                MediaData.Columns.Add(dc);
            }
        }

        /// <summary>
        /// Method for adding the specified media file to the collection of
        /// media files that this playlist represents.
        /// </summary>
        /// <param name="media">
        /// The media file to be added to this playlist.
        /// </param>
        public void AddItem(MediaFile media) {
            MediaList.Add(media);
            MediaData.Rows.Add(CreateRow(media));
        }

        /// <summary>
        /// Method that serializes this playlist to an xml file with the name 
        /// "library.xml".
        /// </summary>
        public void Serialize() {
            MediaData.WriteXml("library.xml");
        }

        /// <summary>
        /// Looks for an xml file in the current directory with the name 
        /// "library.xml" and attempts to deserialize it.
        /// </summary>
        public void Deserialize() {
            if (File.Exists("library.xml")) {
                MediaData.ReadXml("library.xml");
            }
        }

        /// <summary>
        /// Method that returns an enumeration of <see cref="AudioMetadata"/>
        /// for each media file in this playlist, optionally shuffling the
        /// order of the media files.
        /// </summary>
        /// <param name="shuffle">
        /// Whether or not the result enumeration should be shuffled.
        /// </param>
        /// <returns>
        /// An enumeration of AudioMetadata representing this playlist.
        /// </returns>
        public IEnumerable<AudioMetadata> GetEntries(Boolean shuffle) {
            DataTable table = MediaData.Clone();

            if (shuffle) {
                table.Columns.Add(new DataColumn("RandomNum", Type.GetType("System.Int32")));

                Random random = new Random();

                for (int i = 0; i < table.Rows.Count; i += 1) {
                    table.Rows[i]["RandomNum"] = random.Next(1, table.Rows.Count);
                }

                table.DefaultView.Sort = "RandomNum";
            }

            DataRow[] songs = MediaData.Select();

            AudioMetadata[] output = new AudioMetadata[songs.Length];

            for (int i = 0; i < output.Length; i += 1) {
                output[i] = this.AudioMetadataFromRow(songs[i]);
            }

            return output;
        }

        #region [ Private ]

        /// <summary>
        /// DataTable containing the metadata for each media file in this 
        /// playlist.
        /// </summary>
        private DataTable MediaData { get; set; }

        /// <summary>
        /// List of media files contained in this playlist.
        /// </summary>
        private List<MediaFile> MediaList { get; set; }

        /// <summary>
        /// Private helper method that creates a <see cref="DataRow"/> from
        /// the specified MediaFile.
        /// </summary>
        /// <param name="media">
        /// The media file for which the resulting data row should represent.
        /// </param>
        /// <returns>
        /// DataRow representing the specified media file.
        /// </returns>
        private DataRow CreateRow(MediaFile media) {
            DataRow row = MediaData.NewRow();
            foreach (MediaProperty prop in media.Metadata) {
                row[prop.Name] = prop.Value;
            }
            row["MediaFile"] = media;
            return row;
        }

        /// <summary>
        /// Private helper that returns an AudioMetadata structure that the
        /// specified data row represents.
        /// </summary>
        /// <param name="row">
        /// The data row containing the audio metdata that is to be returned.
        /// </param>
        /// <returns>
        /// Audio metadata structure representing the specified row.
        /// </returns>
        private AudioMetadata AudioMetadataFromRow(DataRow row) {
            return new AudioMetadata() {
                Artist = (String)row["Artist"],
                Title = (String)row["Title"],
                Album = (String)row["Album"],
                Codec = (String)row["Codec"],
                Path = (String)row["Path"],
                TrackNumber = (int)row["TrackNumber"],
                Year = (int)row["Year"],
                Channels = (int)row["Channels"],
                SampleRate = (int)row["SampleRate"],
                BitRate = (int)row["BitRate"],
                Duration = (TimeSpan)row["Duration"],
            };
        }
        
        #endregion    
         
    }

}
