using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Xml;
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
                DataTable ArtistTable = MediaCollection.Columns["Artist"].Table;
                DataRow[] quer = ArtistTable.Select();
                int validCounter = 0;
                for (int i = 0; i < quer.Length; i++)
                {
                    if (quer[i] != null)
                    {
                        validCounter++;
                    }
                }
                String[] dataout = new String[validCounter];
                int dataoutCurLen = 0;

                for (int i = 0; i < quer.Length; i += 1)
                {
                    if (quer[i] != null && !dataout.Contains(quer[i]["Artist"]))
                    {
                        try
                        {
                            dataout[dataoutCurLen] = (String)quer[i]["Artist"];
                            dataoutCurLen++;
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Source);
                            Console.WriteLine(e.Message);
                            Console.WriteLine(e.StackTrace);
                        }
                    }
                }

                String[] rval = new String[dataoutCurLen];
                Array.Copy(dataout, rval, dataoutCurLen);

                return rval;
            }
        }

        /// <summary>
        /// A list of the titles of all different albums this Library contains.
        /// </summary>
        public IEnumerable<String> Albums { 
            get
            {
                DataTable AlbumTable = MediaCollection.Columns["Album"].Table;
                DataRow[] quer = AlbumTable.Select();
                int validCounter = 0;
                for (int i = 0; i < quer.Length; i++)
                {
                    if (quer[i] != null)
                    {
                        validCounter++;
                    }
                }
                String[] dataout = new String[validCounter];
                int dataoutCurLen = 0;

                for (int i = 0; i < quer.Length; i += 1)
                {
                    if (quer[i] != null && !dataout.Contains(quer[i]["Album"]))
                    {
                        dataout[dataoutCurLen] = (String)quer[i]["Album"];
                        dataoutCurLen++;
                    }
                }

                String[] rval = new String[dataoutCurLen];
                Array.Copy(dataout, rval, dataoutCurLen);

                return rval;
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
                DataRow[] validSongs = new DataRow[MediaCollection.Rows.Count];
                int i = 0;
                foreach(DataRow row in MediaCollection.Rows){
                    if (row["Artist"].Equals(key))
                    {
                        validSongs[i] = row;
                        i++;
                    }
                }
                DataRow[] SongsbyArtist = new DataRow[i];
                for (int j = 0; j < i; j++)
                {
                    SongsbyArtist[j] = validSongs[j];
                }

                //DataRow[] SongsbyArtist = MediaCollection.Select("Artist = '" + key + "'", "Album DESC");

                String currentname = "";
                List<MediaFile> collect = null;

                for (i = 0; i < SongsbyArtist.Length; i += 1)
                {
                    String album = (String)SongsbyArtist[i]["Album"];
                    String artist = (String)SongsbyArtist[i]["Artist"];


                    if (dataout.Keys.Contains(album) && SongsbyArtist[i] != null && SongsbyArtist[i]["File"] != null && 
                        !SongsbyArtist[i].Equals(System.DBNull.Value) && !SongsbyArtist[i]["File"].Equals(System.DBNull.Value))
                    {
                        dataout[album].Add((MediaFile)SongsbyArtist[i]["File"]);
                    }
                    else
                    {
                        dataout.Add(album, new List<MediaFile>());
                        if (SongsbyArtist[i] != null && SongsbyArtist[i]["File"] != null && !SongsbyArtist[i].Equals(System.DBNull.Value) 
                            && !SongsbyArtist[i]["File"].Equals(System.DBNull.Value))
                        {
                            dataout[album].Add((MediaFile)SongsbyArtist[i]["File"]);
                        }//Else don't add
                    }
                }
                return dataout;
            }
        }

        /// <summary>
        /// Serializes the Library into an XML file.
        /// </summary>
        public void Save() {
            /*XmlSerializer serializer = new XmlSerializer(typeof(Library));
            using (XmlWriter stream = XmlWriter.Create(File.OpenWrite(Root.FullName + FileName)))
            {
                serializer.Serialize(stream, this);
            }*/
        }

        /// <summary>
        /// Tries to load/reload all of the mp3 files in the system defined
        /// music folder into the Library.
        /// </summary>
        public void Refresh()
        {
            FileInfo[] files = new FileInfo[0];
            
            foreach(String s in Config.getProperty("fileformats").Split(';', '|'))
            {
                FileInfo[] temp = Root.GetFiles(s, SearchOption.AllDirectories);
                FileInfo[] temp2 = (FileInfo[])files.Clone();
                files = new FileInfo[temp.Length + files.Length];
                Array.Copy(temp, files, temp.Length);
                Array.Copy(temp2, 0, files, temp.Length, temp2.Length);
            }

            

            Parallel.ForEach(files, file =>
            {
                if (!MediaPaths.Contains(file.FullName))
                {
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

                        bool exists = false;
                        lock (MediaCollection)
                        {
                            foreach (DataRow row in MediaCollection.Rows)
                            {
                                if (row["Title"].Equals(title))
                                {
                                    exists = true;
                                    break;
                                }
                            }
                            if (!exists)
                            {
                                MediaCollection.Rows.Add(dr);
                            }
                        }

                        lock (MediaPaths)
                        {
                            if (!exists)
                            {
                                MediaPaths.Add(file.FullName);
                            }
                        }

                    }
                    catch (Exception e)
                    {
                        Console.Error.WriteLine("Problem Opening MediaFile:\nException Type: {0}\nMessage:{1}", e.GetType().FullName, e.Message);
                    }
                }

            });
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
            DataRow[] validSongs = new DataRow[MediaCollection.Rows.Count];
            int i = 0;
            foreach (DataRow row in MediaCollection.Rows)
            {
                if (row["Artist"].Equals(artist) && row["Album"].Equals(album))
                {
                    validSongs[i] = row;
                    i++;
                }
            }
            DataRow[] SongsbyArtist = new DataRow[i];
            for (int j = 0; j < i; j++)
            {
                SongsbyArtist[j] = validSongs[j];
            }

            return this.GetFiles(SongsbyArtist);

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
            DataRow[] quer;
            try
            {
                quer = MediaCollection.Select(searchexp, sorting);
            }
            catch(Exception e)
            {
                return new MediaFile[0];
            }

            return this.GetFiles(quer);
        }

        #region [ Static ]

        static Library()
        {
            FileName = "Library.xml";
            
        }

        ~Library()
        {
            //this.Save(); -- Removes serialization error
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

        private Library()
        {

        }

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
            DataColumn toAdd = new DataColumn("#", typeof(Int32));
            toAdd.Unique = false;
            output.Columns.Add(toAdd);
            toAdd = new DataColumn("Artist", typeof(String));
            toAdd.Unique = false;
            output.Columns.Add(toAdd);
            toAdd = new DataColumn("Album", typeof(String));
            toAdd.Unique = false;
            output.Columns.Add(toAdd);
            toAdd = new DataColumn("BitRate", typeof(Int32));
            toAdd.Unique = false;
            output.Columns.Add(toAdd);
            toAdd = new DataColumn("Channel", typeof(Int32));
            toAdd.Unique = false;
            output.Columns.Add(toAdd);
            toAdd = new DataColumn("Codec", typeof(String));
            toAdd.Unique = false;
            output.Columns.Add(toAdd);
            toAdd = new DataColumn("Duration", typeof(TimeSpan));
            toAdd.Unique = false;
            output.Columns.Add(toAdd);
            toAdd = new DataColumn("SampleRate", typeof(Int32));
            toAdd.Unique = false;
            output.Columns.Add(toAdd);
            toAdd = new DataColumn("Title", typeof(String));
            toAdd.Unique = false;
            output.Columns.Add(toAdd);
            toAdd = new DataColumn("Year", typeof(Int32));
            toAdd.Unique = false;
            output.Columns.Add(toAdd);
            toAdd = new DataColumn("File", typeof(MediaFile));
            toAdd.Unique = false;
            output.Columns.Add(toAdd);

            output.CaseSensitive = true;
            output.EndInit();

            output.CaseSensitive = true;
            return output;
        }

        private MediaFile[] GetFiles(DataRow[] rows)
        {
            MediaFile[] dataout = new MediaFile[rows.Length];

            for (int i = 0; i < rows.Length; i += 1)
            {
                if (rows[i] != null && rows[i]["File"] != null && !System.DBNull.Value.Equals(rows[i]["File"]))

                {
                    dataout[i] = (MediaFile)rows[i]["File"];
                }
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
