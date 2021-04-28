using System;

namespace GreenFlux.Service.Exceptions
{

    [Serializable]
    public class MinimumConnectorsException : DomainValidationException
    {

        /// <summary>
        /// Minimum number of connectors required by ChargeStation
        /// </summary>
        public int MinimumConnectors { get; set; }
      
        public MinimumConnectorsException(int minimumConnectorsAllowed)
               : base($"Minimum number of connectors allowed in one ChargeStation is ({minimumConnectorsAllowed})")
        {
            MinimumConnectors = minimumConnectorsAllowed;  
        }

        public MinimumConnectorsException() : base()
        { }

        public MinimumConnectorsException(string message)
            : base(message) { }

        public MinimumConnectorsException(string message, Exception inner)
            : base(message, inner) { }



    }
}
