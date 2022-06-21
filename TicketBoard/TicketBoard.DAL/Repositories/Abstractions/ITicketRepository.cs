using TicketBoard.DAL.Data.Entity;

namespace TicketBoard.DAL.Repositories.Abstractions;

public interface ITicketRepository
{
    Task<TicketEntity?> GetById(int ticketId);
    Task<IEnumerable<TicketEntity>> GetAll();
    Task<int?> Add(string title, string destinationPlace, string? description);
    Task<int?> Update(int ticketId, string title, string destinationPlace, string? description);
    Task<int?> DeleteById(int ticketId);
}