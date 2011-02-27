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
        
        public DirectoryInfo Root { get; private set; }

        public Int32 ItemCount { get { return MediaCollection.Rows.Count; } }

        public IEnumerable<String> Artists {
            get
            {
                DataRow[] quer = MediaCollection.DefaultView.ToTable(true, "Artist").Select();
                String[] dataout = new String[quer.Length];

                for (int i = 0; i < quer.Length; i += 1)
                {
                    dataout[i] = (String)quer[i]["Artists"];
                }

                return dataout;
            }
        }

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

        public IEnumerable<MediaFile> AllMedia { 
            get
            {
                DataRow[] quer = MediaCollection.Select();
                MediaFile[] dataout = new MediaFile[quer.Length];

                for (int i = 0; i < quer.Length; i += 1)
                {
                    dataout[i] = (MediaFile)quer[i]["File"];
                }

                return dataout;
            }
        }

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

        public void Save() {
            XmlSerializer serializer = new XmlSerializer(typeof(Library));
            using (Stream stream = File.OpenWrite(Root.FullName + FileName)) {
                serializer.Serialize(stream, this);
            }
        }

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

                        DataRow dr = MediaCollection.NewRow();

                        dr["Artist"] = artist;
                        dr["Album"] = album;
                        dr["Title"] = title;

                        MediaCollection.Rows.Add(dr);
                    }
                    catch (Exception e)
                    {
                        Console.Error.WriteLine("Problem Opening MediaFile:\nException Type: {0}\nMessage:{1}", e.GetType().FullName, e.Message);
                    }
                }

            }
        }

        public IEnumerable<MediaFile> MediaForArtist(String artist)
        {
            DataRow[] quer = MediaCollection.Select("Artist = '" + artist + "'");
            MediaFile[] dataout = new MediaFile[quer.Length];

            for (int i = 0; i < quer.Length; i += 1)
            {
                dataout[i] = (MediaFile) quer[i]["File"];
            }

            return dataout;
        }

        public IEnumerable<MediaFile> MediaForAlbum(String artist, String album)
        {
            DataRow[] quer = MediaCollection.Select("Artist = '" + artist + "' AND Album = '" + album + "'");
            MediaFile[] dataout = new MediaFile[quer.Length];

            for (int i = 0; i < dataout.Length; i += 1)
            {
                dataout[i] = (MediaFile) quer[i]["File"];
            }

            return dataout;
        }

        #region [ Static ]

        static Library() {
            FileName = "Library.xml";
            
        }

        public static String FileName { get; private set; }

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

            output.Columns.Add("#", typeof(Int32));
            output.Columns.Add("Artist", typeof(String));
            output.Columns.Add("Album", typeof(String));
            output.Columns.Add("BitRate", typeof(Int32));
            output.Columns.Add("Channel", typeof(Int32));
            output.Columns.Add("Codec", typeof(String));
            output.Columns.Add("Duration", typeof(TimeSpan));
            output.Columns.Add("SampleRate", typeof(Int32));
            output.Columns.Add("Title", typeof(String));
            output.Columns.Add("Year", typeof(Int32));
            output.Columns.Add("File", typeof(MediaFile));

            return output;
        }

        private List<String> MediaPaths { get; set; }

        #endregion    
    }
}
