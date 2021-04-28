using System;
using System.Collections.Generic;
using System.Text;

namespace GreenFlux.DataAccess.Exceptions
{
   public class DataException : Exception
    {
        public DataException() : base()
        { }

        public DataException(string message) : base(message)
        { }


        public DataException(string message, Exception inner)
            : base(message, inner) { }


    }
}
