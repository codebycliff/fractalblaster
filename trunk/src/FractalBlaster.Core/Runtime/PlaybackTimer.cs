using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FractalBlaster.Universe;
using System.Timers;

namespace FractalBlaster.Core.Runtime {

    /// <remarks>
    /// Timer for keeping track of the playback.
    /// </remarks>
    public class PlaybackTimer : IPlaybackTimer {

        /// <summary>
        /// Gets the timer instance.
        /// </summary>
        public Timer Timer { get; private set; }

        /// <summary>
        /// Gets the current Time according to the timer.
        /// </summary>
        public int CurrentTime { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PlaybackTimer"/> class.
        /// </summary>
        public PlaybackTimer() {
            CurrentTime = 0;
            Timer = new Timer(1000);
            Timer.Elapsed += new ElapsedEventHandler(TimeElapsed);
        }

        /// <summary>
        /// Event handler for Time elapsing on the Timer.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The <see cref="System.Timers.ElapsedEventArgs"/> instance containing the event data.</param>
        private void TimeElapsed(object sender, ElapsedEventArgs e) {
            CurrentTime++;
        }
    
    }
}
