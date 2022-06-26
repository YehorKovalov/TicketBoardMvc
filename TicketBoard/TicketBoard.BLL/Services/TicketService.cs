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

    public async Task<GetTicketByIdResponse<TicketWithRelatedDataDto?>?> GetTicketWithRelatedDataByIdAsync(int? ticketId)
    {
        if (ticketId is <= 0 or null)
        {
            return null;
        }

        var result = await _ticketRepository.GetById(ticketId.Value, true);
        _logger.LogInformation($"ticket id = {result.TicketId}");
        return new GetTicketByIdResponse<TicketWithRelatedDataDto?> { Data  = _mapper.Map<TicketWithRelatedDataDto>(result) };
    }

    public async Task<GetTicketByIdResponse<TicketWithoutRelatedDataDto?>?> GetTicketWithoutRelatedDataByIdAsync(int? ticketId)
    {
        if (ticketId is <= 0 or null)
        {
            return null;
        }

        var result = await _ticketRepository.GetById(ticketId.Value);
        _logger.LogInformation($"ticket id = {result.TicketId}");
        return new GetTicketByIdResponse<TicketWithoutRelatedDataDto?> { Data  = _mapper.Map<TicketWithoutRelatedDataDto>(result) };
    }
    
    public async Task<GetAllTicketsResponse<TicketWithRelatedDataDto>> GetAllTicketsWithRelatedEntitiesAsync()
    {
        var result = await _ticketRepository.GetAll(true);
        _logger.LogInformation($"tickets amount = {result.Count()}");
        return new GetAllTicketsResponse<TicketWithRelatedDataDto>
        {
            Data = result.Select(s => _mapper.Map<TicketWithRelatedDataDto>(s)).ToList()
        };
    }

    public async Task<GetAllTicketsResponse<TicketWithoutRelatedDataDto>> GetAllTicketsWithoutRelatedEntitiesAsync()
    {
        var result = await _ticketRepository.GetAll();
        _logger.LogInformation($"tickets amount = {result.Count()}");
        return new GetAllTicketsResponse<TicketWithoutRelatedDataDto>
        {
            Data = result.Select(s => _mapper.Map<TicketWithoutRelatedDataDto>(s)).ToList()
        };
    }

    public async Task<AddTicketResponse<int>> AddTicketAsync(int placeId, string? description, DateTime date, double price)
    {
        _logger.LogInformation($"State: {nameof(placeId)}: {placeId}; {nameof(date)}: {date}; {nameof(description)}: {description}; {nameof(price)}: {price};");
        var result = await _ticketRepository.Add(placeId, description, date, price);
        _logger.LogInformation($"ticket id = {result}");
        if (result is null)
        {
            throw new Exception($"{nameof(AddTicketAsync)} ---> result is null");
        }

        return new AddTicketResponse<int> { Data = result.Value };
    }

    public async Task<UpdateTicketResponse<int>> UpdateTicketAsync(int ticketId, int placeId, string? description, DateTime date, double price)
    {
        _logger.LogInformation($"State: {nameof(ticketId)}: {ticketId}; {nameof(placeId)}: {placeId}; {nameof(date)}: {date}; {nameof(description)}: {description}; {nameof(price)}: {price};");
        var result = await _ticketRepository.Update(ticketId, placeId, description, date, price);
        _logger.LogInformation($"ticket id = {result}");
        if (result is null)
        {
            throw new Exception($"{nameof(UpdateTicketAsync)} ---> result is null");
        }

        return new UpdateTicketResponse<int> { Data = result.Value };
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

        return new DeleteTicketResponse<int?> { Data = result.Value };
    }
}