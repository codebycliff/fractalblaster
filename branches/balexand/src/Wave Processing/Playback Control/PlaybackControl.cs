using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FractalBlaster.Universe;
using System.Timers;

namespace FractalBlaster.PlaybackControl
{
    class PlaybackControl : IPlaybackControl
    {
        IInput mInput;
        IOutput mOutput;
        IPlaylist mPlaylist;
        IPlaybackControlForm mPlaybackControlForm;
        enum state {Playing, Paused, Stopped};
        state playbackState;
        int playbackTime;
        Timer playbackTimer;

        public PlaybackControl()
        {
            playbackState = state.Stopped;
            playbackTimer = new Timer(1000);
            playbackTimer.Enabled = false;
            playbackTimer.Elapsed += new ElapsedEventHandler(playbackTimer_Elapsed);
        }

        void playbackTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            playbackTime++;
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

        public IPlaybackControlForm playbackControlForm
        {
            set { mPlaybackControlForm = value; }
        }

        public Int32 volume
        {
            set { mOutput.Volume = value; }
        }

        public void Play()
        {
            mPlaybackControlForm.isPlaying = true;
            playbackTimer.Enabled = true;

            switch (playbackState)
            {
                case state.Paused:
                    mOutput.Resume();
                    playbackState = state.Playing;
                    break;
                case state.Playing:
                    break;
                case state.Stopped:
                    playbackTime = 0;
                    mInput.Open(mPlaylist.getCurrent().Info.FullName);
                    mOutput.Play();
                    playbackState = state.Playing;
                    break;
            }

        }

        public void Pause()
        {
            playbackTimer.Enabled = false;
            mPlaybackControlForm.isPlaying = false;
            mOutput.Pause();
        }

        public void Stop()
        {
            playbackTimer.Enabled = false;
            playbackTime = 0;
            mPlaybackControlForm.isPlaying = false;
            switch (playbackState)
            {
                case state.Paused:
                    mOutput.Stop();
                    mInput.Close();
                    break;
                case state.Playing:
                    mOutput.Stop();
                    mInput.Close();
                    break;
                case state.Stopped:
                    break;
            }
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
            Stop();
            MediaFile newMediaFile = new MediaFile(filename);
            mPlaylist.clear();
            mPlaylist.add(newMediaFile);

        }

        public void Add(string filename)
        {
            MediaFile newMediaFile = new MediaFile(filename);
            mPlaylist.add(newMediaFile);
        }

        public void PlaybackComplete()
        {
            Stop();
        }

        public int getPlaybackTime()
        {
            return playbackTime;
        }

        public double getSongLength()
        {
            if (mPlaylist.getCurrent() == null)
            {
                return 0;
            }
            return mPlaylist.getCurrent().Metadata.Duration.TotalSeconds;
        }

        #endregion
    }
}
