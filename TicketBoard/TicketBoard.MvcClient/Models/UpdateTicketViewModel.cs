using System.ComponentModel.DataAnnotations;

namespace TicketBoard.MvcClient.Models;

public class UpdateTicketViewModel
{
    public TicketViewModel Ticket { get; set; } = null!;

    public PlacesSelectListViewModel PlacesSelectList { get; set; } = null!;
}