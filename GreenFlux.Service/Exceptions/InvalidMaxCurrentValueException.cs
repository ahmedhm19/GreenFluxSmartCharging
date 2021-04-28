using System;

namespace GreenFlux.Service.Exceptions
{

    [Serializable]
    public class InvalidMaxCurrentValueException : DomainValidationException
    {

        public InvalidMaxCurrentValueException() 
            : base("MaxCurrent value is not valid")
        { }

        public InvalidMaxCurrentValueException(string message)
            : base(message) { }

        public InvalidMaxCurrentValueException(string message, Exception inner)
            : base(message, inner) { }


    }
}
