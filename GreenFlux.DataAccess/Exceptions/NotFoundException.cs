using System;

namespace GreenFlux.DataAccess.Exceptions
{
    [Serializable]
   public class NotFoundException : DataException
    {

        public NotFoundException() 
            : base ("Resource not found !")
        {

        }

        public NotFoundException(string message)
           : base(message) { }

        public NotFoundException(string message, Exception inner)
            : base(message, inner) { }

    }
}
