using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FractalBlaster.Universe.Legacy;

namespace FractalBlaster.Plugins.AudioOut.Wrapper
{
    public class DLL : IDLL, IOutput
    {

        #region IDLL Members

        public Type GetGenericType()
        {
            return typeof(Output);
        }

        #endregion

        #region IOutput Members

        public Output Generate()
        {
            return new Wrapper();
        }

        #endregion
    }
}
