using AutoMapper;
using Project.Services.Category.DTOs;
using Project.Services.Category.Models;

namespace Project.Services.Category.Mapping
{
    public class CategoryMapping : Profile // Profile: AutoMapper
    {
        public CategoryMapping()
        {
            CreateMap<Product, ProductDTO>().ReverseMap();
            CreateMap<Models.Category, CategoryDTO>().ReverseMap(); // Models.Category --> confuse with Category Folder
            CreateMap<Feature, FeatureDTO>().ReverseMap();

            CreateMap<Product, ProductCreateDTO>().ReverseMap();
            CreateMap<Product, ProductUpdateDTO>().ReverseMap();
        }
    }
}
