using System;
using System.Collections.Generic;
using System.Text;

namespace GreenFlux.Service.Exceptions
{
   public class DomainValidationException : Exception
    {
        public DomainValidationException() : base()
        { }

        public DomainValidationException(string message) : base(message)
        { }

        public DomainValidationException(string message, Exception inner)
            : base(message, inner) { }

    }
}
