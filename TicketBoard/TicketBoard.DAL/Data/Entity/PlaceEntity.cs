namespace TicketBoard.DAL.Data.Entity;

public class PlaceEntity
{
    public int PlaceId { get; set; }

    public string Name { get; set; } = null!;

    public int LocationId { get; set; }
    public LocationEntity Location { get; set; } = null!;

    public IEnumerable<TicketEntity> Tickets { get; set; } = null!;
}