using GreenFlux.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GreenFlux.DataAccess.EF.Configurations
{
    public class GroupConfiguration : IEntityTypeConfiguration<Group>
    {
        public void Configure(EntityTypeBuilder<Group> builder)
        {
            builder.HasKey(e => new { e.Id });

            builder.Property(t => t.Id)
               .ValueGeneratedOnAdd();

            builder.Property(t => t.Capacity)
                .IsRequired();

            builder.Property(t => t.Name)
                .HasMaxLength(300)
                .IsRequired();
        }
    }
}
