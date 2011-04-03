using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using FractalBlaster.Universe;

namespace FractalBlaster.Playlist
{
    static class m3uPlaylist
    {

        public const String HEADER_EXT_M3U = "#EXTM3U";
        public const String HEADER_EXT_INF = "#EXTINF";

        static public Playlist Read(string path)
        {
            Playlist playlist = new Playlist();
            using (StreamReader reader = new StreamReader(File.OpenRead(path)))
            {
                while (!reader.EndOfStream)
                {
                    String line = reader.ReadLine().Trim();
                    if (line.Contains("#"))
                    {
                        line = line.Remove(line.IndexOf('#'));
                    }
                    else if (line.Length == 0 || line == "")
                    {
                        continue;
                    }
                    else
                    {
                        if (line.StartsWith("."))
                        {
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
                        MediaFile file = new MediaFile(line);
                        playlist.add(file);
                    }
                }
            }
            return playlist;
        }


        static public void Write(Playlist playlist, string path)
        {
            using (StreamWriter writer = new StreamWriter(File.OpenWrite(path)))
            {
                writer.WriteLine(HEADER_EXT_M3U);
                foreach (MediaFile file in playlist.getList())
                {
                    writer.WriteLine(file.Info.Name);
                }
            }
        }
    }
}
