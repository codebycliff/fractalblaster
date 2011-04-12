using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FractalBlaster.Universe;
using System.Timers;

namespace FractalBlaster.Core.Runtime
{
    class PlaybackTimer : IPlaybackTimer
    {

        Timer mTimer;
        int mCurrentTime;

        public PlaybackTimer()
        {
            mCurrentTime = 0;
            mTimer = new Timer(1000);
            mTimer.Elapsed += new ElapsedEventHandler(mTimer_Elapsed);
        }

        void mTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            mCurrentTime++;
        }

        #region IPlaybackTimer Members

        public int currentTime
        {
            get
            {
                return mCurrentTime;
            }
            set
            {
                mCurrentTime = value;
            }
        }

        public void timerStart()
        {
            mTimer.Start();
        }

        public void timerStop()
        {
            mTimer.Stop();
        }

        #endregion
    }
}
