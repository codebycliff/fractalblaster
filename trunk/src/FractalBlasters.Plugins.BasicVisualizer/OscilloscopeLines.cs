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
    /// OscilloscopeLines draws the PCM data of the playing sound.
    /// </summary>
    class OscilloscopeLines
    {
        #region Fields
        /// <summary>
        /// Shader used to draw lines.
        /// </summary>
        Effect shader;

        /// <summary>
        /// VertexBuffer for left sound channel line.
        /// </summary>
        DynamicVertexBuffer leftVertBuffer;

        /// <summary>
        /// VertexBuffer for right sound channel line.
        /// </summary>
        DynamicVertexBuffer rightVertBuffer;

        /// <summary>
        /// Left Channel Vertices.
        /// </summary>
        VisualizerVertex[] leftChannelVerts;

        /// <summary>
        /// Right Channel Vertices.
        /// </summary>
        VisualizerVertex[] rightChannelVerts;

        /// <summary>
        /// Color of top line.
        /// </summary>
        Color _topLineColor = Color.Black;

        /// <summary>
        /// Color of bottom line.
        /// </summary>
        Color _bottomLineColor = Color.Black;

        /// <summary>
        /// GraphicsDevice for drawing.
        /// </summary>
        GraphicsDevice _graphics;

        /// <summary>
        /// Current data to draw for left channel.
        /// </summary>
        float[] _currentLeftChannel;

        /// <summary>
        /// Current data to draw for right channel.
        /// </summary>
        float[] _currentRightChannel;

        #endregion

        #region Properties
        public Color TopLineColor
        {
            get { return _topLineColor; }
            set { _topLineColor = value; }
        }

        public Color BottomLineColor
        {
            get { return _bottomLineColor; }
            set { _bottomLineColor = value; }
        }

        public float[] LeftChannelSamples
        {
            set { _currentLeftChannel = value; }
        }

        public float[] RightChannelSamples
        {
            set { _currentRightChannel = value; }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Constructs an OscilloscopeLines object.
        /// </summary>
        /// <param name="device">GraphicsDevice used to draw.</param>
        public OscilloscopeLines(GraphicsDevice device)
        {
            _graphics = device;
        }
        #endregion

        #region Initialize

        /// <summary>
        /// Initializes the OscilloscopeLines object.
        /// </summary>
        /// <param name="content">ContentManager to load shaders.</param>
        public void Initialize(ContentManager content)
        {
            //Load shaders.
            try
            {
                shader = content.Load<Effect>("./Plugins/BasicVisualizer/Shaders/VisualizerShader");
            }
            catch (Exception e)
            {

            }

            //Create buffers.
            leftVertBuffer = new DynamicVertexBuffer(_graphics,
                                         VisualizerVertex.VertexDeclaration,
                                         VisualizerGraphicsControl.kBUFFER_SIZE,
                                         BufferUsage.WriteOnly);
            rightVertBuffer = new DynamicVertexBuffer(_graphics,
                                         VisualizerVertex.VertexDeclaration,
                                         VisualizerGraphicsControl.kBUFFER_SIZE,
                                         BufferUsage.WriteOnly);

            //Create vertex arrays.
            leftChannelVerts = new VisualizerVertex[VisualizerGraphicsControl.kBUFFER_SIZE];
            rightChannelVerts = new VisualizerVertex[VisualizerGraphicsControl.kBUFFER_SIZE];

            //Initialize values for vertex arrays.
            for (int i = 0; i < leftChannelVerts.Length; i++)
            {
                leftChannelVerts[i].Position = new Vector3(2 * (float)i / leftChannelVerts.Length - 1f, 0.5f, 0.0f);
                rightChannelVerts[i].Position = new Vector3(2 * (float)i / rightChannelVerts.Length - 1f, -0.5f, 0.0f);
            }

            //Initialize vertex buffers.
            leftVertBuffer.SetData(leftChannelVerts, 0, leftChannelVerts.Length, SetDataOptions.NoOverwrite);
            rightVertBuffer.SetData(rightChannelVerts, 0, rightChannelVerts.Length, SetDataOptions.NoOverwrite);
        }
        #endregion

        #region Draw

        /// <summary>
        /// Draws this OscilloscopeLines object.
        /// </summary>
        public void Draw()
        {
            //Set the vertices
            for (int i = 0; i < VisualizerGraphicsControl.kBUFFER_SIZE; i++)
            {
                float val = _currentLeftChannel[i];

                leftChannelVerts[i].Position.Y = 0.5f * val + 0.5f;// = new Vector3(2 * (float)i / leftChannelVerts.Length - 1f, 0.25f * val + 0.5f, 0.0f);

                val = _currentRightChannel[i];
                rightChannelVerts[i].Position.Y = 0.5f * val - 0.5f;//= new Vector3(2 * (float)i / rightChannelVerts.Length - 1f, 0.25f * val - 0.5f, 0.0f);
            }

            //Set the vertex buffers.
            leftVertBuffer.SetData(leftChannelVerts, 0, leftChannelVerts.Length, SetDataOptions.NoOverwrite);
            rightVertBuffer.SetData(rightChannelVerts, 0, rightChannelVerts.Length, SetDataOptions.NoOverwrite);

            //Check for lost content.
            if (leftVertBuffer.IsContentLost)
            {
                leftVertBuffer.SetData(leftChannelVerts);
            }
            if (rightVertBuffer.IsContentLost)
            {
                rightVertBuffer.SetData(rightChannelVerts);
            }

            //Draw top line.
            _graphics.SetVertexBuffer(leftVertBuffer);
            shader.Parameters["color"].SetValue(_topLineColor.ToVector4());
            foreach (EffectTechnique tech in shader.Techniques)
            {
                foreach (EffectPass pass in tech.Passes)
                {
                    pass.Apply();
                    _graphics.DrawPrimitives(PrimitiveType.LineStrip, 0, leftVertBuffer.VertexCount - 1);
                }
            }

            //Draw bottom line
            _graphics.SetVertexBuffer(rightVertBuffer);
            shader.Parameters["color"].SetValue(_bottomLineColor.ToVector4());
            foreach (EffectTechnique tech in shader.Techniques)
            {
                foreach (EffectPass pass in tech.Passes)
                {
                    pass.Apply();
                    _graphics.DrawPrimitives(PrimitiveType.LineStrip, 0, rightVertBuffer.VertexCount - 1);
                }
            }
        }
        #endregion
    }
}
