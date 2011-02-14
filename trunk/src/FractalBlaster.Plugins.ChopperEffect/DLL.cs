using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FractalBlaster.Universe.Legacy;

namespace FractalBlaster.Plugins.ChopperEffect
{
    public class DLL : IDLL, IPCMReceiver
    {

        #region IDLL Members

        Type IDLL.GetGenericType()
        {
            return typeof(PCMReceiver);
        }

        #endregion

        #region IPCMReceiver Members

        public Common.PCMReceiver Generate()
        {
            return new Receiver();
        }

        #endregion
    }
}
