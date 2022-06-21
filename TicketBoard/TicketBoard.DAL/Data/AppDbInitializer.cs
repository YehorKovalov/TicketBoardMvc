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

        var tickets = GetTickets();
        if (!await dbContext.Tickets.AnyAsync())
        {
            await dbContext.AddRangeAsync(tickets);
            await dbContext.SaveChangesAsync();
        }
    }

    private IEnumerable<TicketEntity> GetTickets() => new List<TicketEntity>
    {
        new TicketEntity
        {
            Description = "It's long long description for this ticket.",
            DestinationPlace = "Kharkiv Museum",
            Title = "Museum 1"
        },
        new TicketEntity
        {
            Description = "It's long long description for this ticket.",
            DestinationPlace = "Luhansk Museum",
            Title = "Museum 2"
        },
        new TicketEntity
        {
            Description = "It's long long description for this ticket.",
            DestinationPlace = "Kiev Museum",
            Title = "Museum 3"
        },
        new TicketEntity
        {
            Description = "It's long long description for this ticket.",
            DestinationPlace = "Kherson Museum",
            Title = "Museum 4"
        },
        new TicketEntity
        {
            Description = "It's long long description for this ticket.",
            DestinationPlace = "Lviv Museum",
            Title = "Museum 5"
        },
        new TicketEntity
        {
            Description = "It's long long description for this ticket.",
            DestinationPlace = "Donetsk Museum",
            Title = "Museum 6"
        },
        new TicketEntity
        {
            Description = "It's long long description for this ticket.",
            DestinationPlace = "Ivano-Frankovsk Museum",
            Title = "Museum 7"
        }
    };
}