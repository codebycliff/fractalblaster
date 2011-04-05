﻿using System;
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
        IEnumerable<IWavePlugin> mWavePlugins;
        enum state {Playing, Paused, Stopped};
        state playbackState;
        int playbackTime;
        Timer playbackTimer;
        MediaFile currentFile;

        List<string> fileFormats;

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

        public IEnumerable<IWavePlugin> wavePlugins
        {
            set { mWavePlugins = value; }
        }

        public Int32 volume
        {
            set { mOutput.Volume = value; }
        }

        public void Play()
        {
            mPlaybackControlForm.isPlaying = true;
            playbackTimer.Enabled = true;

            foreach (IWavePlugin wp in mWavePlugins)
            {
                wp.Start();
            }

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
                    currentFile = mPlaylist.getCurrent();
                    mInput.Open(currentFile.Info.FullName);
                    mOutput.Play();
                    playbackState = state.Playing;
                    break;
            }

        }

        public void Pause()
        {
            foreach (IWavePlugin wp in mWavePlugins)
            {
                wp.Stop();
            }
            playbackTimer.Enabled = false;
            mPlaybackControlForm.isPlaying = false;
            mOutput.Pause();
            playbackState = state.Paused;
        }

        public void Stop()
        {
            foreach (IWavePlugin wp in mWavePlugins)
            {
                wp.Stop();
            }
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
            playbackState = state.Stopped;
        }

        public void Next()
        {

        }

        public void Previous()
        {

        }

        public System.IO.MemoryStream GetFrames(int numFramesToRead)
        {
            System.IO.MemoryStream myStream = mInput.GetFrames(numFramesToRead);
            if (myStream == null) return null;
            foreach (IWavePlugin wp in mWavePlugins)
            {
                myStream.Seek(0, 0);
                wp.ProcessStream(myStream);
            }
            myStream.Seek(0, 0);
            return myStream;
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
            if (currentFile == null)
            {
                return 0;
            }
            return currentFile.Metadata.Duration.TotalSeconds;
        }

        #endregion
    }
}