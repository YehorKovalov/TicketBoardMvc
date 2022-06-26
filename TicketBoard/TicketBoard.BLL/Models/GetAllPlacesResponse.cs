namespace TicketBoard.BLL.Models;

public class GetAllPlacesResponse<TData>
{
    public IEnumerable<TData> Data { get; set; } = null!;
}