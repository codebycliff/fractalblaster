using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chopper
{
    public class DLL : Common.IDLL, Common.IPCMReceiver
    {

        #region IDLL Members

        Type Common.IDLL.GetGenericType()
        {
            return typeof(Common.PCMReceiver);
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
