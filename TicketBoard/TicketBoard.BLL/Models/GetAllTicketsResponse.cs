namespace TicketBoard.BLL.Models;

public class GetAllTicketsResponse<TData>
{
    public IEnumerable<TData> Data { get; set; } = null!;
}