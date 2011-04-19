using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FractalBlaster.Universe;
using FractalBlaster.Core.Runtime;
using System.IO;
using System.Drawing.Drawing2D;

namespace FractalBlaster.Core.UI {

    /// <remarks>
    /// The main form for the application. Availability of items within this form
    /// depend upon the product model being used.
    /// </remarks>
    public partial class ProductForm : Form {

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductForm"/> class.
        /// </summary>
        public ProductForm() {
            InitializeComponent();
            WindowOffset = new List<Point>();
            Context = FamilyKernel.Instance.Context;
            Engine = Context.Engine;
            PluginViews = new List<Form>();
            PlaylistPluginMap = new Dictionary<String, IPlaylistPlugin>();

            //Create new SeekBar
            SeekBar = new SeekBar();
            SeekBar.Input = Engine.InputPlugin;
            SeekBar.Output = Engine.OutputPlugin;
            SeekBar.PlaybackTimer = Engine.PlaybackTimer;
            SeekBar.UI = this;
            SeekBarPanel.Controls.Add(SeekBar);
            seekBarRefreshTimer.Start();

            //Get references to plugins
            foreach (IPlaylistPlugin plugin in FamilyKernel.Instance.Context.AllPlugins.OfType<IPlaylistPlugin>()) {
                foreach (String f in plugin.SupportedFileExtensions) {
                    if (Config.GetProperty("playlistformats").Contains(f)) {
                        PlaylistPluginMap.Add(f, plugin);
                    }
                }
            }

            //Create first CurrentPlaylist
            PlaylistControl control = CreatePlaylistControl();
            mPlaylistTabControl.TabPages[0].Tag = control;
            mPlaylistTabControl.TabPages[0].Controls.Add(control);

            //Setup CurrentPlaylist tabs
            SetupCollectionTabs();
            foreach (TabPage tp in mPlaylistTabControl.TabPages) {
                tp.AllowDrop = true;
                tp.DragOver += (o, e) => {
                    e.Effect = DragDropEffects.Copy;
                };
            }

            //Disable buttons as specified by config file.
            if (Config.GetProperty("saveloadplaylists") == "false" ||
                Config.GetProperty("playlistformats") == "") {
                mSaveMenuItem.Enabled = false;
                mSaveToolBarButton.Enabled = false;
            }

            //Setup engine CurrentPlaylist.
            Context.Engine.CurrentPlaylist = (mPlaylistTabControl.SelectedTab.Tag as PlaylistControl).Playlist;

            //Hook up some events
            mSaveToolBarButton.Click += SavePlaylist;
            mNewPlaylistMenuItem.Click += AddNewPlaylistTab;
            mPlayToolBarButton.Click += mPlayToolBarButton_Click;
        }

        /// <summary>
        /// Adds the specified view plugin to the known view plugins. The
        /// list of view plugins is represented by the 'View' menu in the
        /// application itself.
        /// </summary>
        /// <param name="view">The view plugin to be added.</param>
        public void AddViewPlugin(IViewPlugin view) {
            ToolStripMenuItem item = new ToolStripMenuItem(view.GetInfo().Name);
            item.CheckOnClick = true;
            Form form = view.UserInterface;
            PluginViews.Add(form);
            form.Owner = this;
            form.FormClosing += (o, e) => {
                    item.Checked = false;
                };
            item.CheckedChanged += (o, e) => {
                ToolStripMenuItem viewItem = o as ToolStripMenuItem;
                if (viewItem.Checked) {
                    form.Show();
                    if (form.Location.IsEmpty) {
                        Point nextLocation = this.Location;
                        nextLocation.Offset(this.Width, NewViewOffsetY);
                        NewViewOffsetY += form.Height;
                        form.Location = nextLocation;
                    }
                }
                else {
                    form.Hide();
                }
            };
            form.FormClosed += (o, ea) => {
                item.CheckState = CheckState.Unchecked;
            };
            mViewsMenu.DropDownItems.Add(item);
        }

        /// <summary>
        /// Adds the specified effect plugin to the known effect plugins. The
        /// list of effect plugins is represented by the 'Effects' menu in the
        /// application itself.
        /// </summary>
        /// <param name="plugin">The effect plugin to be added.</param>
        public void AddEffectPlugin(IEffectPlugin plugin) {
            ToolStripMenuItem item = new ToolStripMenuItem(plugin.GetInfo().Name);
            item.CheckOnClick = true;
            Form form = plugin.UserInterface;

            form.FormClosing += (o, ea) => {
                    item.Checked = false;
                };

            item.CheckStateChanged += (o, ea) => {
                plugin.Enabled = item.CheckState == CheckState.Checked ? true : false;
                ToolStripMenuItem viewItem = o as ToolStripMenuItem;
                if (viewItem.Checked) {
                    form.Show();
                    if (form.Location.IsEmpty) {
                        Point nextLocation = this.Location;
                        nextLocation.Offset(this.Width, NewViewOffsetY);
                        NewViewOffsetY += form.Height;
                        form.Location = nextLocation;
                    }
                }
                else {
                    NewViewOffsetY -= form.Height;
                    form.Hide();
                }
            };
            mEffectsMenu.DropDownItems.Add(item);
        }

        /// <summary>
        /// Adds the specified playlist plugin to the known playlist plugins. The
        /// list of playlist plugins is represented by the availability for reading
        /// and writing certain formats using the open and save dialogs for the
        /// playlists.
        /// </summary>
        /// <param name="plugin">The playlist plugin to be added.</param>
        public void AddPlaylistPlugin(IPlaylistPlugin plugin) {
            ToolStripMenuItem item = new ToolStripMenuItem(String.Format("Export as {0} ...", plugin.GetInfo().Name));
            item.Click += new EventHandler(SavePlaylist);
            mSaveMenuItem.DropDownItems.Add(item);
        }

        #region  [ Private Methods ]

        /// <summary>
        /// Private helper method that sets up the collection tabs.
        /// </summary>
        private void SetupCollectionTabs() {
            Library lib = Library.Load(new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.MyMusic)));
            Library = lib;
            libraryView = new LibraryCollectionView(lib);
            libraryView.Dock = DockStyle.Fill;
            mLibraryCollectionTabPage.Controls.Add(libraryView);
            mLibraryCollectionTabPage.Text = libraryView.Label;

            FileSystemCollectionView fsView = new FileSystemCollectionView();
            fsView.Dock = DockStyle.Fill;
            mFileSystemCollectionTabPage.Controls.Add(fsView);
            mFileSystemCollectionTabPage.Text = fsView.Label;
        }

        /// <summary>
        /// Private helper method that gets the filter string to be used 
        /// in open/save dialog boxes.
        /// </summary>
        /// <returns>The open/save playlist filter string</returns>
        private String GetFilterString() {
            if (PlaylistFilterString == null) {
                IEnumerable<String> extensions = PlaylistPluginMap.Keys;
                String extensionFilters = "";
                foreach (String extension in extensions) {
                    extensionFilters += String.Format("*{0};", extension);
                }
                PlaylistFilterString = String.Format("Playlists ({0}) | {0}", extensionFilters.Remove(extensionFilters.Length - 1));
            }
            return PlaylistFilterString;
        }

        /// <summary>
        /// Private helper that returns a filter string for all files.
        /// </summary>
        /// <returns>Filter string for all files.</returns>
        private String GetAllFilesFilterString() {
            List<string> file_extensions = Config.GetProperty("fileformats").Split(';').ToList<string>();
            IEnumerable<string> pl_extensions = Config.GetProperty("playlistformats").Split(';').ToList<string>();

            file_extensions.AddRange(pl_extensions);
            string filters = "";
            foreach (string extension in file_extensions) {
                filters += String.Format("{0};", extension);
            }
            return String.Format("Audio Files ({0}) | {0}", filters);
        }

        /// <summary>
        /// Private helper that creates a playlist.
        /// </summary>
        /// <param name="playlist">The optionally already created playlist to use.</param>
        /// <returns></returns>
        private Playlist CreatePlaylist(Playlist playlist = null) {
            if (playlist == null) {
                playlist = new Playlist();
            }

            playlist.MediaRequested += m => { PlayMedia(null, new EventArgs()); };
            return playlist;
        }

        /// <summary>
        /// Private helper method that creates a playlist control.
        /// </summary>
        /// <param name="p">The playlist to be used by the playlist control.</param>
        /// <returns>The newly created instance of <see cref="PlaylistControl"/> class.</returns>
        private PlaylistControl CreatePlaylistControl(Playlist p = null) {
            if (p == null) {
                p = CreatePlaylist();
            }
            PlaylistControl pc = new PlaylistControl();
            pc.Playlist = p;
            pc.Dock = DockStyle.Fill;
            pc.SongPlayed += new PlaylistControlSongPlayedEventHandler(pc_SongPlayed);
            return pc;
        }

        /// <summary>
        /// Private helper that creates a new playlist tab page.
        /// </summary>
        /// <param name="playlist">The playlist to use.</param>
        /// <returns>The created tab page.</returns>
        private TabPage CreateNewPlaylistTab(Playlist playlist = null) {
            PlaylistControl pc = CreatePlaylistControl(CreatePlaylist(playlist));
            TabPage tab = new TabPage(String.Format("New {0}", mPlaylistTabControl.TabCount));
            tab.Tag = pc;
            tab.Controls.Add(pc);
            return tab;
        }

        #endregion

        #region [ Private Properties ]
		
        /// <summary>
        /// Gets the currently active playlist control.
        /// </summary>
        private PlaylistControl CurrentPlaylistControl { get { return mPlaylistTabControl.SelectedTab.Tag as PlaylistControl; } }

        /// <summary>
        /// Private property containing the playlist currently being played's control.
        /// </summary>
        /// <value>
        /// The playing playlist control.
        /// </value>
        private PlaylistControl PlayingPlaylistControl { get; set; }

        /// <summary>
        /// Private property containing the point representing the mouse's offset.
        /// </summary>
        /// <value>
        /// The mouse offset.
        /// </value>
        private Point MouseOffset { get; set; }

        /// <summary>
        /// Private property containing a list of points representing the window's offets.
        /// </summary>
        /// <value>
        /// The list of the window's offsets.
        /// </value>
        private List<Point> WindowOffset { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the mouse is down.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if the mouse down; otherwise, <c>false</c>.
        /// </value>
        private Boolean IsMouseDown { get; set; }

        /// <summary>
        /// Private property containing a reference to the Library of audio files.
        /// </summary>
        /// <value>
        /// The library.
        /// </value>
        private Library Library { get; set; }

        private LibraryCollectionView libraryView { get; set; }

        /// <summary>
        /// The application's context it was initialized with.
        /// </summary>
        /// <value>
        /// The context.
        /// </value>
        private AppContext Context { get; set; }

        /// <summary>
        /// Private property holding the reference to the audio engine.
        /// </summary>
        /// <value>
        /// The audio engine.
        /// </value>
        private IEngine Engine { get; set; }

        /// <summary>
        /// Private property containing a list of plugin views.
        /// </summary>
        /// <value>
        /// The plugin views.
        /// </value>
        private List<Form> PluginViews { get; set; }

        /// <summary>
        /// Private property containing a dictionary, where the key is a string
        /// representing the file extension of the playlist, and value is the
        /// plugin that is responsible for reading/writing of playlist's with
        /// that file extension.
        /// </summary>
        /// <value>
        /// Dictionary containing keys representing file extensions with values
        /// represented as <see cref="IPlaylistPlugin"/>'s.
        /// </value>
        private Dictionary<String, IPlaylistPlugin> PlaylistPluginMap { get; set; }

        /// <summary>
        /// Private property containing the string used as a filter string in
        /// certain open/save dialogs.
        /// </summary>
        /// <value>
        /// The playlist filter string.
        /// </value>
        private String PlaylistFilterString { get; set; }

        /// <summary>
        /// Private property holding a reference to seek bar user control.
        /// </summary>
        /// <value>
        /// The seek bar.
        /// </value>
        private SeekBar SeekBar { get; set; }

        /// <summary>
        /// Private property containing a int representing the y-offset for a new
        /// view.
        /// </summary>
        /// <value>
        /// The new view offset Y.
        /// </value>
        private Int32 NewViewOffsetY { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the volume mouse is down.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if the volume mouse is down; otherwise, <c>false</c>.
        /// </value>
        private Boolean IsVolumeMouseDown { get; set; }

	#endregion

    }

}