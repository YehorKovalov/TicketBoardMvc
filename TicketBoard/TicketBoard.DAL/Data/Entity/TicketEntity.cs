namespace TicketBoard.DAL.Data.Entity;

public class TicketEntity
{
    public int TicketId { get; set; }

    public double Price { get; set; }
    public string? Description { get; set; }
    public DateTime Date { get; set; }

    public int PlaceId { get; set; }
    public PlaceEntity Place { get; set; } = null!;
}