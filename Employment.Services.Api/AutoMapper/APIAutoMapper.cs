using AutoMapper;
using Employment.Application.DTOs;
using Employment.Application.ViewModels;
using Employment.Services.Api.Models;

namespace Employment.Services.Api.AutoMapper
{
    public class APIAutoMapper : Profile
    {
        public APIAutoMapper()
        {
            CreateMap<UserModel, UserDto>().ReverseMap();

            CreateMap<VacancyModel, VacancyDTO>().ReverseMap();


        }
    }
}
