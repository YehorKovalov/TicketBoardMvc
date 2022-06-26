using Microsoft.EntityFrameworkCore;
using TicketBoard.DAL.Data.EntitiesConfigurations;
using TicketBoard.DAL.Data.Entity;

namespace TicketBoard.DAL.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<TicketEntity> Tickets { get; set; } = null!;

    public DbSet<PlaceEntity> Places { get; set; } = null!;

    public DbSet<LocationEntity> Locations { get; set; } = null!;
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new TicketConfiguration());
        modelBuilder.ApplyConfiguration(new PlaceConfiguration());
        modelBuilder.ApplyConfiguration(new LocationConfiguration());
    }
}