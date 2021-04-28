using GreenFlux.DataAccess.Base;
using GreenFlux.DataAccess.Exceptions;
using GreenFlux.Model.Base;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace GreenFlux.DataAccess.EF
{
    public class Repository<T> : IRepository<T> where T : Entity
    {


        public DatabaseContext DatabaseContext { get; }

        public Repository(DatabaseContext databaseContext)
        {

            DatabaseContext = databaseContext;

        }


        private DbSet<T> _entities;
        public virtual DbSet<T> Entities
        {
            get
            {
                if (_entities == null)
                {
                    _entities = DatabaseContext.Set<T>();
                }

                return _entities;
            }
        }

        public virtual IQueryable<T> Queryable
        {
            get
            { 
                return Entities;
            }
        }

        public virtual List<T> Collection
        {
            get
            {
                return Entities.ToList();
            }
        }

        public virtual T GetById(params object[] keys)
        {
            return this.Entities.Find(keys);
        }

        public virtual void Add(T entity)
        {
            Entities.Add(entity);
        }

        public virtual void AddRange(ICollection<T> collection)
        {
            Entities.AddRange(collection);
        }

        public virtual void Update(T entity)
        {
            DatabaseContext.Entry(entity).State = EntityState.Modified;
        }

        /// <summary>
        /// Update Entity and ignore given properties
        /// </summary>
        public virtual void Update(T entity, params string[] PropertiesToIgnoreWhenUpdate)
        {

             var oldEntity = DatabaseContext.Entry(entity).GetDatabaseValues().ToObject() as T;
             
            //Ignore these properties when update entity *****
            foreach (var propertyName in PropertiesToIgnoreWhenUpdate)
            {
                //Get old value
                object oldPropValue = oldEntity.GetType().GetProperty(propertyName).GetValue(oldEntity);

                //Restore old value
                DatabaseContext.Entry(entity).Property(propertyName).CurrentValue = oldPropValue;
          
            }

            DatabaseContext.Entry(entity).State = EntityState.Modified;

        }

        public virtual void Delete(T entity)
        {
            Entities.Remove(entity);
        }

        public virtual bool Exists(T entity)
        {
           var entObj = DatabaseContext.Find<T>(entity.GetKey());
            if(entObj != null)
            {
                DatabaseContext.Entry(entObj).State = EntityState.Detached;
                return true;
            }

            return false;
        }




    }
}
