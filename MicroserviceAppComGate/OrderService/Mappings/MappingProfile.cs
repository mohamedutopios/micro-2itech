using AutoMapper;
using OrderService.DTO;
using OrderService.Models;

namespace OrderService.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Order, OrderDTO>().ReverseMap();
        }
    }
}
