using GreenFlux.Model;

namespace GreenFlux.Service.Base
{
    public interface IGroupService
    {
        Group GetGroup(long id);
        Group CreateGroup(Group group);
        Group UpdateGroup(Group group);
        void DeleteGroup(long id);
    }
}
