using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FractalBlaster.Plugins.BasicVisualizer
{
    public class Utilities
    {
        /// <summary>
        /// Renders a texture to the desired target with the applied effect,
        /// used to draw with post processing
        /// </summary>
        /// <param name="texture">Source texture or render target</param>
        /// <param name="renderTarget">Destination render target, null for the back buffer</param>
        /// <param name="effect">Effect to apply, if any</param>
        public static void DrawFullScreenQuad(GraphicsDevice device,SpriteBatch spriteBatch, Texture2D texture, RenderTarget2D renderTarget, Effect effect)
        {
            device.SetRenderTarget(renderTarget);

            if (renderTarget != null)
                DrawFullScreenQuad(spriteBatch, texture, renderTarget.Width, renderTarget.Height, effect);
            else
                DrawFullScreenQuad(spriteBatch, texture, device.Viewport.Width, device.Viewport.Height, effect);
        }

        /// <summary>
        /// Renders a texture to the current active render target with the applied effect,
        /// used to draw with post processing
        /// </summary>
        /// <param name="texture">Source texture or render target</param>
        /// <param name="width">Width of the target</param>
        /// <param name="height">Height of the target</param>
        /// <param name="effect">Effect to apply while drawing</param>
        public static void DrawFullScreenQuad(SpriteBatch spriteBatch, Texture2D texture, int width, int height, Effect effect)
        {
            spriteBatch.Begin(0, BlendState.Opaque, null, null, null, effect);
            spriteBatch.Draw(texture, new Rectangle(0, 0, width, height), Color.White);
            spriteBatch.End();
        }
    }
}
