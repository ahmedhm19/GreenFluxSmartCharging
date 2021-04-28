using GreenFlux.Model;
using System;

namespace GreenFlux.DataAccess.Base
{
    public interface IUnitOfWork : IDisposable
    {

 
        int SaveChanges();

        IRepository<ChargeStation> ChargeStationRepository { get; set; }

        IRepository<Connector> ConnectorRepository { get; set; }

        IRepository<Group> GroupRepository { get; set; }




    }
}
