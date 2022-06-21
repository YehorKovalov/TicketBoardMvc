using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TicketBoard.BLL.Services.Abstractions;
using TicketBoard.MvcClient.Models;

namespace TicketBoard.MvcClient.Controllers;

public class TicketsBoardController : Controller
{
    private readonly ITicketService _ticketService;
    private readonly IMapper _mapper;

    public TicketsBoardController(ITicketService ticketService, IMapper mapper)
    {
        _ticketService = ticketService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> Tickets()
    {
        var tickets = await _ticketService.GetAllTicketsAsync();
        var viewModels = tickets.Data.Select(s => _mapper.Map<TicketViewModel>(s)).ToList();
        return View(viewModels);
    }

    [HttpGet]
    public IActionResult AddTicket() => View();

    [HttpPost]
    public async Task<IActionResult> AddTicket(AddTicketViewModel model)
    {
        var ticketId = await _ticketService.AddTicketAsync(model.Title, model.DestinationPlace, model.Description);
        return RedirectToAction("Tickets");
    }

    [HttpGet]
    public async Task<IActionResult> UpdateTicket(int? id)
    {
        var ticket = await _ticketService.GetTicketByIdAsync(id);
        var viewModel = _mapper.Map<UpdateTicketViewModel>(ticket!.Data);
        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> UpdateTicket(UpdateTicketViewModel model)
    {
        var ticketId = await _ticketService.UpdateTicketAsync(model.TicketId, model.Title, model.DestinationPlace, model.Description);
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
}