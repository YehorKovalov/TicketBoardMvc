using TicketBoard.BLL.Models;

namespace TicketBoard.BLL.Services.Abstractions;

public interface ITicketService
{
    Task<GetTicketByIdResponse<TicketWithRelatedDataDto?>?> GetTicketWithRelatedDataByIdAsync(int? ticketId);
    Task<GetAllTicketsResponse<TicketWithoutRelatedDataDto>> GetAllTicketsWithoutRelatedEntitiesAsync();
    Task<AddTicketResponse<int>> AddTicketAsync(int placeId, string? description, DateTime date, double price);
    Task<UpdateTicketResponse<int>> UpdateTicketAsync(int ticketId, int placeId, string? description, DateTime date, double price);
    Task<DeleteTicketResponse<int?>> DeleteTicketByIdAsync(int? ticketId);
    Task<GetAllTicketsResponse<TicketWithRelatedDataDto>> GetAllTicketsWithRelatedEntitiesAsync();
    Task<GetTicketByIdResponse<TicketWithoutRelatedDataDto?>?> GetTicketWithoutRelatedDataByIdAsync(int? ticketId);
}