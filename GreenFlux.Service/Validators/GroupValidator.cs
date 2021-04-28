using GreenFlux.DataAccess.Base;
using GreenFlux.Model;
using GreenFlux.Service.Base.Validators;
using GreenFlux.Service.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GreenFlux.Service.Validators
{
    public class GroupValidator : IGroupValidator
    {
        private readonly IUnitOfWork _UnitOfWork;
        private readonly ICommonValidator _CommonValidator;

        public GroupValidator(IUnitOfWork unitOfWork,ICommonValidator commonValidator)
        {
            _UnitOfWork = unitOfWork;
            _CommonValidator = commonValidator;
        }


        public void ValidateCreate(Group group)
        {
            ValidateCapacity(group);
        }
       
        public void ValidateUpdate(Group group)
        {
            ValidateCapacity(group);
           
            //Get sum of MaxCurrentSum of all connectors of current group
            var maxCurrentSum =
                (from c in _UnitOfWork.ConnectorRepository.Queryable
                 join cs in _UnitOfWork.ChargeStationRepository.Queryable.Where(e => e.GroupId == @group.Id)
                 on c.ChargeStationId equals cs.Id
                 select c.MaxCurrent).Sum();

            //Make sure that the user did not changed the Capacity to a value less than the sum of existing connectors
            _CommonValidator.ValidateCapacityExceedance(group.Capacity, maxCurrentSum);
        }

        //Helpers

        private void ValidateCapacity(Group group)
        {
            if (group.Capacity <= 0)
            {
                throw new InvalidCapacityValueException();
            }
        }

    }
}
