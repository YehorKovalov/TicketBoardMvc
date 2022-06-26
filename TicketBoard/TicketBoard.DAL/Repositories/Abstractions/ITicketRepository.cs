using TicketBoard.DAL.Data.Entity;

namespace TicketBoard.DAL.Repositories.Abstractions;

public interface ITicketRepository
{
    Task<TicketEntity?> GetById(int ticketId, bool loadRelatedEntities = false);
    Task<IEnumerable<TicketEntity>> GetAll(bool loadRelatedEntities = false);
    Task<int?> Add(int placeId, string? description, DateTime date, double price);
    Task<int?> Update(int ticketId, int placeId, string? description, DateTime date, double price);
    Task<int?> DeleteById(int ticketId);
}