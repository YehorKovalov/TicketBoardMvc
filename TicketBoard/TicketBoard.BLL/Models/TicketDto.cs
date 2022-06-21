namespace TicketBoard.BLL.Models;

public class TicketDto
{
    public int TicketId { get; set; }

    public string Title { get; set; } = null!;

    public string DestinationPlace { get; set; } = null!;

    public string? Description { get; set; }
}