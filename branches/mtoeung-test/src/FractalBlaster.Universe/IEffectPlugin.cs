using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace FractalBlaster.Universe {
    
    /// <remarks>
    /// Defines the members for any plugin that wishes to provide an effect
    /// that can be applied to a media file while it is being outputted.
    /// </remarks>
    public interface IEffectPlugin : IPlugin {

        /// <summary>
        /// Method that is responsible for performing the effect by modifying
        /// the specified input stream that is assoicated with the media file
        /// that is currently being decoded.
        /// </summary>
        /// <param name="stream">
        /// Reference to the media file's input stream.
        /// </param>
        void ProcessStream(MemoryStream stream);

        /// <summary>
        /// Determines whether this effect is enabled.
        /// </summary>
        Boolean Enabled { get; set;  }

    }

}
