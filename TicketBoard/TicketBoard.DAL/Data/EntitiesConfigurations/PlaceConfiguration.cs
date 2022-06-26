using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TicketBoard.DAL.Data.Entity;

namespace TicketBoard.DAL.Data.EntitiesConfigurations;

public class PlaceConfiguration : IEntityTypeConfiguration<PlaceEntity>
{
    public void Configure(EntityTypeBuilder<PlaceEntity> builder)
    {
        builder.ToTable("Place").HasKey(p => p.PlaceId);

        builder.Property(p => p.PlaceId).ValueGeneratedOnAdd();

        builder.Property(p => p.Name).HasMaxLength(120).IsRequired();

        builder.HasOne(p => p.Location)
            .WithMany(l => l.Places)
            .HasForeignKey(p => p.LocationId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}