namespace FractalBlaster.Plugins.BasicVisualizer
{
    partial class VisualizerForm
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
            this.visualizerGraphicsControl1 = new FractalBlaster.Plugins.BasicVisualizer.VisualizerGraphicsControl();
            this.SuspendLayout();
            // 
            // visualizerGraphicsControl1
            // 
            this.visualizerGraphicsControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.visualizerGraphicsControl1.Location = new System.Drawing.Point(0, 0);
            this.visualizerGraphicsControl1.Name = "visualizerGraphicsControl1";
            this.visualizerGraphicsControl1.Size = new System.Drawing.Size(558, 335);
            this.visualizerGraphicsControl1.TabIndex = 0;
            this.visualizerGraphicsControl1.Text = "visualizerGraphicsControl1";
            // 
            // VisualizerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(558, 335);
            this.Controls.Add(this.visualizerGraphicsControl1);
            this.Name = "VisualizerForm";
            this.ShowInTaskbar = false;
            this.Text = "Visualizer";
            this.Load += new System.EventHandler(this.VisualizerForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private VisualizerGraphicsControl visualizerGraphicsControl1;
    }
}