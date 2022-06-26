namespace TicketBoard.MvcClient.Models;

public class PlaceViewModel
{
    public int PlaceId { get; set; }
    public string Name { get; set; } = null!;

    public int LocationId { get; set; }
    public LocationViewModel Location { get; set; } = null!;
    public IEnumerable<TicketsViewModel> Tickets { get; set; } = null!;
}