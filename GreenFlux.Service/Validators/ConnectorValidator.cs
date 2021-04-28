using GreenFlux.DataAccess.Base;
using GreenFlux.DataAccess.Exceptions;
using GreenFlux.Model;
using GreenFlux.Service.Base.Validators;
using GreenFlux.Service.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GreenFlux.Service.Validators
{
    public class ConnectorValidator : IConnectorValidator
    {
       
        private readonly IUnitOfWork _UnitOfWork;
        private readonly ICommonValidator _CommonValidator;

        public ConnectorValidator(IUnitOfWork unitOfWork, ICommonValidator commonValidator)
        {
            _UnitOfWork = unitOfWork;
            _CommonValidator = commonValidator;
        }

        public void ValidateCreate(Connector connector)
        {
            ValidateMaxCurrent(connector);

            //Check ChargeStation exists
            var chargeStation = _UnitOfWork.ChargeStationRepository.GetById(connector.ChargeStationId);
            if (chargeStation == null)
                throw new DependentNotFoundException(nameof(connector.ChargeStationId), connector.ChargeStationId);

            //Check maximum number of allowed connectors per ChargeStation
            var chargeStationConnectorsCount = _UnitOfWork.ConnectorRepository.Queryable
                .Where(c => c.ChargeStationId == chargeStation.Id).Count();
            _CommonValidator.ValidateChargeStationConnectorsCount(chargeStationConnectorsCount+1 );

            //Prevent from exeeding Group Capacity
            var group = _UnitOfWork.GroupRepository.GetById(chargeStation.GroupId);
            ValidateGroupCapacityExceedance(connector, group , false);
        }

        public void ValidateUpdate(Connector connector)
        {
            ValidateMaxCurrent(connector);

            //Get group of given connector
            Group group = (from g in _UnitOfWork.GroupRepository.Queryable
                           join cs in _UnitOfWork.ChargeStationRepository.Queryable on g.Id equals cs.GroupId
                           join c in _UnitOfWork.ConnectorRepository.Queryable on cs.Id equals c.ChargeStationId
                           where c.ChargeStationId == connector.ChargeStationId
                           select g).First();

            ValidateGroupCapacityExceedance(connector, group, true);
        }

        public void ValidateDelete(Connector connector)
        {
            var stationConnectorsCount = _UnitOfWork.ConnectorRepository.Queryable
                .Where(c => c.ChargeStationId == connector.ChargeStationId).Count();

            _CommonValidator.ValidateChargeStationConnectorsCount(stationConnectorsCount - 1);
        }


        //Helpers

        private void ValidateMaxCurrent(Connector connector)
        {
            if (connector.MaxCurrent <= 0)
            {
                throw new InvalidMaxCurrentValueException();
            }
        }
        private void ValidateGroupCapacityExceedance(Connector connector, Group group, bool excludeGivenConnector)
        {
            //Get connectors of current group
            float connectorsSum = 0;

            if (!excludeGivenConnector)
            {
                connectorsSum =
                  (from c in _UnitOfWork.ConnectorRepository.Queryable
                   join cs in _UnitOfWork.ChargeStationRepository.Queryable.Where(e => e.GroupId == @group.Id)
                   on c.ChargeStationId equals cs.Id
                   select c).Sum(c => c.MaxCurrent);
            }
            else
            {
                connectorsSum =
                  (from c in _UnitOfWork.ConnectorRepository.Queryable 
                   where c.ChargeStationId == @connector.ChargeStationId & c.CK_Id != @connector.CK_Id
                   join cs in _UnitOfWork.ChargeStationRepository.Queryable.Where(e => e.GroupId == @group.Id)
                   on c.ChargeStationId equals cs.Id
                   select c).Sum(c => c.MaxCurrent);
            }

            //Validate
            _CommonValidator.ValidateCapacityExceedance(group.Capacity, connectorsSum, connector.MaxCurrent);

        }

    }
}
