#region Using Statements
using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
#endregion

namespace FractalBlaster.Plugins.BasicVisualizer
{
    /// <summary>
    /// VisualizerGraphicsControl is a form control object that controls drawing the visualizer.
    /// </summary>
    public class VisualizerGraphicsControl : GraphicsDeviceControl
    {
        #region Constants
        /// <summary>
        /// Samples per channel per display buffer
        /// </summary>
        internal const int kBUFFER_SIZE = 1024;

        /// <summary>
        /// Number of seconds per display buffer.
        /// </summary>
        private const double kSECONDS_PER_BUFFER = kBUFFER_SIZE / 44100f;

        /// <summary>
        /// Number of milliseconds per display buffer.
        /// </summary>
        private const double kMILLISECONDS_PER_BUFFER = kSECONDS_PER_BUFFER * 1000;

        /// <summary>
        /// Number of "ticks" per buffer (1 tick = 100 nanoseconds)
        /// </summary>
        private const long kTICKS_PER_BUFFER = (long)(kMILLISECONDS_PER_BUFFER * 10000);
        #endregion

        #region Fields
        /// <summary>
        /// Storage for the current set of left samples.
        /// </summary>
        float[] _leftChannel = new float[kBUFFER_SIZE];

        /// <summary>
        /// Storage for the current set of right samples.
        /// </summary>
        float[] _rightChannel = new float[kBUFFER_SIZE];

        /// <summary>
        /// ContentManager for loading shaders and such.
        /// </summary>
        ContentManager _content;

        /// <summary>
        /// ConcurrentQueue is the source of samples for the VisualizerGraphicsControl.
        /// </summary>
        ConcurrentQueue<float> _dataQueue;

        /// <summary>
        /// Draws the oscilloscope lines.
        /// </summary>
        OscilloscopeLines _lines;

        /// <summary>
        /// Applys the visual trails effects when drawn.
        /// </summary>
        VisualTrailsEffect _trails;

        /// <summary>
        /// RenderTarget used for intermediate drawing steps.
        /// </summary>
        RenderTarget2D _target;

        /// <summary>
        /// RenderTarget two used for intermediate drawing steps.
        /// </summary>
        RenderTarget2D _target2;

        /// <summary>
        /// SpriteBatch for drawing render targets to screen.
        /// </summary>
        SpriteBatch _spriteBatch;

        /// <summary>
        /// Number of ticks at the last update.
        /// </summary>
        long _ticks_last_update = 0;

        SpriteFont _spriteFont;

        VisualizerSongTitleDisplay _titleDisplay;


        #endregion

        #region Properties
        internal ConcurrentQueue<float> Queue
        {
            set { _dataQueue = value; }
        }

        internal int ChunkSampleCount
        {
            set;
            get;
        }

        #endregion

        #region Initialize
        /// <summary>
        /// Initializes this VisualizerGraphicsControl.
        /// </summary>
        protected override void Initialize()
        {
            //Create some services
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _content = new ContentManager(this.Services);

            _spriteFont = _content.Load<SpriteFont>("./Plugins/Other Content/SpriteFont");

            VisualizerDisplayTextSettings settings = new VisualizerDisplayTextSettings();

            settings.FadeInTime = 2.0f;
            settings.DisplayTime = 3.0f;
            settings.FadeOutTime = 1.0f;

            settings.StartingSize = 1.5f;
            settings.DisplaySize = 1.5f;
            settings.EndingSize = 0.0f;
            _titleDisplay = new VisualizerSongTitleDisplay(_spriteBatch, _spriteFont, "", "", settings);

            //Create our oscilloscope lines.
            _lines = new OscilloscopeLines(GraphicsDevice);
            _lines.Initialize(_content);
            _lines.BottomLineColor = Microsoft.Xna.Framework.Color.Teal;
            _lines.TopLineColor = Microsoft.Xna.Framework.Color.GhostWhite;
            _lines.LeftChannelSamples = _leftChannel;
            _lines.RightChannelSamples = _rightChannel;

            //Create our trails object.
            _trails = new VisualTrailsEffect(GraphicsDevice);
            _trails.LoadContent(_content);
            _trails.Settings = TrailsSettings.PresetSettings[0];
            _trails.Settings.LastFrameIntensity = 0.9f;
            _trails.Settings.SceneIntensity = 0.3f;

            //Create our render targets.
            _target = new RenderTarget2D(GraphicsDevice,
                                        720,
                                        1280,
                                        false,
                                        GraphicsDevice.PresentationParameters.BackBufferFormat,
                                        DepthFormat.None,
                                        0,
                                        RenderTargetUsage.DiscardContents);
            
            //Create our render targets.
            _target2 = new RenderTarget2D(GraphicsDevice,
                                        720,
                                        1280,
                                        false,
                                        GraphicsDevice.PresentationParameters.BackBufferFormat,
                                        DepthFormat.None,
                                        0,
                                        RenderTargetUsage.DiscardContents);

            //Hook up idle events.
            Application.Idle += delegate { Invalidate(); };
            Application.Idle += delegate { UpdateScene(); };

        }
        #endregion

        #region Update and Draw

        /// <summary>
        /// Updates the scene.
        /// </summary>
        public void UpdateScene()
        {
            //Bleed the queue if we're behind.
            long ticks_now = DateTime.Now.Ticks;
            while (_dataQueue.Count > ChunkSampleCount * 1.75f) //1.75 is arbitrary, seems to work the best though.
            {
                float val;
                _dataQueue.TryDequeue(out val);
                _dataQueue.TryDequeue(out val);
            }

            //Update our drawn values if it's time.
            if (_dataQueue.Count > kBUFFER_SIZE * 2 && ticks_now - _ticks_last_update > kTICKS_PER_BUFFER)
            {
                _ticks_last_update = ticks_now;
                for (int i = 0; i < kBUFFER_SIZE; i++)
                {
                    float val;
                    //Left channel
                    _dataQueue.TryDequeue(out val);
                    _leftChannel[i] = val;

                    //Right channel
                    _dataQueue.TryDequeue(out val);
                    _rightChannel[i] = val;
                }
            }

            _titleDisplay.Update();
        }

        /// <summary>
        /// Draw the visualizer graphics.
        /// </summary>
        protected override void Draw()
        {
            //Set render target
            GraphicsDevice.SetRenderTarget(_target);
            //Clear black
            GraphicsDevice.Clear(Microsoft.Xna.Framework.Color.Black);
            //Draw lines.
            _lines.Draw();
            //Draw trails.
            _trails.Draw(_target, _target2);

            //Draw Title Display
            _titleDisplay.Draw(new Vector2(_target2.Height / 2, _target2.Width / 2));

            //Draw to backbuffer.
            GraphicsDevice.SetRenderTarget(null);
            _spriteBatch.Begin(0, BlendState.Opaque, null, null, null, null);
            _spriteBatch.Draw(_target2, new Microsoft.Xna.Framework.Rectangle(0, 0, this.Width, this.Height), Microsoft.Xna.Framework.Color.White);
            _spriteBatch.End();
        }

        internal void UpdateTitleDisplay(string artist, string title)
        {
            _titleDisplay.SetArtistTitle(artist, title);
            _titleDisplay.Display();
        }
        #endregion
    }
}
