using AutoMapper;
using Services.Catolog.Dto;
using Services.Catolog.Dto.Course;
using Services.Catolog.Model;

namespace Services.Catolog.Mapping
{
    internal class GeneralMapping : Profile
    {
        internal GeneralMapping()
        {
            CreateMap<Course, CourseDto>().ReverseMap();
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Feature, FeatureDto>().ReverseMap();

            CreateMap<Course, CourseCreateDto>().ReverseMap();
            CreateMap<Course, CourseUpdateDto>().ReverseMap();
        }
    }
}