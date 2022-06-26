using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TicketBoard.DAL.Data.Entity;

namespace TicketBoard.DAL.Data.EntitiesConfigurations;

public class LocationConfiguration : IEntityTypeConfiguration<LocationEntity>
{
    public void Configure(EntityTypeBuilder<LocationEntity> builder)
    {
        builder.ToTable("Location").HasKey(l => l.LocationId);

        builder.Property(l => l.LocationId).ValueGeneratedOnAdd();

        builder.Property(l => l.Name).HasMaxLength(120).IsRequired();
    }
}