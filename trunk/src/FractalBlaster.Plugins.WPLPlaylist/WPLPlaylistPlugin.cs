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
        Version = "0.1"
    )]
    public class M3UPlaylistPlugin : IPlaylistPlugin
    {
        public AppContext Context { get; private set; }

        public bool IsFileExtensionSupported(string extension)
        {
            return extension.ToLower().CompareTo("wpl") == 0;
        }

        public Playlist Read(string path)
        {
            Playlist playlist = new Playlist();
            using (StreamReader reader = new StreamReader(File.OpenRead(path)))
            {
                String line = reader.ReadLine().Trim();
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

        public void Write(Playlist playlist, string path)
        {
            using (StreamWriter writer = new StreamWriter(File.OpenWrite(path)))
            {
                writer.WriteLine("<?wpl version=\"1.0\"?>");
                writer.WriteLine("<smil>");
                writer.WriteLine("<head>");
                writer.WriteLine("<meta name=\"Generator\" content=\"Microsoft Windows Media Player -- 12.0.7600.16667\"/>");
                /*writer.WriteLine("<meta name=\"IsNetworkFeed\" content=\"0\"/>");
                writer.WriteLine("<meta name=\"ItemCount\" content=\"\"/>");
                writer.WriteLine("<meta name=\"IsFavorite\"/>");
                writer.WriteLine("<meta name=\"ContentPartnerListID\"/>");
                writer.WriteLine("<meta name=\"ContentPartnerNameType\"/>");
                writer.WriteLine("<meta name=\"ContentPartnerName\"/>");
                writer.WriteLine("<meta name=\"Subtitle\"/>");
                */writer.WriteLine("<author/>");
                writer.WriteLine("<title>WPL Playlist</title>");
                writer.WriteLine("</head>");
                writer.WriteLine("<body>");
                writer.WriteLine("<seq>");
                foreach (MediaFile file in playlist.Items)
                {
                    writer.WriteLine(file.Info.DirectoryName);
                }
            }
        }

        public void Initialize(AppContext context)
        {
            Context = context;
        }

        public IEnumerable<string> SupportedFileExtensions
        {
            get { return new String[] { ".wpl" }; }
        }

        private string ReturnPlaylistName(StreamReader reader)
        {
            string line = "";
            while (!reader.EndOfStream && !line.Contains("<title>") && !line.Contains("<seq>"))//<title> means we found it, <seq> means it doesn't exist, EOS is obvious
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
