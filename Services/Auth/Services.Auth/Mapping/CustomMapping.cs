using AutoMapper;
using Services.Auth.Dto;
using Services.Auth.Model;

namespace Services.Auth.Mapping
{
    public class CustomMapping : Profile
    {
        public CustomMapping()
        {
            CreateMap<UserDto, User>().ReverseMap();
            CreateMap<UserLoginDto, User>().ReverseMap();
            CreateMap<UserRegisterDto, User>().ReverseMap();
        }
    }
}