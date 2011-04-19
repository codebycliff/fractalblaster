using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using FractalBlaster.Universe;
using FractalBlaster.Core.Runtime;
using System.Timers;

namespace FractalBlaster.Core.UI {

    /// <remarks>
    /// User control to seek the position of the audio being played.
    /// </remarks>
    public partial class SeekBar : UserControl {

        /// <summary>
        /// Initializes a new instance of the <see cref="SeekBar"/> class.
        /// </summary>
        public SeekBar() {
            Time = 0;
            IsMouseDown = false;
            InitializeComponent();

        }
        
        #region [ Properties ]

        /// <summary>
        /// Sets the user interface which represented as a 
        /// <see cref="ProductForm"/> instance.
        /// </summary>
        /// <value>
        /// The product form user interface.
        /// </value>
        public ProductForm UI { set; private get; }

        /// <summary>
        /// Gets or sets the current time position.
        /// </summary>
        /// <value>
        /// The current time.
        /// </value>
        public Int32 Time { get; set; }

        /// <summary>
        /// Gets or sets the total time.
        /// </summary>
        /// <value>
        /// The total time.
        /// </value>
        public Int32 TotalTime { get; set; }

        /// <summary>
        /// Gets or sets the input plugin responsible for inputting the audio.
        /// </summary>
        /// <value>
        /// The input plugin.
        /// </value>
        public IInputPlugin Input { get; set; }

        /// <summary>
        /// Gets or sets the output plugin responsible for outputting the audio.
        /// </summary>
        /// <value>
        /// The output plugin.
        /// </value>
        public IOutputPlugin Output { get; set; }

        /// <summary>
        /// Gets or sets the playback timer for the audio file.
        /// </summary>
        /// <value>
        /// The playback timer.
        /// </value>
        public IPlaybackTimer PlaybackTimer { get; set; }
        
        #endregion    
        
        #region [ Private ]

        /// <summary>
        /// Handles the Paint event of the SeekBar control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.PaintEventArgs"/> instance containing the event data.</param>
        private void SeekBar_Paint(object sender, PaintEventArgs e) {
            GraphicsPath seekBorder = new GraphicsPath();
            Rectangle seekBarRect = new Rectangle(1, 5, 200, 10);
            seekBorder.AddRectangle(seekBarRect);
            Region outsideBorder = new Region(this.Bounds);
            outsideBorder.Exclude(seekBorder);
            e.Graphics.DrawPath(Pens.Black, seekBorder);
            e.Graphics.SmoothingMode = SmoothingMode.HighQuality;

            if (TotalTime != 0 && Time < TotalTime && Time >= 0) {
                Rectangle seekRectangle = new Rectangle((Time * 200 / TotalTime) - 6, 3, 14, 14);
                e.Graphics.FillEllipse(Brushes.DarkRed, seekRectangle);

                if (IsMouseDown) {
                    Rectangle newSeekRect = new Rectangle(MouseX - 6, 3, 14, 14);
                    e.Graphics.FillEllipse(Brushes.IndianRed, newSeekRect);
                }
            }

            if (Time >= TotalTime) {
                PlaybackTimer.Timer.Stop();
                Time = 0;

                // Shift to next track if available
                UI.SkipMediaForward(this, new EventArgs());
            }
            if (Time < 0) {
                Time = 0;
            }

            label1.Text = String.Format("{0:d2}:{1:d2}/{2:d2}:{3:d2}",
                Time / 60, Time % 60, TotalTime / 60, TotalTime % 60);
        }

        /// <summary>
        /// Handles the MouseDown event of the SeekBar control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
        private void SeekBar_MouseDown(object sender, MouseEventArgs e) {
            if (FamilyKernel.Instance.Context.Engine.IsMediaLoaded) {
                IsMouseDown = true;
                MouseX = e.X;
            }
        }

        /// <summary>
        /// Handles the MouseUp event of the SeekBar control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
        private void SeekBar_MouseUp(object sender, MouseEventArgs e) {
            if (IsMouseDown == true) {
                if (e.X < 0)
                    MouseX = 0;
                if (e.X > 200)
                    MouseX = 200;
                IsMouseDown = false;
                Debug.printline("seek(" + (MouseX * TotalTime / 200).ToString() + ")");
                Output.Stop();
                Input.Seek(MouseX * TotalTime / 200);
                Output.Play();
                PlaybackTimer.CurrentTime = MouseX * TotalTime / 200;
            }
        }

        /// <summary>
        /// Handles the MouseMove event of the SeekBar control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
        private void SeekBar_MouseMove(object sender, MouseEventArgs e) {
            this.Invalidate();
            MouseX = e.X;
            if (e.X < 0)
                MouseX = 0;
            if (e.X > 200)
                MouseX = 200;
            //MouseX = e.X;
        }
        
        /// <summary>
        /// Private property indicating whether the mouse is currently down.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is mouse down; otherwise, <c>false</c>.
        /// </value>
        private Boolean IsMouseDown { get; set; }

        /// <summary>
        /// Private property containing the x-coordination of the mouse.
        /// </summary>
        /// <value>
        /// The mouse X.
        /// </value>
        private Int32 MouseX { get; set; }

        #endregion

    }
}
