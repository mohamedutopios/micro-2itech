using AutoMapper;
using OrderService.DTO;
using OrderService.Models;

namespace OrderService.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Mapping de Order vers OrderDTO et inversement
            CreateMap<Order, OrderDTO>().ReverseMap();

            // Si nécessaire, ajouter d'autres mappings
            // CreateMap<Product, ProductDTO>().ReverseMap();
            // CreateMap<Customer, CustomerDTO>().ReverseMap();
        }
    }
}
