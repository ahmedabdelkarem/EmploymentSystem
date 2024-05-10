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
    }
}
