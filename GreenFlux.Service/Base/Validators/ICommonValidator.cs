namespace GreenFlux.Service.Base.Validators
{
    public interface ICommonValidator
    {
        void ValidateCapacityExceedance(float groupCapacity, float maxCurrentSum);
        void ValidateCapacityExceedance(float groupCapacity, float maxCurrentSum, float maxCurrentToAdd);
        void ValidateChargeStationConnectorsCount(int connectorsCount);
    }
}