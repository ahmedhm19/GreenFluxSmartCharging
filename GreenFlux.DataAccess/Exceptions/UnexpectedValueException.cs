using System;

namespace GreenFlux.DataAccess.Exceptions
{

    [Serializable]
    public class UnexpectedValueException : DataException
    {

        public UnexpectedValueException() : base()
        {   }

        public UnexpectedValueException(string propertyName)
            : base($"{propertyName} value is not valid !")
        { }


        public UnexpectedValueException(string message, Exception inner)
            : base(message, inner) { }


    }
}
