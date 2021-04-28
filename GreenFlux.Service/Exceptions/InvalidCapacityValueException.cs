using System;
using System.Collections.Generic;
using System.Text;

namespace GreenFlux.Service.Exceptions
{

    [Serializable]
    public class InvalidCapacityValueException : DomainValidationException
    {

        public InvalidCapacityValueException() 
          : base("Capaciy value is not valid")
        { }

        public InvalidCapacityValueException(string message)
            : base(message) { }

        public InvalidCapacityValueException(string message, Exception inner)
            : base(message, inner) { }


    }
}
