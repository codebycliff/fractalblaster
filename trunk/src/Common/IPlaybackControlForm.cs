using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FractalBlaster.Universe
{
    public interface IPlaybackControlForm : IPlugin
    {
        Form form
        {
            get;
        }
        IPlaybackControl playbackControl
        {
            set;
        }
        IPlaylist playlist
        {
            set;
        }
        bool isPlaying
        {
            set;
        }
    }
}
