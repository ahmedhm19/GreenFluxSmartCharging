using GreenFlux.DataAccess.Base;
using GreenFlux.DataAccess.Exceptions;
using GreenFlux.Model;
using GreenFlux.Service.Base.Validators;
using System.Collections.Generic;
using System.Linq;

namespace GreenFlux.Service.Validators
{
    public class ChargeStationValidator : IChargeStationValidator
    {  
        private readonly IUnitOfWork _UnitOfWork;
        private readonly ICommonValidator _CommonValidator;

        public ChargeStationValidator(IUnitOfWork unitOfWork,ICommonValidator commonValidator)
        {
            _UnitOfWork = unitOfWork;
            _CommonValidator = commonValidator;
        }

        public void ValidateCreate(ChargeStation chargeStation, ICollection<Connector> connectors)
        {
            if (chargeStation.GroupId <= 0)
                throw new UnexpectedValueException(nameof(chargeStation.GroupId));

            _CommonValidator.ValidateChargeStationConnectorsCount(connectors.Count);

            //Get group
            var group = _UnitOfWork.GroupRepository.GetById(chargeStation.GroupId);
            if (group == null)
                throw new DependentNotFoundException(nameof(ChargeStation.GroupId), chargeStation.GroupId);

            //Get sum of MaxCurrent of given connectors 
            var connectorsMaxCurrentSum = connectors.Sum(c => c.MaxCurrent);

            //Get sum of MaxCurrentSum of all connectors of current group
            var groupConnectorsMaxCurrentSum =
                (from c in _UnitOfWork.ConnectorRepository.Queryable
                 join cs in _UnitOfWork.ChargeStationRepository.Queryable.Where(e => e.GroupId == chargeStation.GroupId)
                 on c.ChargeStationId equals cs.Id
                 select c.MaxCurrent).Sum();

            _CommonValidator.ValidateCapacityExceedance(group.Capacity, groupConnectorsMaxCurrentSum + connectorsMaxCurrentSum);
        }
        
    }
}
