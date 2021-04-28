using GreenFlux.DataAccess.Base;
using GreenFlux.DataAccess.Exceptions;
using GreenFlux.Model;
using GreenFlux.Service.Base;
using GreenFlux.Service.Base.Validators;
using GreenFlux.Service.Exceptions;
using GreenFlux.Service.Tools;
using System.Collections.Generic;
using System.Linq;

namespace GreenFlux.Service
{
    public class ConnectorService : IConnectorService
    {
        private readonly IUnitOfWork _UnitOfWork;
        private readonly IConnectorKeyGenerator _ConnectorKeyGenerator;
        private readonly IConnectorValidator _ConnectorValidator;


        public ConnectorService(IUnitOfWork unitOfWork , IConnectorKeyGenerator connectorKeyGenerator, IConnectorValidator connectorValidator)
        {
            _UnitOfWork = unitOfWork;
            _ConnectorKeyGenerator = connectorKeyGenerator;
            _ConnectorValidator = connectorValidator;
        }

        public List<Connector> GetConnectorsByChargeStation(long chargeStationId)
        {
            return _UnitOfWork.ConnectorRepository.Queryable.Where(e => e.ChargeStationId == chargeStationId).ToList();
        }

        public List<Connector> GetConnectorsByGroup(long groupId)
        {
            return (from c in _UnitOfWork.ConnectorRepository.Queryable
                    join cs in _UnitOfWork.ChargeStationRepository.Queryable.
                    Where(e => e.GroupId == @groupId)
                    on c.ChargeStationId equals cs.Id
                    select c).ToList();
        }

        public Connector GetConnector(byte ck_Id , long chargeStationId)
        {
            return _UnitOfWork.ConnectorRepository.GetById(ck_Id , chargeStationId);
        }

        public Connector CreateConnector(Connector connector)
        {
            ThrowIf.Argument.IsNull(connector, nameof(connector));

            _ConnectorValidator.ValidateCreate(connector);

            connector.CK_Id = _ConnectorKeyGenerator.GenerateKey(GetConnectorsByChargeStation(connector.ChargeStationId));

            _UnitOfWork.ConnectorRepository.Add(connector);
            _UnitOfWork.SaveChanges();

            return connector;
        }

        public Connector UpdateConnector(Connector connector)
        {
            ThrowIf.Argument.IsNull(connector, nameof(connector));

            if (!_UnitOfWork.ConnectorRepository.Exists(connector))
                throw new NotFoundException();

            _ConnectorValidator.ValidateUpdate(connector);

            _UnitOfWork.ConnectorRepository.Update(connector);
            _UnitOfWork.SaveChanges();

            return connector;
        }

        public void DeleteConnector(byte ck_Id , long chargeStationId)
        {
            var connector = GetConnector(ck_Id, chargeStationId);
            if (connector == null)
                throw new NotFoundException();

            _ConnectorValidator.ValidateDelete(connector);

            _UnitOfWork.ConnectorRepository.Delete(connector);
            _UnitOfWork.SaveChanges();
        }



    }
}
