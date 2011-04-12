using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace FractalBlaster.Plugins.BasicVisualizer
{
    public class VisualizerDisplayTextSettings
    {
        public float FadeInTime = 1.0f;
        public float FadeOutTime = 1.0f;
        public float DisplayTime = 1.0f;

        public float StartingSize = 1.0f;
        public float DisplaySize = 1.0f;
        public float EndingSize = 1.0f;

        public Color StartingColor = Color.Transparent;
        public Color EndingColor = Color.Transparent;
        public Color DisplayColor = Color.White;

        public float StartingRotation = 0.0f;
        public float EndingRotation = 0.0f;
        public float DisplayRotation = 0.0f;

        public float GetSize(VisualizerSongTitleDisplay.TitleDisplayState DisplayState, float Progress)
        {
            switch (DisplayState)
            {
                case(VisualizerSongTitleDisplay.TitleDisplayState.FadingIn):
                    return MathHelper.Lerp(StartingSize, DisplaySize, Progress);
                case(VisualizerSongTitleDisplay.TitleDisplayState.Displaying):
                    return DisplaySize;
                case (VisualizerSongTitleDisplay.TitleDisplayState.FadingOut):
                    return MathHelper.Lerp(DisplaySize, EndingSize, Progress);
                case (VisualizerSongTitleDisplay.TitleDisplayState.NotDisplaying):
                    return 1.0f;
            }
            return 1.0f;
        }

        public Color GetColor(VisualizerSongTitleDisplay.TitleDisplayState DisplayState, float Progress)
        {
            switch (DisplayState)
            {
                case (VisualizerSongTitleDisplay.TitleDisplayState.FadingIn):
                    return new Color(Vector4.Lerp(StartingColor.ToVector4(), DisplayColor.ToVector4(), Progress));
                case (VisualizerSongTitleDisplay.TitleDisplayState.Displaying):
                    return DisplayColor;
                case (VisualizerSongTitleDisplay.TitleDisplayState.FadingOut):
                    return new Color(Vector4.Lerp(DisplayColor.ToVector4(), EndingColor.ToVector4(), Progress));
                case (VisualizerSongTitleDisplay.TitleDisplayState.NotDisplaying):
                    return Color.Transparent;
            }
            return Color.Transparent;
        }

        public float GetRotation(VisualizerSongTitleDisplay.TitleDisplayState DisplayState, float Progress)
        {
            switch (DisplayState)
            {
                case (VisualizerSongTitleDisplay.TitleDisplayState.FadingIn):
                    return MathHelper.Lerp(StartingRotation, DisplayRotation, Progress);
                case (VisualizerSongTitleDisplay.TitleDisplayState.Displaying):
                    return DisplayRotation;
                case (VisualizerSongTitleDisplay.TitleDisplayState.FadingOut):
                    return MathHelper.Lerp(DisplayRotation, EndingRotation, Progress);
                case (VisualizerSongTitleDisplay.TitleDisplayState.NotDisplaying):
                    return 0.0f;
            }
            return 0.0f;
        }
    }
}
