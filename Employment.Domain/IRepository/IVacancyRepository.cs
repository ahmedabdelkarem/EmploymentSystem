using Employment.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employment.Domain.IRepository
{
    public interface IVacancyRepository
    {
 
        Task<IEnumerable<List<Vacancy>>> GetAllVacancies();

        Task<bool> AddVacancy(Vacancy vacancy);

        Task<bool> EditVacancy(Vacancy vacancy);

        Task<bool> DeleteVacancy(int vacancyId);

        Task<bool> DeactivateVacancy(int vacancyId);

        Task<bool> ApplytoVacancy(string userId, int vacancyId);
    }
}
