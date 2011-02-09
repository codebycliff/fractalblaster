using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Output
{
    public class DLL : Common.IDLL, Common.IOutput
    {

        #region IDLL Members

        public Type GetGenericType()
        {
            return typeof(Common.Output);
        }

        #endregion

        #region IOutput Members

        public Common.Output Generate()
        {
            return new Wrapper();
        }

        #endregion
    }
}
