using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FractalBlaster.Universe;
using System.IO;

namespace FractalBlaster.Plugins.M3UPlaylist {

    /// <remarks>
    /// Class providing a playlist plugin implementation for the M3U
    /// playlist format.
    /// </remarks>
    [PluginAttribute(
        Name="M3U Playlist", 
        Description="Provides support for reading and writing the M3U Playlist file format",
        Author="Cliff Braton @ Fractal Blasters",
        Version="0.1beta"
    )]
    public class M3UPlaylistPlugin : IPlaylistPlugin {

        /// <summary>
        /// Constant string representing a M3U header declaration.
        /// </summary>
        public const String HEADER_EXT_M3U = "#EXTM3U";

        /// <summary>
        /// Constant string representing a M3U header declaration for extra FileInfo.
        /// </summary>
        public const String HEADER_EXT_INF = "#EXTINF";

        /// <summary>
        /// Gets the applicatino context the plugin was initialized with.
        /// </summary>
        public AppContext Context { get; private set; }

        #region [ IPlaylistPlugin Implemenation ]

        /// <summary>
        /// Attempts to read in the file at the specified path and create an
        /// instance of <see cref="Playlist"/> based on it's contents.
        /// </summary>
        /// <param name="path">The path the file representing the playlist to be read in.</param>
        /// <returns>
        /// An instance of <see cref="Playlist"/> based on the contents of the
        /// file at the specified path.
        /// </returns>
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
                        if (!File.Exists(line)) {
                            string newpath = new FileInfo(path).Directory + "\\" + line;
                            if (!File.Exists(newpath)) {
                                continue;
                            }
                            else {
                                line = newpath;
                            }
                        }
                        try {

                            MediaFile file = new MediaFile(line);

                            playlist.AddItem(file);
                        }
                        catch {
                            Console.WriteLine("Error opening file: " + line);
                        }
                    }
                }
            }
            return playlist;
        }

        /// <summary>
        /// Method used to write a given playlist to the path specified.
        /// </summary>
        /// <param name="playlist">The playlist to write.</param>
        /// <param name="path">The path to the file to written to.</param>
        public void Write(Playlist playlist, string path) {
            using (StreamWriter writer = new StreamWriter(File.OpenWrite(path))) {
                writer.WriteLine(HEADER_EXT_M3U);
                foreach (MediaFile file in playlist.Items) {
                    writer.WriteLine(file.Info.FullName);
                }
            }
        }

        /// <summary>
        /// Initializes the plugin with specified application context.
        /// </summary>
        /// <param name="context">The context.</param>
        public void Initialize(AppContext context) {
            Context = context;
        }

        /// <summary>
        /// Determines the file extension this plugin can create a <see cref="Playlist"/> instance from.
        /// </summary>
        public IEnumerable<string> SupportedFileExtensions {
            get { return new String[] { ".m3u" }; }
        }

        /// <summary>
        /// Determines whether the specified file extension is supported.
        /// </summary>
        /// <param name="extension">The extension.</param>
        /// <returns>
        ///   <c>true</c> if the specified file extension is supported; otherwise, <c>false</c>.
        /// </returns>
        public Boolean IsFileExtensionSupported(String extension) {
            return extension.ToLower().CompareTo("m3u") == 0;
        }
        
        #endregion

    }
}
