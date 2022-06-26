namespace TicketBoard.BLL.Models;

public class TicketDto
{
    public int TicketId { get; set; }

    public double Price { get; set; }
    public string? Description { get; set; }
    public DateTime Date { get; set; }
    public int PlaceId { get; set; }

    public PlaceDto Place { get; set; } = null!;
}