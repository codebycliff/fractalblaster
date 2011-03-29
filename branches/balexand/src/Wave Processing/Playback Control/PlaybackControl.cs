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
        string mFilename;

        #region IPlaybackControl Members

        public IInput input
        {
            set { mInput = value; }
        }

        public IOutput output
        {
            set { mOutput = value; }
        }

        public void Play()
        {
            mOutput.Play();
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
            mInput.Open(filename);
        }

        #endregion
    }
}
