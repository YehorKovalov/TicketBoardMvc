using Microsoft.EntityFrameworkCore;
using TicketBoard.DAL.Data.Entity;

namespace TicketBoard.DAL.Data;

public class AppDbInitializer : IDbInitializer<AppDbContext>
{
    public async Task Initialize(AppDbContext dbContext)
    {
        var migrations =  dbContext.Database.GetMigrations();
        if (!migrations.Any())
        {
            await dbContext.Database.EnsureCreatedAsync();
            await dbContext.Database.MigrateAsync();
        }

        if (!await dbContext.Locations.AnyAsync())
        {
            var locations = GetLocations();
            await dbContext.AddRangeAsync(locations);
            await dbContext.SaveChangesAsync();
        }
        
        if (!await dbContext.Places.AnyAsync())
        {
            var places = GetPlaces();
            await dbContext.AddRangeAsync(places);
            await dbContext.SaveChangesAsync();
        }

        if (!await dbContext.Tickets.AnyAsync())
        {
            var tickets = GetTickets();
            await dbContext.AddRangeAsync(tickets);
            await dbContext.SaveChangesAsync();
        }
    }

    private IEnumerable<PlaceEntity> GetPlaces(int amountForEachLocation = 2)
    {
        var places = new List<PlaceEntity>();
        var locationsAmount = GetLocations().Count() - 1;

        var resultAmount = amountForEachLocation * locationsAmount;
        for (var i = 1; i <= resultAmount; i++)
        {
            places.Add(new PlaceEntity
            {
                Name = $"Place Name {i}",
                LocationId = i % locationsAmount + 1
            });
        }

        return places;
    }

    private IEnumerable<LocationEntity> GetLocations()
    {
        return new List<LocationEntity>
        {
            new LocationEntity { Name = "Kharkiv" },
            new LocationEntity { Name = "Kiev" },
            new LocationEntity { Name = "Lviv" }
        };
    }

    private IEnumerable<TicketEntity> GetTickets(int amountForEachPlace = 2)
    {
        var random = new Random();
        var tickets = new List<TicketEntity>();
        var placesAmount = GetPlaces().Count() - 1;

        var resultAmount = amountForEachPlace * placesAmount;

        for (var i = 1; i <= resultAmount; i++)
        {
            tickets.Add(new TicketEntity
            {
                Price = random.Next(1000),
                PlaceId = i % placesAmount + 1,
                Description = "Long long description. Long long description. Long long description. Long long description. Long long description.",
                Date = DateTime.UtcNow.AddDays(random.Next(100))
            });
        }

        return tickets;
    }
}