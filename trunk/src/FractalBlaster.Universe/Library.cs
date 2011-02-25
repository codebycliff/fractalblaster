using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using FractalBlaster.Universe;

namespace FractalBlaster.Universe {

    [Serializable]
    public class Library {
        
        public DirectoryInfo Root { get; private set; }

        public Int32 ItemCount { get { return MediaPaths.Count; } }

        public IEnumerable<String> Artists { get { return MediaItems.Keys; } }

        public IEnumerable<String> Albums { get { return MediaItems.Values.SelectMany(i=> i.Keys); } }

        public IEnumerable<MediaFile> AllMedia { 
            get {
                List<MediaFile> allmedia = new List<MediaFile>();
                foreach (String artist in MediaItems.Keys) {
                    foreach (List<MediaFile> album in MediaItems[artist].Values) {
                        allmedia.AddRange(album);
                    }
                }
                return allmedia;
            } 
        }

        public Dictionary<String, List<MediaFile>> this[String key] {
            get {
                return MediaItems[key];
            }
        }

        public void Save() {
            XmlSerializer serializer = new XmlSerializer(typeof(Library));
            using (Stream stream = File.OpenWrite(Root.FullName + FileName)) {
                serializer.Serialize(stream, this);
            }
        }

        public void Refresh() {
            FileInfo[] files = Root.GetFiles("*.mp3", SearchOption.AllDirectories);
            
            foreach (FileInfo file in files) {
                if (MediaPaths.Contains(file.FullName)) {
                    continue;
                }
                else {
                    MediaPaths.Add(file.FullName);
                    try {
                        MediaFile media = file.CreateMediaFile();
                        String artist = media.Metadata.Artist;
                        String album = media.Metadata.Album;
                        String title = media.Metadata.Title;
                        if (artist != Metadata.NOT_AVAILABLE) {
                            if (!MediaItems.ContainsKey(artist)) {
                                MediaItems.Add(artist, new Dictionary<String, List<MediaFile>>());
                            }
                        }
                        if (album != Metadata.NOT_AVAILABLE) {
                            if (!MediaItems[artist].ContainsKey(album)) {
                                MediaItems[artist].Add(album, new List<MediaFile>());
                            }
                        }
                        if (title != Metadata.NOT_AVAILABLE) {
                            if (MediaItems.ContainsKey(artist)
                                && MediaItems[artist].ContainsKey(album)
                                && !MediaItems[artist][album].Contains(media)) {
                                MediaItems[artist][album].Add(media);
                            }
                        }
                    }
                    catch (Exception e) {
                        Console.Error.WriteLine("Problem Opening MediaFile:\nException Type: {0}\nMessage:{1}", e.GetType().FullName, e.Message);
                    }
                }
                
            }
        }

        public IEnumerable<MediaFile> MediaForArtist(String artist) {
            
            List<MediaFile> results = new List<MediaFile>();
            foreach (List<MediaFile> mlist in MediaItems[artist].Values) {
                results.AddRange(mlist);
            }
            return results.AsEnumerable<MediaFile>();
        }

        public IEnumerable<MediaFile> MediaForAlbum(String artist, String album) {
            return MediaItems[artist][album].AsEnumerable<MediaFile>();
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

            MediaItems =new Dictionary<
                    String,             // ARTIST NAME
                    Dictionary<
                        String,         // ALBUM NAME
                        List<MediaFile> // LIST OF SONGS ON ALBUM
                    >
                >();
            if (File.Exists(Root.FullName + FileName)) {
                File.Delete(Root.FullName + FileName);
            }
            MediaPaths = new List<String>();
        }

        private Dictionary<String, Dictionary<String, List<MediaFile>>> MediaItems { get; set; }

        private List<String> MediaPaths { get; set; }

        #endregion    
    }
}
