using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TicketBoard.DAL.Data;

namespace TicketBoard.DAL.Extensions;

public static class DatabaseExtensions
{
    public static void CreateDbIfDoestExist<TDbContext>(this IHost app, IDbInitializer<TDbContext> initializer)
        where TDbContext : DbContext
    {
        using var scope = app.Services.CreateScope();
        var services = scope.ServiceProvider;
        try
        {
            var context = services.GetRequiredService<TDbContext>() ?? throw new Exception("DbContext service hasn't added to IoC");
            initializer.Initialize(context).Wait();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}