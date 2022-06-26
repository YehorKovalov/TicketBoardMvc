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

    public async Task<TicketEntity?> GetById(int ticketId, bool loadRelatedEntities = false)
    {
        if (ticketId <= 0)
        {
            _logger.LogError($"{nameof(GetById)} ---> Unpredictable behavior: {nameof(ticketId)} = {ticketId}");
            return null;
        }

        var ticket = await _dbContext.Tickets.FirstOrDefaultAsync(t => t.TicketId == ticketId);

        if (ticket == null)
        {
            _logger.LogError($"{nameof(GetById)} ---> ticket's not found");
            return null;
        }
        _logger.LogError($"{nameof(GetById)} ---> TicketId = {ticket.TicketId}, Price = {ticket.Price}");

        if (loadRelatedEntities)
        {
            await _dbContext
                .Entry(ticket)
                .Reference(t => t.Place)
                .Query()
                .Include(t => t.Location)
                .LoadAsync();
        }

        return ticket;
    }

    public async Task<IEnumerable<TicketEntity>> GetAll(bool loadRelatedEntities = false)
    {
        if (!await _dbContext.Tickets.AnyAsync())
        {
            _logger.LogWarning($"{nameof(GetAll)} ---> Ticket table is empty");
            return Enumerable.Empty<TicketEntity>();
        }

        var tickets = _dbContext.Tickets.AsQueryable();

        if (loadRelatedEntities)
        {
            tickets = tickets
                .Include(t => t.Place)
                .ThenInclude(p => p.Location);
        }

        var result = await tickets.ToListAsync();

        return result;
    }

    public async Task<int?> Add(int placeId, string? description, DateTime date, double price)
    {
        _logger.LogInformation($"State: {nameof(placeId)}: {placeId}; {nameof(date)}: {date}; {nameof(description)}: {description}; {nameof(price)}: {price};");
        var ticket = new TicketEntity
        {
            PlaceId = placeId,
            Date = date,
            Price = price,
            Description = description
        };
        
        var result = await _dbContext.AddAsync(ticket);
        await _dbContext.SaveChangesAsync();
        return result.Entity.TicketId;
    }

    public async Task<int?> Update(int ticketId, int placeId, string? description, DateTime date, double price)
    {
        _logger.LogInformation($"State: {nameof(ticketId)}: {ticketId}; {nameof(placeId)}: {placeId}; {nameof(date)}: {date}; {nameof(description)}: {description}; {nameof(price)}: {price};");
        var ticket = new TicketEntity
        {
            TicketId = ticketId,
            PlaceId = placeId,
            Date = date,
            Price = price,
            Description = description
        };
        
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
}