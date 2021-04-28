using GreenFlux.Model;

namespace GreenFlux.Service.Base.Validators
{
    public interface IConnectorValidator
    {
        void ValidateCreate(Connector connector);
        void ValidateUpdate(Connector connector);
        void ValidateDelete(Connector connector);
    }
}
