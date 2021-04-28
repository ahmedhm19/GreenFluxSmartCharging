using GreenFlux.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GreenFlux.DataAccess.EF.Configurations
{
    public class ChargeStationConfiguration : IEntityTypeConfiguration<ChargeStation>
    {
        public void Configure(EntityTypeBuilder<ChargeStation> builder)
        {
            builder.HasKey(e => new { e.Id });

            builder.Property(t => t.Id)
              .ValueGeneratedOnAdd();

            builder.Property(t => t.Name)
                .HasMaxLength(300)
                .IsRequired();
        }
    }
}
