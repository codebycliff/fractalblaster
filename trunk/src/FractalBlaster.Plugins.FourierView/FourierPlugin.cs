using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using FractalBlaster.Universe;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace FractalBlaster.Plugins.FourierView
{
    [PluginAttribute(Name = "Fourier View", Author = "Fractal Blasters", Description = "Displays Fourier Transform")]
    public class FourierPlugin : IViewPlugin, IEffectPlugin
    {
        AppContext context;
        FourierPluginUI UI;

        public FourierPlugin()
        {
            UI = new FourierPluginUI();
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

        public void ProcessStream(MemoryStream stream)
        {
            float[] data = new float[(int)Math.Pow(2,10)];
            BinaryReader reader = new BinaryReader(stream);
            for (int i = 0; i < data.Length;i++ )
            {
                data[i] = (stream.ReadByte() << 8) | stream.ReadByte();
            }
            //Exocortex.DSP.Fourier.RFFT(data, data.Length, Exocortex.DSP.FourierDirection.Forward);
            float[] fft = new float[1024];
            float dc;
            Exocortex.DSP.Fourier.ComputeFFTPolarMag(fft, data, out dc);
            updateUI(fft);
        }

        public bool Enabled { get; set; }

        private void updateUI(float[] data)
        {
            if (Enabled == false) return;
            if (UI.Visible == false) return;

            UI.Width = data.Length/2;

            Graphics graphics = UI.CreateGraphics();
            graphics.FillRectangle(new SolidBrush(Color.Black), 0, 0, UI.Width, UI.Height);
            float max = 1;
            /*
            float[] power = new float[data.Length / 2];

            for (int i = 1; i < data.Length/2; i+=2)
            {
                power[i] = (float)Math.Sqrt(Math.Pow(data[i], 2) + Math.Pow(data[i+1],2));
                if(power[i] > max)
                {
                    max = power[i];
                }
            }
             * */
            for (int i = 0; i < data.Length/2; i++)
            {
                if (data[i] > max)
                    max = data[i];
            }


            max /= UI.Height;

            float x1 =0, x2 =0, y1=0, y2=0;
            GraphicsPath myPath = new GraphicsPath();
            for (int i = 0; i < data.Length/2-1; i++)
            {
                x1 = i;
                y1 = data[i] / max;
                x2 = i+1;
                y2 = data[i + 1] / max;
                myPath.AddLine(x1, y1, x2, y2);
            }

            graphics.DrawPath(new Pen(Color.Blue), myPath);
        }

        #endregion
    }
}
