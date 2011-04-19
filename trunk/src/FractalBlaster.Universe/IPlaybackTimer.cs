using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;

namespace FractalBlaster.Universe {

    /// <remarks>
    /// Interface that specifies the required properties and methods for any
    /// implemented playback timer.
    /// </remarks>
    public interface IPlaybackTimer {

        /// <summary>
        /// Gets the current time elapsed by the playback timer.
        /// </summary>
        /// <value>
        /// The current time.
        /// </value>
        Int32 CurrentTime { get; set; }

        /// <summary>
        /// Gets the timer for this playback timer.
        /// </summary>
        Timer Timer { get;  }
    
    }

}
