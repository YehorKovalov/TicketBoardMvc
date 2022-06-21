using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TicketBoard.DAL.Data;
using TicketBoard.DAL.Data.Entity;
using TicketBoard.DAL.Repositories.Abstractions;

namespace TicketBoard.DAL.Repositories;

public class TicketRepository : ITicketRepository
{

    private readonly AppDbContext _dbContext;
    private readonly ILogger<TicketRepository> _logger;

    public TicketRepository(AppDbContext dbContext, ILogger<TicketRepository> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<TicketEntity?> GetById(int ticketId)
    {
        if (ticketId <= 0)
        {
            _logger.LogError($"{nameof(GetById)} ---> Unpredictable behavior: {nameof(ticketId)} = {ticketId}");
            return null;
        }

        var result = await _dbContext.Tickets.FirstOrDefaultAsync(t => t.TicketId == ticketId);

        return result;
    }

    public async Task<IEnumerable<TicketEntity>> GetAll()
    {
        var tickets = await _dbContext.Tickets.ToListAsync();
        if (!tickets.Any())
        {
            _logger.LogWarning($"{nameof(GetAll)} ---> Ticket table is empty");
            return Enumerable.Empty<TicketEntity>();
        }

        return tickets;
    }

    public async Task<int?> Add(string title, string destinationPlace, string? description)
    {
        _logger.LogInformation($"State: {nameof(title)}: {title}; {nameof(destinationPlace)}: {destinationPlace}; {nameof(description)}: {description};");
        var ticket = new TicketEntity
        {
            Title = title,
            DestinationPlace = destinationPlace,
            Description = description
        };

        if (!StateForAddingIsValid(ticket))
        {
            _logger.LogError($"{nameof(Add)} ---> State is not valid");
            return null;
        }
        
        var result = await _dbContext.AddAsync(ticket);
        await _dbContext.SaveChangesAsync();
        return result.Entity.TicketId;
    }

    public async Task<int?> Update(int ticketId, string title, string destinationPlace, string? description)
    {
        _logger.LogInformation($"State: {nameof(ticketId)}: {ticketId}; {nameof(title)}: {title}; {nameof(destinationPlace)}: {destinationPlace}; {nameof(description)}: {description};");
        var ticket = new TicketEntity
        {
            TicketId = ticketId,
            Title = title,
            DestinationPlace = destinationPlace,
            Description = description
        };

        if (!StateForUpdatingIsValid(ticket))
        {
            _logger.LogError($"{nameof(Update)} ---> State is not valid");
            return -1;
        }
        
        var result = _dbContext.Update(ticket);
        await _dbContext.SaveChangesAsync();
        return result.Entity.TicketId;
    }

    public async Task<int?> DeleteById(int ticketId)
    {
        if (ticketId <= 0)
        {
            _logger.LogError($"{nameof(DeleteById)} ---> Unpredictable behavior: {nameof(ticketId)} = {ticketId}");
            return null;
        }

        var ticket = new TicketEntity { TicketId = ticketId};
        _dbContext.Entry(ticket).State = EntityState.Deleted;
        
        await _dbContext.SaveChangesAsync();

        return ticketId;
    }

    private bool StateForAddingIsValid(TicketEntity ticket)
    {
        var titleIsValid = !string.IsNullOrWhiteSpace(ticket.Title);
        var destinationPlaceIsValid = !string.IsNullOrWhiteSpace(ticket.DestinationPlace);

        return titleIsValid && destinationPlaceIsValid;
    }
    
    private bool StateForUpdatingIsValid(TicketEntity ticket)
    {
        var titleIsValid = !string.IsNullOrWhiteSpace(ticket.Title);
        var destinationPlaceIsValid = !string.IsNullOrWhiteSpace(ticket.DestinationPlace);
        var idIsValid = ticket.TicketId > 0;
        return titleIsValid && destinationPlaceIsValid && idIsValid;
    }
}