using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TicketBoard.DAL.Data.Entity;

namespace TicketBoard.DAL.Data.EntitiesConfigurations;

public class TicketConfiguration : IEntityTypeConfiguration<TicketEntity>
{
    public void Configure(EntityTypeBuilder<TicketEntity> builder)
    {
        builder.ToTable("Ticket").HasKey(t => t.TicketId);

        builder.Property(t => t.TicketId).ValueGeneratedOnAdd();

        builder.Property(t => t.Description).HasMaxLength(2000).IsRequired();

        builder.Property(t => t.Price).IsRequired();

        builder.Property(t => t.Date).HasColumnType("date").IsRequired();

        builder.HasOne(t => t.Place)
            .WithMany(p => p.Tickets)
            .HasForeignKey(t => t.PlaceId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}