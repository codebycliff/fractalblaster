using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace FractalBlaster.Plugins.BasicVisualizer
{
    /// <summary>
    /// Trails post processing effect
    /// </summary>
    public class VisualTrailsEffect
    {
        #region Fields

        /// <summary>
        /// Whether the effect is active
        /// </summary>
        private bool isActive;

        /// <summary>
        /// Settings in use by the effect
        /// </summary>
        private TrailsSettings settings;

        /// <summary>
        /// Contains the last frame drawn
        /// </summary>
        private RenderTarget2D lastFrame;

        /// <summary>
        /// Trail combine shader
        /// </summary>
        private Effect combine;

        /// <summary>
        /// Reference to the graphics device
        /// </summary>
        private GraphicsDevice device;

        /// <summary>
        /// Spritebatch used for drawing full screen quads.
        /// </summary>
        private SpriteBatch spriteBatch;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets whether the effect is active
        /// </summary>
        public bool IsActive
        {
            get { return isActive; }
            set { isActive = value; }
        }

        /// <summary>
        /// Gets or sets parameters controlling the trails effect
        /// </summary>
        public TrailsSettings Settings
        {
            get { return settings; }
            set { settings = value; }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Constructs a new trails post processing effect component
        /// </summary>
        /// <param name="manager">Post processing effect manager</param>
        public VisualTrailsEffect(GraphicsDevice device)
        {
            this.device = device;

            try
            {
                spriteBatch = new SpriteBatch(device);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message + "fejafiea");
            }
        }

        #endregion

        #region Load and Unload

        /// <summary>
        /// Loads content for the trails component
        /// </summary>
        /// <param name="content">Content manager</param>
        public void LoadContent(ContentManager content)
        {
            settings = TrailsSettings.PresetSettings[0];

            combine = content.Load<Effect>("./Plugins/BasicVisualizer/Shaders/TrailCombine");
            PresentationParameters pp = device.PresentationParameters;

            int width = 1280;
            int height = 800;

            SurfaceFormat format = pp.BackBufferFormat;

            lastFrame = new RenderTarget2D(device,
                width,
                height,
                false,
                format,
                DepthFormat.None,
                0,
                RenderTargetUsage.DiscardContents);
        }

        /// <summary>
        /// Unloads unmanaged resources
        /// </summary>
        public void UnloadContent()
        {
            combine.Dispose();
            lastFrame.Dispose();
        }

        #endregion

        #region Draw

        /// <summary>
        /// Applies the trails effect
        /// </summary>
        /// <param name="source">Source target</param>
        /// <param name="intermediate1">Unused</param>
        /// <param name="intermediate2">Unused</param>
        /// <param name="destination">Destination target, null for back buffer</param>
        public void Draw(RenderTarget2D source, RenderTarget2D destination)
        {
            device.SamplerStates[1] = SamplerState.LinearClamp;

            //Pass 1: Combine current screen with the last frame.

            //Set parameters
            EffectParameterCollection parameters = combine.Parameters;
            parameters["LastFrameIntensity"].SetValue(settings.LastFrameIntensity);
            parameters["SceneIntensity"].SetValue(settings.SceneIntensity);
            //parameters["threshold"].SetValue(0.015f);

            //Set render target to the currentFrame
            device.SetRenderTarget(destination);
            //Combine with sceneTarget
            device.Textures[1] = source;

            //Draw
            Utilities.DrawFullScreenQuad(device,spriteBatch,lastFrame, destination, combine);

            //Finally, save our new currentFrame with lastFrame.
            if (destination != null)
            {
                //Copy texture
                Utilities.DrawFullScreenQuad(device,spriteBatch,destination, lastFrame, null);
            }
            else
            {
                //Can't do this.
            }
        }
        #endregion
    }
}
