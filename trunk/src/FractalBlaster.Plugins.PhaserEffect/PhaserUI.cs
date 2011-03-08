using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FractalBlaster.Plugins.PhaserEffect
{
    public partial class PhaserUI : Form
    {
        private PhaserPlugin plugin;
        Point mouse_offset;

        public PhaserUI(PhaserPlugin p)
        {
            plugin = p;
            InitializeComponent();

            plugin.m_Wet = (trkWet.Value / 10.0f) -1;
            plugin.m_Dry = (trkDry.Value / 10.0f) -1;
            plugin.m_Feedback = (trkFeedback.Value / 10.0f) -1;
            plugin.m_SweepRange = (trkSweep.Value / 2.0f);
            plugin.m_SweepRate = (trkRate.Value / 10.0f);
            plugin.m_Frequency = ((trkFreq.Value - 1) * 10.0f);

            plugin.UpdateParams();
        }

        private void trkWet_Scroll(object sender, EventArgs e)
        {
            lblWet.Text = "Wet Value: " + (trkWet.Value / 10.0f - 1).ToString("F2");
            plugin.m_Wet = (trkWet.Value / 10.0f) -1;
            plugin.UpdateParams();
        }

        private void trkDry_Scroll(object sender, EventArgs e)
        {
            lblDry.Text = "Dry Value: " + (trkDry.Value / 10.0f - 1).ToString("F2");
            plugin.m_Dry = (trkDry.Value / 10.0f) -1;
            plugin.UpdateParams();
        }

        private void trkFeedback_Scroll(object sender, EventArgs e)
        {
            lblFeedback.Text = "Feedback Value: " + (trkFeedback.Value / 10.0f -1).ToString("F2");
            plugin.m_Feedback = (trkFeedback.Value / 10.0f) -1;
            plugin.UpdateParams();
        }

        private void trkSweep_Scroll(object sender, EventArgs e)
        {
            lblSweep.Text = "Sweep Range: " + (trkSweep.Value / 2.0f).ToString("F2");
            plugin.m_SweepRange = (trkSweep.Value / 2.0f);
            plugin.UpdateParams();
        }

        private void trkRate_Scroll(object sender, EventArgs e)
        {
            lblRate.Text = "Sweep Rate: " + (trkRate.Value / 10.0f).ToString("F2");
            plugin.m_SweepRate = (trkRate.Value / 10.0f);
            plugin.UpdateParams();
        }

        private void trkFreq_Scroll(object sender, EventArgs e)
        {
            lblFrequency.Text = "Frequency: " + ((trkFreq.Value - 1) *10.0f).ToString("F2");
            plugin.m_Frequency = ((trkFreq.Value - 1) * 10.0f);
            plugin.UpdateParams();
        }

        private void WaveViewPluginUI_MouseDown(object sender, MouseEventArgs e)
        {
            mouse_offset = new Point(-e.X, -e.Y);
        }

        private void WaveViewPluginUI_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                Point mousePos = Control.MousePosition;
                mousePos.Offset(mouse_offset);
                this.Location = mousePos;
            }
        }
    }
}
