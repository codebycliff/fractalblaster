using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using System.Data;
using FractalBlaster.Universe;

namespace FractalBlaster.Universe {

    [Serializable]
    public class Library {
        
        /// <summary>
        /// The directory that contains the songs this Library was loaded from.
        /// </summary>
        public DirectoryInfo Root { get; private set; }

        /// <summary>
        /// The number of songs this Library contains.
        /// </summary>
        public Int32 ItemCount { get { return MediaCollection.Rows.Count; } }

        /// <summary>
        /// A list of the names of all different artists this Library contains.
        /// </summary>
        public IEnumerable<String> Artists {
            get
            {
                DataRow[] quer = MediaCollection.DefaultView.ToTable(true, "Artist").Select();
                String[] dataout = new String[quer.Length];

                for (int i = 0; i < quer.Length; i += 1)
                {
                    dataout[i] = (String)quer[i]["Artist"];
                }

                return dataout;
            }
        }

        /// <summary>
        /// A list of the titles of all different albums this Library contains.
        /// </summary>
        public IEnumerable<String> Albums { 
            get 
            {
                DataRow[] quer = MediaCollection.DefaultView.ToTable(true, "Album").Select();

                String[] dataout = new String[quer.Length];

                for (int i = 0; i < quer.Length; i += 1)
                {
                    dataout[i] = (String)quer[i]["Album"];
                }

                return dataout;
            }
        }

        /// <summary>
        /// A list of all songs contained in this Library, sorted by when they were added.
        /// </summary>
        public IEnumerable<MediaFile> AllMedia { 
            get
            {
                DataRow[] quer = MediaCollection.Select();

                return GetFiles(quer);
            }
        }


        /// <summary>
        /// A method to accomodate the tree view structure of viewing the Library.
        /// </summary>
        /// <param name="key">The name of the artist to search for.</param>
        /// <returns>
        /// Returns a Dictionary containing a mapping of all album names by
        /// the given artist to a list of the songs in that album.
        /// </returns>
        public Dictionary<String, List<MediaFile>> this[String key] {
            get
            {
                Dictionary<String, List<MediaFile>> dataout = new Dictionary<string, List<MediaFile>>();

                DataRow[] SongsbyArtist = MediaCollection.Select("Artist = '" + key + "'", "Album DESC");
                String currentname = "";
                List<MediaFile> collect = null;

                for(int i = 0; i < SongsbyArtist.Length; i += 1)
                {
                    if(!currentname.Equals(SongsbyArtist[i]["Album"]))
                    {
                        if (!currentname.Equals("")) { dataout.Add(currentname, collect); };
                        
                        currentname = (String) SongsbyArtist[i]["Album"];
                        collect = new List<MediaFile>();
                    }
                    collect.Add((MediaFile) SongsbyArtist[i]["File"]);
                }

                dataout.Add(currentname, collect);

                return dataout;
            }
        }

        /// <summary>
        /// Serializes the Library into an XML file.
        /// </summary>
        public void Save() {
            XmlSerializer serializer = new XmlSerializer(typeof(Library));
            using (Stream stream = File.OpenWrite(Root.FullName + FileName)) {
                serializer.Serialize(stream, this);
            }
        }

        /// <summary>
        /// Tries to load/reload all of the mp3 files in the system defined
        /// music folder into the Library.
        /// </summary>
        public void Refresh()
        {
            FileInfo[] files = Root.GetFiles("*.mp3", SearchOption.AllDirectories);

            foreach (FileInfo file in files)
            {
                if (MediaPaths.Contains(file.FullName))
                {
                    continue;
                }
                else
                {
                    MediaPaths.Add(file.FullName);
                    try
                    {
                        MediaFile media = file.CreateMediaFile();
                        String artist = media.Metadata.Artist;
                        String album = media.Metadata.Album;
                        String title = media.Metadata.Title;

                        int bitrate = media.Metadata.BitRate;
                        int channel = media.Metadata.Channels;
                        int samplerate = media.Metadata.SampleRate;
                        int year = media.Metadata.Year;

                        TimeSpan duration = media.Metadata.Duration;

                        DataRow dr = MediaCollection.NewRow();


                        //Apostrophes mess with searching and sorting, so we just strip them out
                        artist = artist.Replace("'", "");
                        album = album.Replace("'", "");
                        title = title.Replace("'", "");

                        dr["Artist"] = artist;
                        dr["Album"] = album;
                        dr["Title"] = title;
                        dr["BitRate"] = bitrate;
                        dr["Channel"] = channel;
                        dr["SampleRate"] = samplerate;
                        dr["Year"] = year;
                        dr["Duration"] = duration;
                        dr["File"] = media;

                        MediaCollection.Rows.Add(dr);
                    }
                    catch (Exception e)
                    {
                        Console.Error.WriteLine("Problem Opening MediaFile:\nException Type: {0}\nMessage:{1}", e.GetType().FullName, e.Message);
                    }
                }

            }
        }

        /// <summary>
        /// Searches the Library for a list of all songs from a given artist.
        /// </summary>
        /// <param name="artist">The name of an Artist</param>
        /// <returns>A list of all songs who's ID3 tags have the given artist name.</returns>
        public IEnumerable<MediaFile> MediaForArtist(String artist)
        {
            return this.SearchStrict("Artist", artist, "ASC");
        }

        /// <summary>
        /// Searches the Library for a list of all songs from a given artist's album.
        /// </summary>
        /// <param name="artist">The name of an Artist.</param>
        /// <param name="album">The title of an album</param>
        /// <returns>A list of all the songs on the given album from the given artist</returns>
        public IEnumerable<MediaFile> MediaForAlbum(String artist, String album)
        {
            DataRow[] quer = MediaCollection.Select("Artist = '" + artist + "' AND Album = '" + album + "'");

            return this.GetFiles(quer);
        }

        /// <summary>
        /// Lists all the songs contained in this Library in ascending order for a given ID3 tag.
        /// </summary>
        /// <param name="columnname">The name of an ID3 tag by which to sort.</param>
        /// <returns>A list of all songs sorted in ascending order by given tag.</returns>
        public IEnumerable<MediaFile> Sort(String columnname)
        {
            return this.Sort(columnname, "ASC");
        }

        /// <summary>
        /// Lists all the songs contained in this Library by a given order and tag.
        /// </summary>
        /// <param name="columnname">The name of an ID3 tag by which to sort.</param>
        /// <param name="order">Either ASC for ascending order or DESC for descending.</param>
        /// <returns>A list of all songs sorted in the given order by the given tag.</returns>
        public IEnumerable<MediaFile> Sort(String columnname, String order)
        {
            DataRow[] quer = MediaCollection.Select("", columnname + " " + NormalizeOrder(order));

            return this.GetFiles(quer);
        }

        /// <summary>
        /// Searches all songs over a given ID3 tag for those containing a given search term.
        /// </summary>
        /// <param name="columnname">The ID3 tag that will contain the search term.</param>
        /// <param name="searchterm">The term you wish to search for.</param>
        /// <returns>A list of all songs that have the given search term somewhere in the ID3 tag given, sorted in ascending order.</returns>
        public IEnumerable<MediaFile> Search(String columnname, String searchterm)
        {
            return this.Search(columnname, searchterm, "ASC");
        }

        /// <summary>
        /// Searches all songs over a given ID3 tag for those containing a given search term.
        /// </summary>
        /// <param name="columnname">The ID3 tag that will contain the search term.</param>
        /// <param name="searchterm">The term you wish to search for.</param>
        /// <param name="order">Either ASC for ascending order or DESC for descending.</param>
        /// <returns>A list of all songs that have the given search term somewhere in the ID3 tag given, sorted in the given order.</returns>
        public IEnumerable<MediaFile> Search(String columnname, String searchterm, String order)
        {
            String searchexp = columnname + " LIKE '%" + searchterm + "%'";
            String sorting = columnname + " " + NormalizeOrder(order);

            DataRow[] quer = MediaCollection.Select(searchexp, sorting);

            return this.GetFiles(quer);
        }

        /// <summary>
        /// Searches all songs over a given ID3 tag for those which match a given search term.
        /// </summary>
        /// <param name="columnname">The ID3 tag that will contain the search term.</param>
        /// <param name="searchterm">The term you wish to search for.</param>
        /// <returns>A list of all songs whose given ID3 tag are equal to the search term</returns>
        public IEnumerable<MediaFile> SearchStrict(String columnname, String searchterm)
        {
            return this.SearchStrict(columnname, searchterm, "ASC");
        }

        /// <summary>
        /// Searches all songs over a given ID3 tag for those which match a given search term.
        /// </summary>
        /// <param name="columnname">The ID3 tag that will contain the search term.</param>
        /// <param name="searchterm">The term you wish to search for.</param>
        /// <param name="order">Either ASC for ascending order or DESC for descending.</param>
        /// <returns>A list of all songs whose given ID3 tag are equal to the search term, sorted in the given order.</returns>
        public IEnumerable<MediaFile> SearchStrict(String columnname, String searchterm, String order)
        {

            String searchexp = columnname + " = '" + searchterm + "'";
            String sorting = columnname + " " + NormalizeOrder(order);

            DataRow[] quer = MediaCollection.Select(searchexp, sorting);

            return this.GetFiles(quer);
        }


        #region [ Static ]

        static Library()
        {
            FileName = "Library.xml";
            
        }

        public static String FileName { get; private set; }

        /// <summary>
        /// Groups all files in a directory together into a Library.
        /// </summary>
        /// <param name="dir">A directory containing music files or a serialized library file</param>
        /// <returns>A library containing all the music in the given directory.</returns>
        public static Library Load(DirectoryInfo dir) {
            Library library = null;

            String libraryConfig = Path.Combine(dir.FullName, FileName);
            if (File.Exists(libraryConfig)) {
                XmlSerializer deserializer = new XmlSerializer(typeof(Library));
                using (Stream stream = File.OpenRead(dir.FullName)) {
                    library = (Library)deserializer.Deserialize(stream);
                }
            }
            else {
                library = new Library(dir);
                
            }
            return library;
        }

        #endregion

        #region [ Private ]

        private Library(DirectoryInfo root) {
            Root = root;

            MediaCollection = this.createTable();

            if (File.Exists(Root.FullName + FileName)) {
                File.Delete(Root.FullName + FileName);
            }
            MediaPaths = new List<String>();
        }

        private DataTable MediaCollection { get; set; }

        private DataTable createTable()
        {
            DataTable output = new DataTable();


            /*
             * Eventually replace this with a global list of supported ID3 tags
             * So as to reduce the "magic number" nature of this code.
             */

            output.Columns.Add("#",         typeof(Int32));
            output.Columns.Add("Artist",    typeof(String));
            output.Columns.Add("Album",     typeof(String));
            output.Columns.Add("BitRate",   typeof(Int32));
            output.Columns.Add("Channel",   typeof(Int32));
            output.Columns.Add("Codec",     typeof(String));
            output.Columns.Add("Duration",  typeof(TimeSpan));
            output.Columns.Add("SampleRate",typeof(Int32));
            output.Columns.Add("Title",     typeof(String));
            output.Columns.Add("Year",      typeof(Int32));
            output.Columns.Add("File",      typeof(MediaFile));

            return output;
        }

        private MediaFile[] GetFiles(DataRow[] rows)
        {
            MediaFile[] dataout = new MediaFile[rows.Length];

            for (int i = 0; i < rows.Length; i += 1)
            {
                dataout[i] = (MediaFile)rows[i]["File"];
            }

            return dataout;
        }

        private String NormalizeOrder(String order)
        {
            if (!order.Equals("ASC") && !order.Equals("DESC"))
            {
                return "ASC";
            }

            return order;
        }

        private List<String> MediaPaths { get; set; }

        #endregion    
    }
}
