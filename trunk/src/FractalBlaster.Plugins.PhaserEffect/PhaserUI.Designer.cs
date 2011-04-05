namespace FractalBlaster.Plugins.PhaserEffect
{
    partial class PhaserUI
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.trkWet = new System.Windows.Forms.TrackBar();
            this.lblWet = new System.Windows.Forms.Label();
            this.lblDry = new System.Windows.Forms.Label();
            this.trkDry = new System.Windows.Forms.TrackBar();
            this.lblFeedback = new System.Windows.Forms.Label();
            this.trkFeedback = new System.Windows.Forms.TrackBar();
            this.lblSweep = new System.Windows.Forms.Label();
            this.trkSweep = new System.Windows.Forms.TrackBar();
            this.lblRate = new System.Windows.Forms.Label();
            this.trkRate = new System.Windows.Forms.TrackBar();
            this.lblFrequency = new System.Windows.Forms.Label();
            this.trkFreq = new System.Windows.Forms.TrackBar();
            ((System.ComponentModel.ISupportInitialize)(this.trkWet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkDry)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkFeedback)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkSweep)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkRate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkFreq)).BeginInit();
            this.SuspendLayout();
            // 
            // trkWet
            // 
            this.trkWet.Location = new System.Drawing.Point(12, 25);
            this.trkWet.Maximum = 20;
            this.trkWet.Name = "trkWet";
            this.trkWet.Size = new System.Drawing.Size(260, 45);
            this.trkWet.TabIndex = 0;
            this.trkWet.Value = 20;
            this.trkWet.Scroll += new System.EventHandler(this.trkWet_Scroll);
            // 
            // lblWet
            // 
            this.lblWet.AutoSize = true;
            this.lblWet.Location = new System.Drawing.Point(12, 9);
            this.lblWet.Name = "lblWet";
            this.lblWet.Size = new System.Drawing.Size(69, 13);
            this.lblWet.TabIndex = 1;
            this.lblWet.Text = "Wet Value: 1";
            // 
            // lblDry
            // 
            this.lblDry.AutoSize = true;
            this.lblDry.Location = new System.Drawing.Point(12, 60);
            this.lblDry.Name = "lblDry";
            this.lblDry.Size = new System.Drawing.Size(65, 13);
            this.lblDry.TabIndex = 3;
            this.lblDry.Text = "Dry Value: 1";
            // 
            // trkDry
            // 
            this.trkDry.Location = new System.Drawing.Point(12, 76);
            this.trkDry.Maximum = 20;
            this.trkDry.Name = "trkDry";
            this.trkDry.Size = new System.Drawing.Size(260, 45);
            this.trkDry.TabIndex = 2;
            this.trkDry.Value = 20;
            this.trkDry.Scroll += new System.EventHandler(this.trkDry_Scroll);
            // 
            // lblFeedback
            // 
            this.lblFeedback.AutoSize = true;
            this.lblFeedback.Location = new System.Drawing.Point(12, 111);
            this.lblFeedback.Name = "lblFeedback";
            this.lblFeedback.Size = new System.Drawing.Size(112, 13);
            this.lblFeedback.TabIndex = 5;
            this.lblFeedback.Text = "Feedback Value: 0.20";
            // 
            // trkFeedback
            // 
            this.trkFeedback.Location = new System.Drawing.Point(12, 127);
            this.trkFeedback.Maximum = 20;
            this.trkFeedback.Name = "trkFeedback";
            this.trkFeedback.Size = new System.Drawing.Size(260, 45);
            this.trkFeedback.TabIndex = 4;
            this.trkFeedback.Value = 4;
            this.trkFeedback.Scroll += new System.EventHandler(this.trkFeedback_Scroll);
            // 
            // lblSweep
            // 
            this.lblSweep.AutoSize = true;
            this.lblSweep.Location = new System.Drawing.Point(12, 162);
            this.lblSweep.Name = "lblSweep";
            this.lblSweep.Size = new System.Drawing.Size(87, 13);
            this.lblSweep.TabIndex = 7;
            this.lblSweep.Text = "Sweep Range: 4";
            // 
            // trkSweep
            // 
            this.trkSweep.Location = new System.Drawing.Point(12, 178);
            this.trkSweep.Maximum = 20;
            this.trkSweep.Name = "trkSweep";
            this.trkSweep.Size = new System.Drawing.Size(260, 45);
            this.trkSweep.TabIndex = 6;
            this.trkSweep.Value = 8;
            this.trkSweep.Scroll += new System.EventHandler(this.trkSweep_Scroll);
            // 
            // lblRate
            // 
            this.lblRate.AutoSize = true;
            this.lblRate.Location = new System.Drawing.Point(12, 213);
            this.lblRate.Name = "lblRate";
            this.lblRate.Size = new System.Drawing.Size(78, 13);
            this.lblRate.TabIndex = 9;
            this.lblRate.Text = "Sweep Rate: 1";
            // 
            // trkRate
            // 
            this.trkRate.Location = new System.Drawing.Point(12, 229);
            this.trkRate.Maximum = 15;
            this.trkRate.Name = "trkRate";
            this.trkRate.Size = new System.Drawing.Size(260, 45);
            this.trkRate.TabIndex = 8;
            this.trkRate.Value = 10;
            this.trkRate.Scroll += new System.EventHandler(this.trkRate_Scroll);
            // 
            // lblFrequency
            // 
            this.lblFrequency.AutoSize = true;
            this.lblFrequency.Location = new System.Drawing.Point(12, 264);
            this.lblFrequency.Name = "lblFrequency";
            this.lblFrequency.Size = new System.Drawing.Size(81, 13);
            this.lblFrequency.TabIndex = 11;
            this.lblFrequency.Text = "Frequency: 100";
            // 
            // trkFreq
            // 
            this.trkFreq.Location = new System.Drawing.Point(12, 280);
            this.trkFreq.Maximum = 16;
            this.trkFreq.Name = "trkFreq";
            this.trkFreq.Size = new System.Drawing.Size(260, 45);
            this.trkFreq.TabIndex = 10;
            this.trkFreq.Value = 11;
            this.trkFreq.Scroll += new System.EventHandler(this.trkFreq_Scroll);
            // 
            // PhaserUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 347);
            this.Controls.Add(this.lblFrequency);
            this.Controls.Add(this.trkFreq);
            this.Controls.Add(this.lblRate);
            this.Controls.Add(this.trkRate);
            this.Controls.Add(this.lblSweep);
            this.Controls.Add(this.trkSweep);
            this.Controls.Add(this.lblFeedback);
            this.Controls.Add(this.trkFeedback);
            this.Controls.Add(this.lblDry);
            this.Controls.Add(this.trkDry);
            this.Controls.Add(this.lblWet);
            this.Controls.Add(this.trkWet);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "PhaserUI";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "PhaserUI";
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.WaveViewPluginUI_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.WaveViewPluginUI_MouseMove);
            ((System.ComponentModel.ISupportInitialize)(this.trkWet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkDry)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkFeedback)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkSweep)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkRate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkFreq)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TrackBar trkWet;
        private System.Windows.Forms.Label lblWet;
        private System.Windows.Forms.Label lblDry;
        private System.Windows.Forms.TrackBar trkDry;
        private System.Windows.Forms.Label lblFeedback;
        private System.Windows.Forms.TrackBar trkFeedback;
        private System.Windows.Forms.Label lblSweep;
        private System.Windows.Forms.TrackBar trkSweep;
        private System.Windows.Forms.Label lblRate;
        private System.Windows.Forms.TrackBar trkRate;
        private System.Windows.Forms.Label lblFrequency;
        private System.Windows.Forms.TrackBar trkFreq;
    }
}