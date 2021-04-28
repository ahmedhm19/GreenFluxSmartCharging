using GreenFlux.DataAccess.Base;
using GreenFlux.Model;
using Microsoft.EntityFrameworkCore;
using System;
using Unity;

namespace GreenFlux.DataAccess.EF
{
    public class UnitOfWork : IUnitOfWork
    {
        public DatabaseContext DatabaseContext { get; }

        public UnitOfWork(DatabaseContext databaseContext)
        {
            DatabaseContext = databaseContext;
        }

        [Dependency]
        public IRepository<ChargeStation> ChargeStationRepository { get; set; }

        [Dependency]
        public IRepository<Connector> ConnectorRepository { get; set; }

        [Dependency]
        public IRepository<Group> GroupRepository { get; set; }
  
        public int SaveChanges()
        {
            try
            {
                return DatabaseContext.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                string message = "Database failure";

                #region SqlException
                
                //if (ex.GetBaseException() is SqlException sqlException)
                //{
                //    var number = sqlException.Number;

                //    switch (number)
                //    {
                //        case 547:
                //            {
                //                message = "Can not delete this record, because it is still used by another one !";
                //                break;
                //            }
                //        case 544:
                //            {
                //                message = "Specifying custom keys is not allowed !";
                //                break;
                //            }
                //    }

                //}
              
                #endregion


                throw new Exception( message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Database failure", ex);
            }
        }

        public void Dispose()
        {
            DatabaseContext.Dispose();
        }
   
    }
}
