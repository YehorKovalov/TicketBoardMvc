namespace TicketBoard.BLL.Models;

public class PlaceDto
{
    public int PlaceId { get; set; }
    public string Name { get; set; } = null!;

    public int LocationId { get; set; }
    public LocationDto Location { get; set; } = null!;
    public IEnumerable<TicketDto> Tickets { get; set; } = null!;
}