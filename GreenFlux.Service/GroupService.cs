using GreenFlux.DataAccess.Base;
using GreenFlux.DataAccess.Exceptions;
using GreenFlux.Model;
using GreenFlux.Service.Base;
using GreenFlux.Service.Base.Validators;
using GreenFlux.Service.Tools;
using GreenFlux.Service.Validators;
using System.Collections;
using System.Collections.Generic;

namespace GreenFlux.Service
{
    public class GroupService : IGroupService
    {
        private readonly IUnitOfWork _UnitOfWork;
        private readonly IGroupValidator _GroupValidator;

        public GroupService(IUnitOfWork unitOfWork, IGroupValidator groupValidator)
        {
            _UnitOfWork = unitOfWork;
            _GroupValidator = groupValidator;
        }

        public Group GetGroup(long id)
        {
            return _UnitOfWork.GroupRepository.GetById(id);
        }

        public Group CreateGroup(Group group)
        {
            ThrowIf.Argument.IsNull(group, nameof(group));

            _GroupValidator.ValidateCreate(group);

            _UnitOfWork.GroupRepository.Add(group);

            _UnitOfWork.SaveChanges();

            return group;
        }

        public Group UpdateGroup(Group group)
        {
            ThrowIf.Argument.IsNull(group, nameof(group));

            if (!_UnitOfWork.GroupRepository.Exists(group))
                throw new NotFoundException();

            _GroupValidator.ValidateUpdate(group);

            _UnitOfWork.GroupRepository.Update(group);

            _UnitOfWork.SaveChanges();

            return group;
        }

        public void DeleteGroup(long id)
        {
            Group group = GetGroup(id);
            if (group == null)
                throw new NotFoundException();         

            _UnitOfWork.GroupRepository.Delete(group);
            _UnitOfWork.SaveChanges();
        }

    }
}
