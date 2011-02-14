using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FractalBlaster.Core {
    
    class IniPreferenceReader : IPreferenceReader {

        public IniPreferenceReader(String path) {
            Path = path;
        }

        public string Path { get; private set; }

        public Dictionary<string, object> Read() {
            return new Dictionary<string, object>();
        }
    }

}
