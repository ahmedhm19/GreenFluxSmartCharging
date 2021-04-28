using GreenFlux.Service.Base.Validators;
using GreenFlux.Service.Exceptions;

namespace GreenFlux.Service.Validators
{

    public class CommonValidator : ICommonValidator
    {

        /// <summary>
        /// Throw exception if given count exceed the number of connectors allowed in one ChargeStation
        /// </summary>
        public void ValidateChargeStationConnectorsCount(int connectorsCount)
        {
            if (connectorsCount == 0)
                throw new MinimumConnectorsException(1);

            if (connectorsCount > 5)
                throw new MaximumConnectorsException(5);
        }

        /// <summary>
        /// Throw exception if allowed capacity is exceeded
        /// </summary>
        /// <param name="MaxCurrentToAdd">Check if adding given value to group's total MaxCurrent exceed capacity, then throw exception </param>
        public void ValidateCapacityExceedance(float groupCapacity, float maxCurrentSum)
        {
            if (maxCurrentSum > groupCapacity)
                throw new ConnectorsMaxCurrentExceedGroupCapacityException(groupCapacity, maxCurrentSum);

        }
        public void ValidateCapacityExceedance(float groupCapacity, float maxCurrentSum, float maxCurrentToAdd)
        {

            if (maxCurrentToAdd > groupCapacity)
                throw new ConnectorMaxCurrentExceedGroupCapacityException(groupCapacity, maxCurrentToAdd);

            float connectorsSum = maxCurrentSum + maxCurrentToAdd;

            if (connectorsSum > groupCapacity)
                throw new ConnectorsMaxCurrentExceedGroupCapacityException(groupCapacity, connectorsSum);
        }

    }
}
