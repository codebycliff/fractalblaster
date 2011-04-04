using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FractalBlaster.Universe;
using System.IO;

namespace FractalBlaster.Plugins.WPLPlaylist
{

    [PluginAttribute(
        Name = "WPL Playlist",
        Description = "Provides support for reading and writing the WPL Playlist file format",
        Author = "Kevin Moore @ Fractal Blasters",
        Version = "1.0"
    )]
    public class WPLPlaylistPlugin : IPlaylistPlugin
    {
        public AppContext Context { get; private set; }

        public bool IsFileExtensionSupported(string extension)
        {
            return extension.ToLower().CompareTo("wpl") == 0;
        }

        /// <summary>
        /// Reads the WPL formatted playlist and returns a valid Playlist object as pertains to our program.
        /// </summary>
        /// <param name="path">The file path of the WPL Playlist to read</param>
        /// <returns>A valid Playlist as readable by our program</returns>
        public Playlist Read(string path)
        {
            Playlist playlist = new Playlist();
            using (StreamReader reader = new StreamReader(File.OpenRead(path)))
            {
                String line = reader.ReadLine().Trim();
                while (!reader.EndOfStream && !line.Contains("<title>"))
                {
                    line = reader.ReadLine();
                }
                if (line.Contains("<title>"))
                {
                    line = line.Replace("<title>", "");
                    line = line.Replace("</title>", "");
                    playlist.Title = line;
                }
                while (!reader.EndOfStream && !line.Contains("<media src=\""))//Pass all the meta tags and crap
                {
                    line = reader.ReadLine();
                }
                if (line.Contains("<media src=\""))
                {
                    while (!reader.EndOfStream && !line.Contains("</seq>"))//Now fill in array until end tag
                    {
                        int start = line.IndexOf("<media src=\"") + 12;
                        int end = line.IndexOf("\"", start) - start;
                        line = line.Substring(start, end);
                        MediaFile file = new MediaFile(line);
                        playlist.AddItem(file);
                        line = reader.ReadLine();
                    }
                }
                //Else it is a blank playlist
            }
            return playlist;
        }

        /// <summary>
        /// Writes the Playlist deemed by "playlist" to the location given by "path" in a valid WPL Playlist format.
        /// </summary>
        /// <param name="playlist">The Playlist object to convert to WPL and write to file</param>
        /// <param name="path">The file path to write the Playlist to</param>
        public void Write(Playlist playlist, string path)
        {
            using (StreamWriter writer = new StreamWriter(File.OpenWrite(path)))
            {
                writer.WriteLine("<?wpl version=\"1.0\"?>");
                writer.WriteLine("<smil>");
                writer.WriteLine("<head>");
                writer.WriteLine("<meta name=\"Generator\" content=\"Microsoft Windows Media Player -- 12.0.7600.16667\"/>");
                writer.WriteLine("<author/>");
                writer.WriteLine("<title>\""+playlist.Title+"\"</title>");
                writer.WriteLine("</head>");
                writer.WriteLine("<body>");
                writer.WriteLine("<seq>");
                foreach (MediaFile file in playlist.Items)
                {
                    writer.WriteLine(file.Info.DirectoryName);
                }
            }
        }

        /// <summary>
        /// Initialize the plugin
        /// </summary>
        /// <param name="context"></param>
        public void Initialize(AppContext context)
        {
            Context = context;
        }

        /// <summary>
        /// Return the supported file extensions as deemed by this plugin
        /// </summary>
        public IEnumerable<string> SupportedFileExtensions
        {
            get { return new String[] { ".wpl" }; }
        }

        /// <summary>
        /// Retrieves the playlist name from the StreamReader
        /// </summary>
        /// <param name="reader">The StreamReader to grab the Playlist name from</param>
        /// <returns>The name of the Playlist or a blank string if no name</returns>
        private string ReturnPlaylistName(StreamReader reader)
        {
            string line = "";
            while (!reader.EndOfStream && !line.Contains("<title>") && !line.Contains("<seq>"))//<title> means we found it, <seq> means it doesn't exist, EOS is simply the end of stream
            {
                line = reader.ReadLine().Trim();
            }
            if (line.Contains("<title>"))
            {
                line = line.Replace("<title>", "");
                line = line.Replace("</title>", "");
                return line;
            }
            return "";
        }
    }
}
