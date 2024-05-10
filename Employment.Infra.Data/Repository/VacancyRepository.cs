using Employment.Domain.Entities;
using Employment.Domain.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employment.Infra.Data.Repository
{
    public class VacancyRepository : GenericRepository<Vacancy>, IVacancyRepository
    {
        public VacancyRepository(EmploymentContext DBContext) : base(DBContext)
        {
        }

        public async Task<IEnumerable<List<Vacancy>>> GetAllVacancies()
        {
           var result =  _dbContext.Vacancies.Where(vac => vac.IsActive == true
                                        && vac.IsExpired == false
                                        && vac.IsPosted == true);

            return (IEnumerable<List<Vacancy>>)result;
        }
        public async Task<bool> AddVacancy(Vacancy vacancy)
        {
            var result =  Insert(_dbContext, vacancy);
            return result;


        }
    }
}
