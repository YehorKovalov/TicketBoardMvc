using AutoMapper;
using Microsoft.Extensions.Logging;
using TicketBoard.BLL.Models;
using TicketBoard.BLL.Services.Abstractions;
using TicketBoard.DAL.Repositories.Abstractions;

namespace TicketBoard.BLL.Services;

public class PlaceService : IPlaceService
{
    private readonly IPlaceRepository _placeRepository;
    private readonly ILogger<PlaceService> _logger;
    private readonly IMapper _mapper;
    public PlaceService(IPlaceRepository placeRepository, ILogger<PlaceService> logger, IMapper mapper)
    {
        _placeRepository = placeRepository;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<GetAllPlacesResponse<PlaceDto>> GetAllPlacesWithoutRelatedDataAsync()
    {
        var result = await _placeRepository.GetAllOrEmpty();
        _logger.LogInformation($"tickets amount = {result.Count()}");
        return new GetAllPlacesResponse<PlaceDto>
        {
            Data = result.Select(s => _mapper.Map<PlaceDto>(s)).ToList()
        };
    }
}