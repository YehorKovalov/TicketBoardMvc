namespace TicketBoard.MvcClient.Models;

public class TicketViewModel
{
    public int TicketId { get; set; }

    public double Price { get; set; }
    public string? Description { get; set; }
    public DateTime Date { get; set; }
    public int PlaceId { get; set; }
    public PlaceViewModel Place { get; set; } = null!;
}