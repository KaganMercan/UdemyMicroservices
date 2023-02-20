using AutoMapper;
using FreeCourse.Services.Catalog.Models;
using FreeCourse.Services.Catalog.Dtos;

namespace FreeCourse.Services.Catalog.Mapping
{
    public class GeneralMapping:Profile
    {
        // Similar to JAVA(ModelMapper)
        public GeneralMapping() {
            // Convert from object to Dto]
            CreateMap<Course, CourseDto>().ReverseMap();
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Feature, FeatureDto>().ReverseMap();
            CreateMap<Course, CourseCreateDto>().ReverseMap();
            CreateMap<Course, CourseUpdateDto>().ReverseMap();
        } 
    }
}
