using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FractalBlaster.Plugins.BasicVisualizer
{
    public class VisualizerSongTitleDisplay
    {
        public enum TitleDisplayState
        {
            FadingIn,
            FadingOut,
            Displaying,
            NotDisplaying
        }
        /// <summary>
        /// Title of the song to display.
        /// </summary>
        private String _displayString;

        /// <summary>
        /// Number of ticks at the time the song title was displayed.
        /// </summary>
        private long _ticksAtTitleDisplayTime;

        /// <summary>
        /// Duration for song title to display when song is changed in ticks
        /// </summary>
        private long _titleDisplayDuration;

        /// <summary>
        /// Duration over which the title will fade in
        /// </summary>
        private long _fadeInTime;

        /// <summary>
        /// Duration over which the title will fade out.
        /// </summary>
        private long _fadeOutTime;

        /// <summary>
        /// Font with which the text will be displayed.
        /// </summary>
        private SpriteFont _font;

        private Color _displayColor;

        private float _rotation;

        private float _size;

        VisualizerDisplayTextSettings _settings;

        /// <summary>
        /// Are we dispalying?
        /// </summary>
        private TitleDisplayState _currentState = TitleDisplayState.NotDisplaying;

        /// <summary>
        /// SpriteBatch to draw with.
        /// </summary>
        private SpriteBatch _spriteBatch;

        public VisualizerSongTitleDisplay(SpriteBatch spriteBatch, SpriteFont font, string artist, string title, VisualizerDisplayTextSettings settings)
        {
            _displayString = artist + " - " + title;
            _spriteBatch = spriteBatch;
            _font = font;
            _settings = settings;
            _fadeInTime = (long)(TimeSpan.TicksPerSecond * _settings.FadeInTime);
            _fadeOutTime = (long)(TimeSpan.TicksPerSecond * _settings.FadeOutTime);
            _titleDisplayDuration = (long)(TimeSpan.TicksPerSecond * _settings.DisplayTime);
        }

        public void SetArtistTitle(string artist, string title)
        {
            _displayString = artist + " - " + title;
        }

        public void Display()
        {
            this._currentState = TitleDisplayState.FadingIn;
            _ticksAtTitleDisplayTime = DateTime.Now.Ticks;
        }

        public void Update()
        {
            long time_now = DateTime.Now.Ticks;
            long time_since_last_change = time_now - _ticksAtTitleDisplayTime;


            float progress = 0.0f;


            switch (_currentState)
            {
                case (TitleDisplayState.Displaying):
                    if (time_since_last_change > _titleDisplayDuration)
                    {
                        _currentState = TitleDisplayState.FadingOut;
                        _ticksAtTitleDisplayTime = time_now;
                    }
                    progress = (float)((double)time_since_last_change / _titleDisplayDuration);
                    break;
                case (TitleDisplayState.FadingIn):
                    if (time_since_last_change > _fadeInTime)
                    {
                        _currentState = TitleDisplayState.Displaying;
                        _ticksAtTitleDisplayTime = time_now;
                    }
                    progress = (float)((double)time_since_last_change / _fadeInTime);
                    break;
                case (TitleDisplayState.FadingOut):
                    if (time_since_last_change > _fadeOutTime)
                    {
                        _currentState = TitleDisplayState.NotDisplaying;
                        _ticksAtTitleDisplayTime = time_now;
                    }
                    progress = (float)((double)time_since_last_change / _fadeOutTime);
                    break;
                case(TitleDisplayState.NotDisplaying):
                    //Do nothing.
                    break;
            }

            _displayColor = _settings.GetColor(_currentState, progress);
            _rotation = _settings.GetRotation(_currentState, progress);
            _size = _settings.GetSize(_currentState, progress);
        }

        public void Draw(Vector2 center)
        {
            Vector2 origin = _font.MeasureString(_displayString);
            origin.X *= 0.5f;
            origin.Y *= 0.5f;
            _spriteBatch.Begin();
            _spriteBatch.DrawString(_font, _displayString, center, _displayColor, _rotation ,origin,_size, SpriteEffects.None, 0);
            _spriteBatch.End();

        }


    }
}
