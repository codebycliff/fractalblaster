using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FractalBlaster.Universe;

namespace FractalBlaster.Plugins.PhaserEffect
{
    [PluginAttribute(Name = "Phaser Effect", Author = "Fractal Blasters", Description = "Applies Phaser Effect")]
    public class PhaserPlugin : IEffectPlugin
    {
        private PhaserUI UI;
        private AppContext appContext;

        public float m_Dry { get; set; }	        // dry mix
        public float m_Wet { get; set; }		    // Wet mix
        public float m_Feedback { get; set; }       // Feedback gain
        public float m_SweepRate { get; set; }	    // Sweep rate (Hz / s)
        public float m_SweepRange { get; set; }	    // Sweep range (octaves)
        public float m_Frequency { get; set; }  	// Sweep frequency (Hz)

        private float m_MinWp, m_MaxWp, m_Wp;
        private float m_SamplingRate = 44100;
        private float m_Rate;
        private float m_SweepFac;

        private float m_x1;
        private float m_x2;
        private float m_x3;
        private float m_x4;

        private float m_y1;
        private float m_y2;
        private float m_y3;
        private float m_y4;

        public void UpdateParams()
        {
            m_MinWp = (float)(Math.PI * m_Frequency / m_SamplingRate);
            double Range = Math.Pow(2.0, m_SweepRange);
            m_MaxWp = (float)(Math.PI * m_Frequency * Range / m_SamplingRate);
            m_Rate = (float)Math.Pow(Range, 2.0f * m_SweepRate / m_SamplingRate);

            m_SweepFac = m_Rate;
            m_Wp = m_MinWp;
        }

        private System.Int16 ProcessSample(System.Int16 x)
        {
            float K = (1.0f - m_Wp) / (1.0f + m_Wp);

            float x1 = x + m_Feedback * m_y4;
            m_y1 = K * (m_y1 + x1) - m_x1; // 1st filter
            m_x1 = x1;
            m_y2 = K * (m_y2 + m_y1) - m_x2; // 2nd filter
            m_x2 = m_y1;
            m_y3 = K * (m_y3 + m_y2) - m_x3; // 3rd filter
            m_x3 = m_y2;
            m_y4 = K * (m_y4 + m_y3) - m_x4; // 4th filter
            m_x4 = m_y3;

            float y = m_y4 * m_Wet + x * m_Dry;

            m_Wp *= m_SweepFac;
            if (m_Wp > m_MaxWp)
                m_SweepFac = 1.0f / m_Rate;
            else if (m_Wp < m_MinWp)
                m_SweepFac = m_Rate;

            return (System.Int16)y;
        }

        public PhaserPlugin()
        {
            UI = new PhaserUI(this);
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
            appContext = context;
        }

        #endregion

        #region IEffectPlugin Members

        public void ProcessStream(System.IO.MemoryStream stream)
        {
            UpdateParams();
            for (int i = 0; i < stream.Length; i += 2) 
            {
                byte[] buf = new byte[2];
                if (stream.Read(buf, 0, 2) <= 0)
                    break;
                System.Int16 sample = System.BitConverter.ToInt16(buf, 0);
                sample = ProcessSample(sample);
                stream.Seek(-2, System.IO.SeekOrigin.Current);
                byte[] smpl = System.BitConverter.GetBytes(sample);
                stream.Write(smpl, 0, 2);
            }
        }

        public bool Enabled
        {
            get;
            set;
        }

        #endregion
    }
}
