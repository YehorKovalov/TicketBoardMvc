using TicketBoard.DAL.Data.Entity;

namespace TicketBoard.DAL.Repositories.Abstractions;

public interface IPlaceRepository
{
    Task<IEnumerable<PlaceEntity>> GetAllOrEmpty(bool loadRelatedEntities = false);
}