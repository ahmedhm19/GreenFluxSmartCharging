using GreenFlux.Model;
using System.Collections.Generic;

namespace GreenFlux.Service.Base
{
    public interface IConnectorService
    {
        List<Connector> GetConnectorsByChargeStation(long chargeStationId);
        List<Connector> GetConnectorsByGroup(long groupId);
        Connector GetConnector(byte ck_Id, long chargeStationId);
        Connector CreateConnector(Connector connector);
        Connector UpdateConnector(Connector connector);
        void DeleteConnector(byte ck_Id, long chargeStationId);
    }
}
