using System;

namespace GreenFlux.Service.Exceptions
{

    [Serializable]
    public class ConnectorKeyGenerationException : DomainValidationException
    {

        public ConnectorKeyGenerationException() 
         : base("Connector key generation failed")
        { }

        public ConnectorKeyGenerationException(string message)
            : base(message) { }

        public ConnectorKeyGenerationException(string message, Exception inner)
            : base(message, inner) { }


    }
}
