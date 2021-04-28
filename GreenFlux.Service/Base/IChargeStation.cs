using GreenFlux.Model;
using System.Collections.Generic;

namespace GreenFlux.Service.Base
{
    public interface IChargeStationService
    {
        ChargeStation GetChargeStation(long id);
        ChargeStation CreateChargeStation(ChargeStation chargeStation, List<Connector> connectors);
        ChargeStation UpdateChargeStation(ChargeStation chargeStation);
        void DeleteChargeStation(long id);
    }
}
