using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FractalBlaster.Plugins.BasicVisualizer
{
    /// <summary>
    /// Class holds all the settings used to tweak the bloom effect.
    /// </summary>
    public class TrailsSettings
    {
        #region Fields

        // Name of a preset bloom setting, for display to the user.
        public readonly string Name;

        /// <summary>
        /// Controls the amount of the bloom and base images that
        /// will be mixed into the final scene. Range 0 to 1.
        /// </summary>
        public float LastFrameIntensity;

        /// <summary>
        /// Controls the amount of the bloom and base images that
        /// will be mixed into the final scene. Range 0 to 1.
        /// </summary>
        public float SceneIntensity;


        #endregion


        /// <summary>
        /// Constructs a new bloom settings descriptor.
        /// </summary>
        public TrailsSettings(string name, float lastFrameIntensity, float sceneIntensity)
        {
            Name = name;
            LastFrameIntensity = lastFrameIntensity;
            SceneIntensity = sceneIntensity;
        }


        /// <summary>
        /// Table of preset bloom settings, used by the sample program.
        /// </summary>
        public static TrailsSettings[] PresetSettings =
        {
            //                Name           last  scene
            new TrailsSettings("Best",       1.0f, 0.75f),
            new TrailsSettings("Default",    0.3f, 1.0f),
            new TrailsSettings("Soft",       1,     1),
            new TrailsSettings("Desaturated",2,     1),
            new TrailsSettings("Saturated",  2,     1),
            new TrailsSettings("Blurry",     1,     0),
            new TrailsSettings("Subtle",     1,     1),
            new TrailsSettings("Greg's Brew",1.75f, 1f),
        };
    }
}
