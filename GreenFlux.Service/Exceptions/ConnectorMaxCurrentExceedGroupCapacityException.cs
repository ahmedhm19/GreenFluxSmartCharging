using System;

namespace GreenFlux.Service.Exceptions
{

    [Serializable]
    public class ConnectorMaxCurrentExceedGroupCapacityException : DomainValidationException
    {

        public float GroupCapacity { get; set; }
        public float MaxCurrentSum { get; set; }

        public ConnectorMaxCurrentExceedGroupCapacityException(float groupCapacity, float maxCurrentSum ) 
            : base($"Connector MaxCurrent ({maxCurrentSum}) can not be greater that Group capacity ({groupCapacity}) ")
        {
            GroupCapacity = groupCapacity;
            MaxCurrentSum = maxCurrentSum;
        }

        public ConnectorMaxCurrentExceedGroupCapacityException() : base()
        { }

        public ConnectorMaxCurrentExceedGroupCapacityException(string message)
            : base(message) { }

        public ConnectorMaxCurrentExceedGroupCapacityException(string message, Exception inner)
            : base(message, inner) { }


    }
}
