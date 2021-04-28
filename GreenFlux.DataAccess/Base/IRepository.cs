using GreenFlux.Model.Base;
using System.Collections.Generic;
using System.Linq;

namespace GreenFlux.DataAccess.Base
{
    public interface IRepository<T> where T : Entity
    {
        IQueryable<T> Queryable { get; }
        List<T> Collection { get; }
        T GetById(params object[] keys);
        void Add(T entity);
        void AddRange(ICollection<T> collection);
        void Update(T entity);
        void Update(T entity, params string[] PropertiesToIgnoreWhenUpdate);
        void Delete(T entity);
        bool Exists(T entity);
    }
}
