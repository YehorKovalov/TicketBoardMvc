using Microsoft.EntityFrameworkCore;

namespace TicketBoard.DAL.Data;

public interface IDbInitializer<in TDbContext>
    where TDbContext : DbContext
{
    Task Initialize(TDbContext dbContext);
}