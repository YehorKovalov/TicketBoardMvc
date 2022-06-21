using TicketBoard.BLL.Models;

namespace TicketBoard.BLL.Services.Abstractions;

public interface ITicketService
{
    Task<GetTicketByIdResponse<TicketDto?>?> GetTicketByIdAsync(int? ticketId);
    Task<GetAllTicketsResponse<TicketDto>> GetAllTicketsAsync();
    Task<AddTicketResponse<int>> AddTicketAsync(string title, string destinationPlace, string? description);
    Task<UpdateTicketResponse<int>> UpdateTicketAsync(int ticketId, string title, string destinationPlace, string? description);
    Task<DeleteTicketResponse<int?>> DeleteTicketByIdAsync(int? ticketId);
}