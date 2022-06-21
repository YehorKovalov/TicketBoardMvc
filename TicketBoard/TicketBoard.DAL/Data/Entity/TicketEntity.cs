namespace TicketBoard.DAL.Data.Entity;

public class TicketEntity
{
    public int TicketId { get; set; }

    public string Title { get; set; } = null!;

    public string DestinationPlace { get; set; } = null!;

    public string? Description { get; set; }
}