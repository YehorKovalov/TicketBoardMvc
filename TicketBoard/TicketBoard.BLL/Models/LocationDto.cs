namespace TicketBoard.BLL.Models;

public class LocationDto
{
    public int LocationId { get; set; }

    public string Name { get; set; } = null!;

    public IEnumerable<PlaceDto> Places { get; set; } = null!;
}