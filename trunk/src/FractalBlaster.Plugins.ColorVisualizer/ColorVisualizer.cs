using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FractalBlaster.Universe;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace FractalBlaster.Plugins.ColorVisualizer
{
    [PluginAttribute(Name = "Color Visualizer", Author = "Fractal Blasters", Description = "Displays Color Based Visualizer")]
    public class ColorVisualizerPlugin : IEffectPlugin
    {
        AppContext context;
        ColorVisualizerUI UI;

        public ColorVisualizerPlugin()
        {
            UI = new ColorVisualizerUI();
        }

        #region IViewPlugin Members

        public System.Windows.Forms.Form UserInterface
        {
            get { return UI; }
        }

        #endregion

        #region IPlugin Members

        public void Initialize(AppContext context)
        {
            this.context = context;
        }

        #endregion

        #region IEffectPlugin Members

        public void ProcessStream(System.IO.MemoryStream stream)
        {
            if (Enabled == false) return;
            if (UI.Visible == false) return;
            Graphics graphics = UI.CreateGraphics();
            

            int r = 0, g = 0, b = 0;
            for (int i = 0; i < stream.Length; i++)
            {
                if (i < (stream.Length / 3))
                {
                    r += stream.ReadByte();
                }
                else if (i < (2 * stream.Length / 3))
                {
                    g += stream.ReadByte();
                }
                else
                {
                    b += stream.ReadByte();
                }
            }
            Color c = Color.FromArgb(r%255, g%255, b%255);
            graphics.FillRectangle(new SolidBrush(c), 0, 0, UI.Width, UI.Height);

        }

        public bool Enabled { get; set; }

        #endregion
    }
}
