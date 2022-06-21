using System.ComponentModel.DataAnnotations;

namespace TicketBoard.MvcClient.Models;

public class TicketViewModel
{
    public int TicketId { get; set; }

    [Required]
    [MaxLength(100)]
    public string Title { get; set; } = null!;

    [Required]
    [MaxLength(200)]
    public string DestinationPlace { get; set; } = null!;

    [Required]
    [MaxLength(2000)]
    public string? Description { get; set; }
}