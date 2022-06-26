using Microsoft.EntityFrameworkCore;
using TicketBoard.DAL.Data;
using TicketBoard.DAL.Data.Entity;
using TicketBoard.DAL.Repositories.Abstractions;

namespace TicketBoard.DAL.Repositories;

public class PlaceRepository : IPlaceRepository
{
    private readonly AppDbContext _dbContext;

    public PlaceRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<PlaceEntity>> GetAllOrEmpty(bool loadRelatedEntities = false)
    {
        var places = _dbContext.Places.AsQueryable();
        if (!await places.AnyAsync())
        {
            return Enumerable.Empty<PlaceEntity>();
        }

        if (loadRelatedEntities)
        {
            places
                .Include(p => p.Tickets)
                .Include(p => p.Location);
        }

        return await places.ToListAsync();
    }
}