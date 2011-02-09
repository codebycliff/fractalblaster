using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Decoder
{
    public class DLL : Common.IDLL, Common.IDecode
    {
        #region IDLL Members

        public Type GetGenericType()
        {
            return typeof(Common.Decoder);
        }
  
        #endregion

        #region IDecode Members

        Common.Decoder Common.IDecode.Generate(string filepath)
        {
            return new AudioFile(filepath);
        }

        #endregion
    }
}
