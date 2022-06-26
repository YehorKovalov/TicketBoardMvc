namespace TicketBoard.DAL.Data.Entity;

public class LocationEntity
{
    public int LocationId { get; set; }

    public string Name { get; set; } = null!;

    public IEnumerable<PlaceEntity> Places { get; set; } = null!;
}