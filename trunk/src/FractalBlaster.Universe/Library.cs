using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml;
using FractalBlaster.Universe;

namespace FractalBlaster.Universe
{

    [Serializable]
    public class Library
    {

        /// <summary>
        /// The directory that contains the songs this Library was loaded from.
        /// </summary>
        public ICollection<DirectoryInfo> Root
        {
            get { return root; }
            private set { root = value; }
        }

        private ICollection<DirectoryInfo> root;

        /// <summary>
        /// The number of songs this Library contains.
        /// </summary>
        public Int32 ItemCount { get { return MediaCollection.Rows.Count; } }

        /// <summary>
        /// A list of the names of all different artists this Library contains.
        /// </summary>
        public IEnumerable<String> Artists
        {
            get
            {
                DataTable ArtistTable = MediaCollection.Columns["Artist"].Table;
                DataView view = new DataView(ArtistTable);
                ArtistTable = view.ToTable(true, "Artist");
                string[] rval = new string[ArtistTable.Rows.Count];
                for (int i = 0; i < ArtistTable.Rows.Count; i++)//DataRow row in ArtistTable.Rows)
                {
                    rval[i] = (string)ArtistTable.Rows[i]["Artist"];
                }
                return rval;
                /*
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
                */
            }
        }

        /// <summary>
        /// A list of the titles of all different albums this Library contains.
        /// </summary>
        public IEnumerable<String> Albums
        {
            get
            {
                DataTable AlbumTable = MediaCollection.Columns["Album"].Table;
                DataView view = new DataView(AlbumTable);
                AlbumTable = view.ToTable(true, "Album");

                string[] rval = new string[AlbumTable.Rows.Count];
                for (int i = 0; i < AlbumTable.Rows.Count; i++)
                {
                    rval[i] = (string)AlbumTable.Rows[i]["Album"];
                }
                return rval;

                /*
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
                 */
            }
        }

        /// <summary>
        /// A list of all songs contained in this Library, sorted by when they were added.
        /// </summary>
        public IEnumerable<MediaFile> AllMedia
        {
            get
            {
                DataRow[] quer = MediaCollection.Select();

                return GetFiles(quer);
            }
        }

        public class AlbumCollection : List<Album>
        {
        }

        public class Album : List<MediaFile>
        {
            public string AlbumName
            {
                get;
                private set;
            }

            public Album(string AlbumName)
            {
                this.AlbumName = AlbumName;
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
        public AlbumCollection this[String key]
        {
            get
            {
                var albumsToSongs = new Dictionary<string, List<MediaFile>>();

                DataRow[] SongsbyArtist = MediaCollection.Select("Artist = '" + key + "'");

                for (int i = 0; i < SongsbyArtist.Length; i++)
                {
                    String album = (String)SongsbyArtist[i]["Album"];
                    MediaFile media = (MediaFile)SongsbyArtist[i]["File"];

                    if (!albumsToSongs.ContainsKey(album))
                    {
                        albumsToSongs[album] = new List<MediaFile>();
                    }

                    albumsToSongs[album].Add(media);
                }

                AlbumCollection albums = new AlbumCollection();
                foreach (KeyValuePair<string, List<MediaFile>> a in albumsToSongs)
                {
                    Album album = new Album(a.Key);
                    album.AddRange(a.Value);
                    albums.Add(album);
                }
                return albums;
            }
        }

        /// <summary>
        /// Tries to load/reload all of the mp3 files in the system defined
        /// music folder into the Library.
        /// </summary>
        public void Refresh()
        {
            FileInfo[] files = new FileInfo[0];

            Console.WriteLine("Reading Files");
            foreach (String s in Config.getProperty("fileformats").Split(';', '|'))
            {
                foreach (DirectoryInfo dir in Root)
                {
                    FileInfo[] temp = dir.GetFiles(s, SearchOption.AllDirectories);
                    FileInfo[] temp2 = (FileInfo[])files.Clone();
                    files = new FileInfo[temp.Length + files.Length];
                    Array.Copy(temp, files, temp.Length);
                    Array.Copy(temp2, 0, files, temp.Length, temp2.Length);
                }
            }
            Console.WriteLine("Scanning Files");
            MediaPaths.Clear();
            MediaCollection.Clear();
            foreach (FileInfo file in files)
            {
                //if (!MediaPaths.Contains(file.FullName))
                //{
                    try
                    {
                        MediaFile media = file.CreateMediaFile();

                        if (media != null)
                        {
                            String fullName = file.FullName;
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

                            dr["FullName"] = fullName;
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
                            MediaPaths.Add(file.FullName);
                        }
                    }
                    catch (Exception e)
                    {
                        Console.Error.WriteLine("Problem Opening MediaFile:\nException Type: {0}\nMessage:{1}", e.GetType().FullName, e.Message);
                    }
                }
            //}
        }

        /// <summary>
        /// Searches the Library for a list of all songs from a given artist.
        /// </summary>
        /// <param name="artist">The name of an Artist</param>
        /// <returns>A list of all songs who's ID3 tags have the given artist name.</returns>
        public IEnumerable<MediaFile> MediaForArtist(String artist)
        {
            string searchExpression = "Artist = '" + artist + "'";
            return GetFiles(MediaCollection.Select(searchExpression));
            //return this.SearchStrict("Artist", artist, "ASC");
        }

        /// <summary>
        /// Searches the Library for a list of all songs from a given artist's album.
        /// </summary>
        /// <param name="artist">The name of an Artist.</param>
        /// <param name="album">The title of an album</param>
        /// <returns>A list of all the songs on the given album from the given artist</returns>
        public IEnumerable<MediaFile> MediaForAlbum(String artist, String album)
        {
            string searchExpression = "Artist = '" + artist + "' " +
                                      "AND Album = '" + album + "'";
            DataRow[] SongsbyArtist = MediaCollection.Select(searchExpression);
            /*
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
            }*/

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
            catch (Exception e)
            {
                return new MediaFile[0];
            }

            return this.GetFiles(quer);
        }

        #region [ Static ]

        static Library()
        {
            LibraryPathsFileName = "LibraryPaths.fbp";

        }

        ~Library()
        {
            this.Save();
        }

        public static String LibraryPathsFileName { get; private set; }

        /// <summary>
        /// Groups all files in a directory together into a Library.
        /// </summary>
        /// <param name="dir">A directory containing music files or a serialized library file</param>
        /// <returns>A library containing all the music in the given directory.</returns>
        public static Library Load(DirectoryInfo defaultDirectory)
        {
            Library library = new Library();
            try
            {
                using (StreamReader reader = new StreamReader(File.OpenRead(LibraryPathsFileName)))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        if (Directory.Exists(line))
                        {
                            library.Root.Add(new DirectoryInfo(line));
                        }
                    }
                }

                if (!File.Exists("library.fbsl"))
                {
                    Stream wtf = File.Create("library.fbsl");
                    wtf.Close();
                }
                else
                    using (StreamReader reader = new StreamReader(File.OpenRead("library.fbsl")))
                    {
                        string line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            DataRow row = library.MediaCollection.NewRow();

                            string fullname = line;
                            string artist = reader.ReadLine();
                            string album = reader.ReadLine();
                            string title = reader.ReadLine();
                            int bitrate = Int32.Parse(reader.ReadLine());
                            int channels = Int32.Parse(reader.ReadLine());
                            int samplerate = Int32.Parse(reader.ReadLine());
                            int year = Int32.Parse(reader.ReadLine());
                            TimeSpan duration = TimeSpan.Parse(reader.ReadLine());

                            Metadata metadata = new Metadata();

                            row["FullName"] = fullname;
                            row["Artist"] = artist;
                            row["Album"] = album;
                            row["Title"] = title;
                            row["BitRate"] = bitrate;
                            row["Channel"] = channels;
                            row["SampleRate"] = samplerate;
                            row["Year"] = year;
                            row["Duration"] = duration;

                            metadata["Artist"] = MediaProperty.Create("Artist", artist, typeof(string));
                            metadata["Album"] = MediaProperty.Create("Album", album, typeof(string));
                            metadata["Title"] = MediaProperty.Create("Title", title, typeof(string));
                            metadata["BitRate"] = MediaProperty.Create("BitRate", bitrate, typeof(int));
                            metadata["Channels"] = MediaProperty.Create("Channels", channels, typeof(int));
                            metadata["Duration"] = MediaProperty.Create("Duration", duration, typeof(TimeSpan));
                            metadata["SampleRate"] = MediaProperty.Create("SampleRate", samplerate, typeof(int));
                            metadata["Year"] = MediaProperty.Create("Year", year, typeof(int));

                            row["File"] = new MediaFile(fullname, metadata);

                            library.MediaPaths.Add(fullname);
                            library.MediaCollection.Rows.Add(row);
                        }
                    }

            }
            catch (Exception e)
            {
            }

            return library;
        }

        /// <summary>
        /// Serializes the Library into an XML file.
        /// </summary>
        public void Save()
        {
            using (StreamWriter writer = new StreamWriter(File.OpenWrite(LibraryPathsFileName)))
            {
                foreach (DirectoryInfo dir in Root)
                {
                    writer.WriteLine(dir.FullName);
                }
            }

            using (StreamWriter write = new StreamWriter(File.OpenWrite("library.fbsl")))
            {
                foreach (DataRow row in MediaCollection.Rows)
                {
                    write.WriteLine(row["FullName"]);
                    write.WriteLine(row["Artist"]);
                    write.WriteLine(row["Album"]);
                    write.WriteLine(row["Title"]);
                    write.WriteLine(row["BitRate"]);
                    write.WriteLine(row["Channel"]);
                    write.WriteLine(row["SampleRate"]);
                    write.WriteLine(row["Year"]);
                    write.WriteLine(row["Duration"]);
                }

            }

        }

        #endregion

        #region [ Private ]

        private Library()
        {
            Root = new List<DirectoryInfo>();
            MediaCollection = this.createTable();
            MediaPaths = new List<String>();
        }

        private Library(DirectoryInfo root)
            : this()
        {
            Root.Add(root);
        }

        private DataTable MediaCollection;

        private DataTable createTable()
        {
            DataTable output = new DataTable();
            
            try
            {
                output.DefaultView.Sort = "FullName";
            }
            catch (Exception e)
            {
            }

            /*
             * Eventually replace this with a global list of supported ID3 tags
             * So as to reduce the "magic number" nature of this code.
             */
            DataColumn toAdd = new DataColumn("#", typeof(Int32));
            toAdd.Unique = false;
            output.Columns.Add(toAdd);
            toAdd = new DataColumn("FullName", typeof(String));
            toAdd.Unique = true;
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
                dataout[i] = (MediaFile)rows[i]["File"];
                //if (rows[i] != null && rows[i]["File"] != null && !System.DBNull.Value.Equals(rows[i]["File"]))
                //{
                //    dataout[i] = (MediaFile)rows[i]["File"];
                //}
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

        private List<String> MediaPaths;

        #endregion
    }
}
