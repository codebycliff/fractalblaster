using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FractalBlaster.Core {
    
    interface IPreferenceReader {

        
        String Path { get; set; }

        Dictionary<String, Object> Read();

    }

}
