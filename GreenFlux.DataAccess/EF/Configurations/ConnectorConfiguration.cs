using GreenFlux.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GreenFlux.DataAccess.EF.Configurations
{
    public class ConnectorConfiguration : IEntityTypeConfiguration<Connector>
    {
        public void Configure(EntityTypeBuilder<Connector> builder)
        {
            builder.HasKey(e => new { e.CK_Id, e.ChargeStationId });

            builder.Property(e => e.CK_Id)
                .ValueGeneratedNever()
                .IsRequired();

            builder.Property(e => e.MaxCurrent)
                .IsRequired();
        }
    }
}
