using AutoMapper;
using TicketBoard.BLL.Models;
using TicketBoard.DAL.Data.Entity;
using TicketBoard.MvcClient.Models;

namespace TicketBoard.MvcClient.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<TicketEntity, TicketDto>();
        CreateMap<TicketEntity, TicketWithRelatedDataDto>();
        CreateMap<TicketEntity, TicketWithoutRelatedDataDto>();
        CreateMap<PlaceEntity, PlaceDto>();
        CreateMap<LocationEntity, LocationDto>();

        CreateMap<TicketDto, TicketsViewModel>();
        CreateMap<TicketWithRelatedDataDto, TicketViewModel>();
        CreateMap<TicketWithoutRelatedDataDto, TicketViewModel>();
        CreateMap<LocationDto, LocationViewModel>();
        CreateMap<PlaceDto, PlaceViewModel>();
    }
}