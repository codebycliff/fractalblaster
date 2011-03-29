using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FractalBlaster.Universe;

namespace FractalBlaster.PlaybackControl
{
    class PlaybackControl : IPlaybackControl
    {
        IInput mInput;
        IOutput mOutput;
        IPlaylist mPlaylist;
        string mFilename;
        bool fileOpen;
        enum state {Playing, Paused, Stopped};
        state playbackState;

        public PlaybackControl()
        {
            fileOpen = false;
            playbackState = state.Stopped;
        }

        #region IPlaybackControl Members

        public IInput input
        {
            set { mInput = value; }
        }

        public IOutput output
        {
            set { mOutput = value; }
        }

        public IPlaylist playlist
        {
            set { mPlaylist = value; }
        }

        public Int32 volume
        {
            set 
            {
                Debug.printline("volume = " + value.ToString());
                mOutput.Volume = value;
            }
        }

        public void Play()
        {
            switch (playbackState)
            {
                case state.Paused:
                    mOutput.Resume();
                    break;
                case state.Playing:
                    break;
                case state.Stopped:
                    if (fileOpen)
                    {
                        mOutput.Play();
                    }
                    break;
            }

        }

        public void Pause()
        {
            mOutput.Pause();
        }

        public void Stop()
        {
            mOutput.Stop();
            mInput.Close();
            mInput.Open(mFilename);
        }

        public void Next()
        {

        }

        public void Previous()
        {

        }

        public System.IO.MemoryStream GetFrames()
        {
            throw new NotImplementedException();
        }

        public void Open(string filename)
        {
            mFilename = filename;
            MediaFile newMediaFile = new MediaFile(filename);
            mPlaylist.add(newMediaFile);
            mInput.Open(filename);
            fileOpen = true;
        }

        #endregion
    }
}
