using GreenFlux.Model;
using System.Collections.Generic;

namespace GreenFlux.Service.Base.Validators
{
    public interface IChargeStationValidator
    {
        void ValidateCreate(ChargeStation ChargeStation, ICollection<Connector> connectors);       
    }
}
