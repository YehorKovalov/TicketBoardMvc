using Microsoft.EntityFrameworkCore;
using TicketBoard.BLL.Services;
using TicketBoard.BLL.Services.Abstractions;
using TicketBoard.DAL.Data;
using TicketBoard.DAL.Extensions;
using TicketBoard.DAL.Repositories;
using TicketBoard.DAL.Repositories.Abstractions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddTransient<ITicketRepository, TicketRepository>();
builder.Services.AddTransient<ITicketService, TicketService>();

builder.Services.AddDbContextFactory<AppDbContext>(o =>
{
    var connectionString = builder.Configuration.GetConnectionString("ConnectionString");
    o.UseSqlServer(connectionString);
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=TicketsBoard}/{action=Tickets}/{id?}");

app.CreateDbIfDoestExist(new AppDbInitializer());
app.Run();