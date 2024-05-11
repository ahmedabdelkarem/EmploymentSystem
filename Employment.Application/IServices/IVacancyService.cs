using Employment.Application.DTOs;
using Employment.Application.ViewModels;
using Employment.Infra.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employment.Application.IServices
{
    public interface IVacancyService : IDisposable
    {
        Task<IEnumerable<List<VacancyDTO>>> GetAllVacancies();
        Task<bool> AddVacancy (VacancyDTO vacancyDTO);
        Task<bool> EditVacancy(VacancyDTO vacancyDTO);
        Task<bool> DeleteVacancy(int vacancyId);
        Task<bool> PostVacancy(int vacancyId);
        Task<bool> DeactivateVacancy(int vacancyId);
        Task<bool> ApplytoVacancy(string userId, int vacancyId);
        Task<IEnumerable<List<UserDto>>> GetAllVacancyApplicants(int vacancyId);
       
    }
}
