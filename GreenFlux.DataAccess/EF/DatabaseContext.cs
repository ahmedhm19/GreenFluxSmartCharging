using GreenFlux.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace GreenFlux.DataAccess.EF
{
    public class DatabaseContext : DbContext 
    {

        public DatabaseContext(DbContextOptions options) : base(options)
        {

        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            modelBuilder.Entity<Group>()
              .HasMany<ChargeStation>()
              .WithOne()
              .HasForeignKey(e => e.GroupId).OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<ChargeStation>()
               .HasMany<Connector>(e=>e.Connectors) 
               .WithOne()
               .HasForeignKey(e => e.ChargeStationId).OnDelete(DeleteBehavior.Cascade);


        }

        public DbSet<ChargeStation> ChargeStations { get; set; }
        public DbSet<Connector> Connectors { get; set; }
        public DbSet<Group> Groups { get; set; }

        public override int SaveChanges()
        {
            return base.SaveChanges();
        }


        public static void SeedExamples(DatabaseContext databaseContext)
        {

            if (databaseContext.Connectors.Count() == 0)
            {

                var group = new Group()
                {
                    Name = "Group A",
                    Capacity = 70
                };
                databaseContext.Groups.Add(group);
                
                databaseContext.SaveChanges();

                var chargeStation1 = new ChargeStation
                {
                    Name = "Charge Station 01",
                    GroupId = group.Id,
                    Connectors = new List<Connector>
                    {
                        new Connector{MaxCurrent = 10, CK_Id = 1 },
                        new Connector{MaxCurrent = 5, CK_Id = 2 },
                        new Connector{MaxCurrent = 5, CK_Id = 3},
                        new Connector{MaxCurrent = 4, CK_Id = 4 },
                        new Connector{MaxCurrent = 15, CK_Id = 5 },
                    }
                };
                databaseContext.ChargeStations.Add(chargeStation1);

                var chargeStation2 = new ChargeStation
                {
                    Name = "Charge Station 02",
                    GroupId = group.Id,
                    Connectors = new List<Connector>
                    {
                        new Connector{MaxCurrent = 12, CK_Id = 1 },
                        new Connector{MaxCurrent = 9, CK_Id = 2 },
                        new Connector{MaxCurrent = 3, CK_Id = 3},
                    }
                };
                databaseContext.ChargeStations.Add(chargeStation2);







                databaseContext.SaveChanges();
            }
        }


    }
}
