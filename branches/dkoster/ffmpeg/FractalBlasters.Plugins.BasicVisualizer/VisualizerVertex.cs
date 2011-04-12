using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FractalBlaster.Plugins.BasicVisualizer
{
    /// <summary>
    /// Struct for VisualizerVertex format.
    /// </summary>
    struct VisualizerVertex
    {
        /// <summary>
        /// Position of the vertex.
        /// </summary>
        public Vector3 Position;

        /// <summary>
        /// VertexDeclaration object.
        /// </summary>
        public static readonly VertexDeclaration VertexDeclaration = new VertexDeclaration(

            new VertexElement(0,VertexElementFormat.Vector3,VertexElementUsage.Position,0)

        );

        /// <summary>
        /// Size in bytes of each vertex.
        /// </summary>
        public const int sizeInBytes = 12;
    }
}
