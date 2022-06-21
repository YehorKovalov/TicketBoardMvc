using AutoMapper;
using Microsoft.Extensions.Logging;
using TicketBoard.BLL.Models;
using TicketBoard.BLL.Services.Abstractions;
using TicketBoard.DAL.Repositories.Abstractions;

namespace TicketBoard.BLL.Services;

public class TicketService : ITicketService
{
    private readonly ITicketRepository _ticketRepository;
    private readonly ILogger<TicketService> _logger;
    private readonly IMapper _mapper;

    public TicketService(
        ITicketRepository ticketRepository,
        ILogger<TicketService> logger,
        IMapper mapper)
    {
        _ticketRepository = ticketRepository;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<GetTicketByIdResponse<TicketDto?>?> GetTicketByIdAsync(int? ticketId)
    {
        if (ticketId is <= 0 or null)
        {
            return null;
        }

        var result = await _ticketRepository.GetById(ticketId.Value);
        _logger.LogInformation($"ticket id = {result.TicketId}");
        return new GetTicketByIdResponse<TicketDto?>
        {
            Data  = _mapper.Map<TicketDto>(result)
        };
    }

    public async Task<GetAllTicketsResponse<TicketDto>> GetAllTicketsAsync()
    {
        var result = await _ticketRepository.GetAll();
        _logger.LogInformation($"tickets amount = {result.Count()}");
        return new GetAllTicketsResponse<TicketDto>
        {
            Data = result.Select(s => _mapper.Map<TicketDto>(s)).ToList()
        };
    }

    public async Task<AddTicketResponse<int>> AddTicketAsync(string title, string destinationPlace, string? description)
    {
        _logger.LogInformation($"State: {nameof(title)}: {title}; {nameof(destinationPlace)}: {destinationPlace}; {nameof(description)}: {description};");
        var result = await _ticketRepository.Add(title, destinationPlace, description);
        _logger.LogInformation($"ticket id = {result}");
        if (result is null)
        {
            throw new Exception($"{nameof(AddTicketAsync)} ---> result is null");
        }

        return new AddTicketResponse<int>
        {
            Data = result.Value
        };
    }

    public async Task<UpdateTicketResponse<int>> UpdateTicketAsync(int ticketId, string title, string destinationPlace, string? description)
    {
        _logger.LogInformation($"State: {nameof(ticketId)}: {ticketId}; {nameof(title)}: {title}; {nameof(destinationPlace)}: {destinationPlace}; {nameof(description)}: {description};");
        var result = await _ticketRepository.Update(ticketId, title, destinationPlace, description);
        _logger.LogInformation($"ticket id = {result}");
        if (result is null)
        {
            throw new Exception($"{nameof(UpdateTicketAsync)} ---> result is null");
        }

        return new UpdateTicketResponse<int>
        {
            Data = result.Value
        };
    }

    public async Task<DeleteTicketResponse<int?>> DeleteTicketByIdAsync(int? ticketId)
    {
        if (ticketId is <= 0 or null)
        {
            return new DeleteTicketResponse<int?> { Data = null };
        }

        var result = await _ticketRepository.DeleteById(ticketId.Value);
        _logger.LogInformation($"ticket id = {result}");
        if (result is null)
        {
            throw new Exception($"{nameof(DeleteTicketByIdAsync)} ---> result is null");
        }

        return new DeleteTicketResponse<int?>
        {
            Data = result.Value
        };
    }
}