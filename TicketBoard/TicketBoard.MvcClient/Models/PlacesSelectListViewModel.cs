using Microsoft.AspNetCore.Mvc.Rendering;

namespace TicketBoard.MvcClient.Models;

public class PlacesSelectListViewModel
{
    public IEnumerable<SelectListItem> Data { get; set; } = null!;
}