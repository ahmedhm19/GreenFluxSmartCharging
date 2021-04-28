using System;

namespace GreenFlux.Service.Exceptions
{

    [Serializable]
    public class MaximumConnectorsException : DomainValidationException
    {

        /// <summary>
        /// Maximum number of connectors allowed by one ChargeStation
        /// </summary>
        public int MaximumConnectors { get; set; }

        public MaximumConnectorsException()  : base()
        { }

        public MaximumConnectorsException(int maximumConnectorsAllowed) 
            : base($"Maximum number of connectors per ChargeStation is ({maximumConnectorsAllowed})")
        {
            MaximumConnectors = maximumConnectorsAllowed;
        }

        public MaximumConnectorsException(string message)
            : base(message) { }

        public MaximumConnectorsException(string message, Exception inner)
            : base(message, inner) { }

   
    }
}
