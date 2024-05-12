using Employment.Application.DTOs;
using Employment.Application.ViewModels;
using Employment.Domain.Common;
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
        Task<ResponseModel<List<VacancyDTO>>> GetAllVacancies();
        Task<ResponseModel<bool>> AddVacancy (VacancyDTO vacancyDTO);
        Task<ResponseModel<bool>> EditVacancy(VacancyDTO vacancyDTO);
        Task<ResponseModel<bool>> DeleteVacancy(int vacancyId);
        Task<bool> PostVacancy(int vacancyId);
        Task<bool> DeactivateVacancy(int vacancyId);

        bool CheckApplicationMaxNumber(int vacancyId);


	}
}
