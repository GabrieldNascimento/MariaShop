using AutoMapper;
using FirstMariaShopMk1.Application.DTOs;
using FirstMariaShopMk1.Models;

namespace FirstMariaShopMk1.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductDTO>().ReverseMap();
            CreateMap<Category, CategoryDTO>().ReverseMap();
        }
    }
}
