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
        
        CreateMap<TicketDto, TicketViewModel>();
        CreateMap<TicketDto, UpdateTicketViewModel>();
    }
}