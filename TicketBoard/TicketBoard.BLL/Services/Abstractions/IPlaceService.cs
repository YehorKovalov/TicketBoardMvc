using TicketBoard.BLL.Models;

namespace TicketBoard.BLL.Services.Abstractions;

public interface IPlaceService
{
    Task<GetAllPlacesResponse<PlaceDto>> GetAllPlacesWithoutRelatedDataAsync();
}