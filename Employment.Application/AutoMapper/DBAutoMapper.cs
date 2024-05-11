using AutoMapper;
using Employment.Application.DTOs;
using Employment.Application.ViewModels;
using Employment.Domain.Entities;
using Employment.Infra.Data;

namespace Employment.Application.AutoMapper
{
    public class DBAutoMapper : Profile
    {
        public DBAutoMapper()
        {
            CreateMap<AspNetUser, UserDto>().ReverseMap();

            CreateMap<Vacancy, VacancyDTO>().ReverseMap();

        }
    }
}
