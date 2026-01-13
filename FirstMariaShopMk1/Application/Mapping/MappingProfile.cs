using AutoMapper;
using MariaShop.Api.Application.DTOs;
using MariaShop.Api.Models;

namespace MariaShop.Api.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile() {
            CreateMap<Product, ProductDTO>().ReverseMap();
            CreateMap<Category, CategoryDTO>().ReverseMap();
        }
    }
}
