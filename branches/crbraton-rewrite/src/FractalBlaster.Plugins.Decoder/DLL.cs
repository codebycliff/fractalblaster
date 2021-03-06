﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Decoder
{
    public class DLL : FractalBlaster.Core.Legacy.IDLL, FractalBlaster.Core.Legacy.IDecode
    {
        #region IDLL Members

        public Type GetGenericType()
        {
            return typeof(FractalBlaster.Core.Legacy.Decoder);
        }
  
        #endregion

        #region IDecode Members

        FractalBlaster.Core.Legacy.Decoder FractalBlaster.Core.Legacy.IDecode.Generate(string filepath)
        {
            return new Decoder.AudioFile(filepath);
        }

        #endregion
    }
}
