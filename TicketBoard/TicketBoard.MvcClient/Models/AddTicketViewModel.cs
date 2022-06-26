using System.ComponentModel.DataAnnotations;

namespace TicketBoard.MvcClient.Models;

public class AddTicketViewModel
{
    [Required]
    public int PlaceId { get; set; }

    [Required]
    public double Price { get; set; }

    [MaxLength(2000)]
    public string? Description { get; set; }

    [Required]
    public DateTime Date { get; set; }
}