using System;

namespace GreenFlux.Service.Exceptions
{

    [Serializable]
    public class ConnectorsMaxCurrentExceedGroupCapacityException : DomainValidationException
    {

        public float GroupCapacity { get; set; }
        public float MaxCurrentSum { get; set; }

        public ConnectorsMaxCurrentExceedGroupCapacityException(float groupCapacity, float maxCurrentSum ) 
            : base($"Total of connectors MaxCurrent ({maxCurrentSum}) exceed Group capacity ({groupCapacity}) ")
        {
            GroupCapacity = groupCapacity;
            MaxCurrentSum = maxCurrentSum;
        }

        public ConnectorsMaxCurrentExceedGroupCapacityException() : base()
        { }

        public ConnectorsMaxCurrentExceedGroupCapacityException(string message)
            : base(message) { }

        public ConnectorsMaxCurrentExceedGroupCapacityException(string message, Exception inner)
            : base(message, inner) { }


    }
}
