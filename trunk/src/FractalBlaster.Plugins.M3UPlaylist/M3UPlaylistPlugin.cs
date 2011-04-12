using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FractalBlaster.Universe;
using System.IO;

namespace FractalBlaster.Plugins.M3UPlaylist {
    
    [PluginAttribute(
        Name="M3U Playlist", 
        Description="Provides support for reading and writing the M3U Playlist file format",
        Author="Cliff Braton @ Fractal Blasters",
        Version="0.1beta"
    )]
    public class M3UPlaylistPlugin : IPlaylistPlugin {

        public const String HEADER_EXT_M3U = "#EXTM3U";
        public const String HEADER_EXT_INF = "#EXTINF";

        public AppContext Context { get; private set; }

        public bool IsFileExtensionSupported(string extension) {
            return extension.ToLower().CompareTo("m3u") == 0;
        }

        public Playlist Read(string path) {
            Playlist playlist = new Playlist();
            using (StreamReader reader = new StreamReader(File.OpenRead(path))) {
                while (!reader.EndOfStream) {
                    String line = reader.ReadLine().Trim();
                    if (line.Contains("#")) {
                        line = line.Remove(line.IndexOf('#'));
                    }
                    else if (line.Length == 0 || line == "") {
                        continue;
                    }
                    else {
                        if (line.StartsWith(".")) {
                            line = new FileInfo(path).Directory + line.Substring(1);
                        }
                        if (!File.Exists(line))
                        {
                            string newpath = new FileInfo(path).Directory + "\\" + line;
                            if (!File.Exists(newpath))
                            {
                                continue;
                            }
                            else
                            {
                                line = newpath;
                            }
                        }
                        try
                        {

                            MediaFile file = new MediaFile(line);

                            playlist.AddItem(file);
                        }
                        catch
                        {
                            Console.WriteLine("Error opening file: " + line);
                        }
                    }
                }                
            }
            return playlist;
        }


        public void Write(Playlist playlist, string path) {
            using (StreamWriter writer = new StreamWriter(File.OpenWrite(path))) {
                writer.WriteLine(HEADER_EXT_M3U);
                foreach (MediaFile file in playlist.Items) {
                    writer.WriteLine(file.Info.FullName);
                }
            }
        }

        public void Initialize(AppContext context) {
            Context = context;
        }


        public IEnumerable<string> SupportedFileExtensions {
            get { return new String[] { ".m3u" }; }
        }
    }
}
