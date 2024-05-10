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
        Task<IEnumerable<bool>> AddVacancy(VacancyDTO vacancyDTO);
        Task<IEnumerable<bool>> EditVacancy(VacancyDTO vacancyDTO);
        Task<IEnumerable<bool>> DeleteVacancy(int vacancyId);
        Task<IEnumerable<bool>> PostVacancy(int vacancyId);
        Task<IEnumerable<bool>> DeactivateVacancy(int vacancyId);
        Task<IEnumerable<bool>> ApplytoVacancy(int userId, int vacancyId);
        Task<IEnumerable<List<UserDto>>> GetAllVacancyApplicants(int vacancyId);
       
    }
}
