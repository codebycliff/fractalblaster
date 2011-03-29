using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
//using FractalBlaster.Core.Runtime;
using FractalBlaster.Universe;
//using FractalBlaster.Core;
//using log4net.Config;
using System.Threading;

namespace FractalBlaster.Runtime {

    static class Program {

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(String[] args) {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Debug.printline("FractalBlasters started");

            PluginManager.Refresh();
            Debug.printline("Found " + PluginManager.AllPlugins.Count().ToString() + " Plugins");

            IEnumerable<IPlugin> inputs = PluginManager.GetInterfaces(typeof(IInput));
            Debug.printline("Found " + inputs.Count().ToString() + " Inputs");

            IEnumerable<IPlugin> libraryForms = PluginManager.GetInterfaces(typeof(ILibraryForm));
            Debug.printline("Found " + libraryForms.Count().ToString() + " Library Forms");

            IEnumerable<IPlugin> libraries = PluginManager.GetInterfaces(typeof(ILibrary));
            Debug.printline("Found " + libraries.Count().ToString() + " Libraries");

            IEnumerable<IPlugin> outputs = PluginManager.GetInterfaces(typeof(IOutput));
            Debug.printline("Found " + outputs.Count().ToString() + " Outputs");

            IEnumerable<IPlugin> playbackControlForms = PluginManager.GetInterfaces(typeof(IPlaybackControlForm));
            Debug.printline("Found " + playbackControlForms.Count().ToString() + " Playback Control Forms");
            
            IEnumerable<IPlugin> playbackControls = PluginManager.GetInterfaces(typeof(IPlaybackControl));
            Debug.printline("Found " + playbackControls.Count().ToString() + " Playback Controls");

            IEnumerable<IPlugin> playlistForms = PluginManager.GetInterfaces(typeof(IPlaylistForm));
            Debug.printline("Found " + playlistForms.Count().ToString() + " Playlist Forms");

            IEnumerable<IPlugin> playlists = PluginManager.GetInterfaces(typeof(IPlaylist));
            Debug.printline("Found " + playlists.Count().ToString() + " Playlists");

            IEnumerable<IPlugin> pluginForms = PluginManager.GetInterfaces(typeof(IPluginForm));
            Debug.printline("Found " + pluginForms.Count().ToString() + " Plugin Forms");

            IEnumerable<IPlugin> wavePlugins = PluginManager.GetInterfaces(typeof(IWavePlugin));
            Debug.printline("Found " + wavePlugins.Count().ToString() + " Wave Plugins");

            if (playbackControlForms.Count() < 1)
            {
                Debug.printline("ERROR: Playback Control Form not found.  Exiting in 5 seconds");
                Thread.Sleep(5000);
                return;
            }
            else if (playbackControlForms.Count() > 1)
            {
                Debug.printline("WARNING: More than one Playback Control Form found.");
            }

            if (playbackControls.Count() < 1)
            {
                Debug.printline("ERROR: Playback Control not found.  Exiting in 5 seconds");
                Thread.Sleep(5000);
                return;
            }
            else if (playbackControls.Count() > 1)
            {
                Debug.printline("WARNING: More than one Playback Control found.");
            }

            Debug.printline("Starting Playback Control Form");
            IPlaybackControlForm playbackControlForm = (IPlaybackControlForm) playbackControlForms.First();
            IPlaybackControl playbackControl = (IPlaybackControl)playbackControls.First();
            playbackControlForm.playbackControl = playbackControl;

            if (playlistForms.Count() < 1)
            {
                Debug.printline("WARNING: Playlist Form not found.");
            }
            else
            {
                if (playlistForms.Count() > 1)
                {
                    Debug.printline("WARNING: More than one Playlist Form found.");
                }
                IPlaylistForm playlistForm = (IPlaylistForm)playlistForms.First();
                playbackControlForm.form.AddOwnedForm(playlistForm.form);
                playlistForm.form.Show();
            }

            if (inputs.Count() < 1)
            {
                Debug.printline("ERROR: Input not found.  Exiting in 5 seconds");
                Thread.Sleep(5000);
                return;
            }
            else if (inputs.Count() > 1)
            {
                Debug.printline("WARNING: More than one Input found.");
            }

            IInput input = (IInput)inputs.First();

            if (outputs.Count() < 1)
            {
                Debug.printline("ERROR: Output not found.  Exiting in 5 seconds");
                Thread.Sleep(5000);
                return;
            }
            else if (outputs.Count() > 1)
            {
                Debug.printline("WARNING: More than one Output found.");
            }

            IOutput output = (IOutput)outputs.First();

            output.input = input;

            playbackControl.input = input;
            playbackControl.output = output;
            
            Application.Run(playbackControlForm.form);
        }
    }
}
