using System;
using System.Collections.Generic;
using System.Text;

namespace GreenFlux.Service.Tools
{
    internal static class ThrowIf
    {
        public static class Argument
        {
            public static void IsNull<T>(T argument, string name)
            {
                if (argument is null)
                {
                    throw new ArgumentNullException(name);
                }
            }
        }
    }
}
