using GreenFlux.Model;

namespace GreenFlux.Service.Base.Validators
{
    public interface IGroupValidator
    {
        void ValidateCreate(Group group);
        void ValidateUpdate(Group group);
    }
}
