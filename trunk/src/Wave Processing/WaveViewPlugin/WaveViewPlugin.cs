using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading;
using System.Drawing;
using System.Drawing.Drawing2D;
using FractalBlaster.Universe;

namespace FractalBlaster.Plugins.WaveView
{
    [PluginAttribute(Name = "Wave View", Author = "Fractal Blasters", Description = "Displays Waveform")]
    public class WaveViewPlugin : IWavePlugin , IPluginForm
    {
        WaveViewPluginUI UI;
        System.IO.MemoryStream myStream;
        System.IO.MemoryStream nextStream;
        DateTime lastUpdate;
        Timer waveUpdateTimer;
        byte[] waveData;

        public WaveViewPlugin()
        {
            UI = new WaveViewPluginUI();
            lastUpdate = new DateTime(0);
            myStream = new System.IO.MemoryStream();
            nextStream = new System.IO.MemoryStream();
            waveData = new byte[441];
            for (int i = 0; i < 441; i++)
            {
                waveData[i] = 0;
            }
            waveUpdateTimer = new Timer(new TimerCallback(updateWave), null, 0, 40);
        }

        public System.Windows.Forms.Form form
        {
            get { return UI; }
        }

        public void ProcessStream(System.IO.MemoryStream stream)
        {
            myStream.Seek(0, System.IO.SeekOrigin.Begin);
            nextStream.CopyTo(myStream);
            myStream.Seek(0, System.IO.SeekOrigin.Begin);
            if (stream != null)
            {
                nextStream.Seek(0, System.IO.SeekOrigin.Begin);
                stream.CopyTo(nextStream);
                nextStream.Seek(0, System.IO.SeekOrigin.Begin);
            }
        }

        public void Start()
        {
            Enabled = true;
        }

        public void Stop()
        {
            Enabled = false;
        }

        bool Enabled;

        private void updateWave(object state)
        {
            if (Enabled == false) return;
            if (UI.Visible == false) return;
            Graphics graphics = UI.CreateGraphics();
            graphics.FillRectangle(Brushes.Black, 0, 0, 441, 100);
            byte [] buffer = new byte[16];
            for (int i = 0; i < 441; i++)
            {
                int readResult = myStream.Read(buffer, 0, 16);
                if (readResult == 0)
                {
                    break;
                }
                long sum = 0;
                for (int j = 0; j < 16; j+=2)
                {
                    sum += ((Int16)((buffer[j + 1] << 8) | (buffer[j]))) + 32768;
                }
                waveData[i] = (byte)(sum / 8.0 / 655.35);

            }
            float x1 = 0, y1 = 0, x2 = 0, y2 = 0;
            GraphicsPath myPath = new GraphicsPath();
            for (int i = 0; i < 440; i++)
            {
                x1 = i;
                y1 = waveData[i];
                x2 = i + 1;
                y2 = waveData[i + 1];
                myPath.AddLine(x1, y1, x2, y2);
            }
            graphics.DrawPath(Pens.Green, myPath);
        }
    }
}
