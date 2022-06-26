namespace TicketBoard.MvcClient.Models;

public class LocationViewModel
{
    public int LocationId { get; set; }

    public string Name { get; set; } = null!;

    public IEnumerable<PlaceViewModel> Places { get; set; } = null!;
}