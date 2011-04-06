using System;
using System.IO;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FractalBlaster.Universe;

namespace FractalBlaster.Plugins.BasicVisualizer
{
    /// <summary>
    /// BasicVisualizerPlugin provides basic visualizer functionality.
    /// </summary>
    [PluginAttribute(Name = "Basic Visualizer", Author = "Fractal Blaster", Description = "A basic visualizer", Version = "0.1a")]
    public class BasicVisualizerPlugin : IViewPlugin, IEffectPlugin
    {
        #region Fields
        /// <summary>
        /// Visualizer Form UI object.
        /// </summary>
        VisualizerForm _UI;

        /// <summary>
        /// VisualizerGraphicsControl reference to pass data.
        /// </summary>
        VisualizerGraphicsControl _graphicsControl;

        /// <summary>
        /// ConcurrentQueue used to push new sample data to the VisualizerGraphicsControl.
        /// </summary>
        ConcurrentQueue<float> _visData;

        /// <summary>
        /// AppContext provides information about playing songs and stuff.
        /// </summary>
        AppContext _context;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructs a new BasicVisualizerPlugin.
        /// </summary>
        public BasicVisualizerPlugin()
        {
            //Create UI
            _UI = new VisualizerForm(this);
            //Get reference to the graphics control
            _graphicsControl = _UI.GraphicsControl;
            //Create new Queue for data transfer.
            _visData = new ConcurrentQueue<float>();
            //Set queue reference in GraphicsControl.
            _graphicsControl.Queue = _visData;

        }
        #endregion

        #region IPlugin Members
        /// <summary>
        /// Initialize this plugin.
        /// </summary>
        /// <param name="context">AppContext provides information about currently playing song.</param>
        public void Initialize(AppContext context)
        {
            _context = context;
            context.Engine.OnMediaChanged += new MediaChangeHandler(OnMediaChanged);
        }
        #endregion

        #region IViewPlugin Members
        /// <summary>
        /// Accessor for user interface form.
        /// </summary>
        public Form UserInterface
        {
            get { return _UI; }
        }
        #endregion

        #region IEffectPlugin
        /// <summary>
        /// Processes MemoryStream from audio engine.
        /// </summary>
        /// <param name="stream">Stream containing audio byte data.</param>
        public void ProcessStream(MemoryStream stream)
        {
            //Tell GraphicsControl object how long the stream is.
            _graphicsControl.ChunkSampleCount = (int)(stream.Length / 2);

            //Copy, process, and submit to VisualizerForm
            for (int i = 0; i < stream.Length; i += 4)
            {
                //Check the number of bytes we're reading each time.
                int bytes_read;
                //Allocate memory to read each sample into
                byte[] temp = new byte[2];

                //Read left sample
                bytes_read = stream.Read(temp, 0, 2);
                int sampleLeft = BitConverter.ToInt16(temp, 0);

                //Read right sample
                bytes_read = stream.Read(temp, 0, 2);
                int sampleRight = BitConverter.ToInt16(temp, 0);

                //Record new floating point PCM samples.
                _visData.Enqueue((float)(2f * sampleLeft / UInt16.MaxValue));
                _visData.Enqueue((float)(2f * sampleRight / UInt16.MaxValue));
            }
        }

        /// <summary>
        /// Enables or disables this plugin.
        /// </summary>
        public bool Enabled
        {
            get;
            set;
        }
        #endregion


        #region Private Members

        /// <summary>
        /// Media Change event handler.
        /// </summary>
        /// <param name="mediaFile">The media file to which our media player has changed.</param>
        private void OnMediaChanged(MediaFile mediaFile)
        {
        }
        #endregion
    }
}
