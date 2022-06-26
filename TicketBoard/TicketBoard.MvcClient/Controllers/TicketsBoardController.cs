using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TicketBoard.BLL.Models;
using TicketBoard.BLL.Services.Abstractions;
using TicketBoard.MvcClient.Models;

namespace TicketBoard.MvcClient.Controllers;

public class TicketsBoardController : Controller
{
    private readonly ITicketService _ticketService;
    private readonly IPlaceService _placeService;
    private readonly IMapper _mapper;

    public TicketsBoardController(ITicketService ticketService, IMapper mapper, IPlaceService placeService)
    {
        _ticketService = ticketService;
        _mapper = mapper;
        _placeService = placeService;
    }

    [HttpGet]
    public async Task<IActionResult> Tickets()
    {
        var tickets = await _ticketService.GetAllTicketsWithRelatedEntitiesAsync();
        var viewModels = new TicketsViewModel
        {
            Data = tickets.Data.Select(s => _mapper.Map<TicketViewModel>(s)).ToList()
        };
        
        return View(viewModels);
    }

    [HttpGet]
    public IActionResult ReserveTicket() => View();

    [HttpPost]
    public async Task<IActionResult> ReserveTicket(AddTicketViewModel model)
    {
        var ticketId = await _ticketService.AddTicketAsync(model.PlaceId, model.Description, model.Date, model.Price);
        return RedirectToAction("Tickets");
    }

    [HttpGet]
    public async Task<IActionResult> UpdateTicket(int? id)
    {
        var ticket = await _ticketService.GetTicketWithoutRelatedDataByIdAsync(id);
        var places = await _placeService.GetAllPlacesWithoutRelatedDataAsync();
        var viewModel = new UpdateTicketViewModel
        {
            PlacesSelectList = BuildPlacesViewModel(places.Data),
            Ticket = _mapper.Map<TicketViewModel>(ticket.Data)
        };
        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> UpdateTicket(UpdateTicketViewModel model)
    {
        var ticketId = await _ticketService.UpdateTicketAsync(model.Ticket.TicketId, model.Ticket.PlaceId, model.Ticket.Description, model.Ticket.Date, model.Ticket.Price);
        return RedirectToAction("Tickets");
    }

    [HttpGet]
    public async Task<IActionResult> DeleteTicket(int? id)
    {
        Console.WriteLine("delete");
        var ticketId = await _ticketService.DeleteTicketByIdAsync(id);
        if (ticketId.Data == null)
        {
            return BadRequest();
        }

        return RedirectToAction("Tickets");
    }

    [NonAction]
    private PlacesSelectListViewModel BuildPlacesViewModel(IEnumerable<PlaceDto> placeDtos)
    {
        return new PlacesSelectListViewModel
        {
            Data = placeDtos.Select(s => new SelectListItem
            {
                Value = s.PlaceId.ToString(),
                Text = s.Name
            })
        };
    }
}