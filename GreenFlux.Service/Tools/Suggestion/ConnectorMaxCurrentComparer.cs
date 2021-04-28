using GreenFlux.Model;
using System.Collections.Generic;

namespace GreenFlux.Service.Tools
{
    public class ConnectorMaxCurrentComparer : IComparer<Connector>
    {

        public int Compare(Connector x, Connector y)
        {
            if (x.MaxCurrent < y.MaxCurrent)
            return 1;
            if (x.MaxCurrent > y.MaxCurrent)
            {
                return -1;
            }

            return 0;
        }
    }

}
