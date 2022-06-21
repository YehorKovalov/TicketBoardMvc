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

        builder.Property(t => t.Title).HasMaxLength(100).IsRequired();

        builder.Property(t => t.DestinationPlace).HasMaxLength(200).IsRequired();

        builder.Property(t => t.Description).HasMaxLength(2000).IsRequired();
    }
}