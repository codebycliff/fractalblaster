using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FractalBlaster.Core {

    #region [ Enums ]


    #endregion

    #region [ Delegates ]



    #endregion

    #region [ Structs ]


    public struct AudioMetadata {
        public String Artist;
        public String Title;
        public String Album;
        public String Codec;
        public Int32 TrackNumber;
        public Int32 Year;
        public Int32 Channels;
        public Int32 SampleRate;
        public Int32 BitRate;
        public TimeSpan Duration;
    }

    public struct MediaProperty {

        public String Name { get; private set; }

        public Type Type { get; private set; }

        public Object Value { get; private set; }

        public static MediaProperty CreateProperty(String name, Object value, Type type) {
            return new MediaProperty() {
                Name = name,
                Type = type,
                Value = value
            };
        }

    }

    #endregion

}
