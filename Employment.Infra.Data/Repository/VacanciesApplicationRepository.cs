using Employment.Domain.Entities;
using Employment.Domain.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Employment.Infra.Data.Repository
{
	public class VacanciesApplicationRepository : GenericRepository<VacanciesApplication>, IVacanciesApplicationRepository
	{
		public VacanciesApplicationRepository(EmploymentContext DBContext) : base(DBContext)
		{
		}

		public async Task<bool> ApplytoVacancy(VacanciesApplication vacanciesApplication)
		{
            bool Inserted = false;

            using (TransactionScope Scope = new TransactionScope())
			{

                Inserted = Insert(_dbContext, vacanciesApplication);

				if (Inserted)
				{
                    var currentApp = _dbContext.VacanciesApplications.
                                                   Where(a => a.FkVacancyId == vacanciesApplication.FkVacancyId)
                                                  .FirstOrDefault();

                    if (currentApp != null)
                    {
                       currentApp.FkVacancy.CurrentNumberOfApplication++;
                       Inserted =  Update(_dbContext, currentApp);
                    }
					if (Inserted)
					{
                        Scope.Complete();
                    }
                    Scope.Dispose();
                }
                
            }
			return Inserted;
		}

		public List<VacanciesApplication> CheckApplicationExist(string userId, int vacancyId)
		{
			List<VacanciesApplication> vacancyApp = new List<VacanciesApplication>();

			vacancyApp = _dbContext.VacanciesApplications.Where(a => a.FkApplicantId.Trim() == userId.Trim()
			&& a.FkVacancyId == vacancyId).ToList();

			return vacancyApp;

		}
		public List<VacanciesApplication> CheckApplicationMaxTime(string userId)
		{
			List<VacanciesApplication> vacancyApp = new List<VacanciesApplication>();

			vacancyApp = _dbContext.VacanciesApplications.Where(a => a.FkApplicantId.Trim() == userId.Trim()
			&& a.ApplicationDate.Value.Date == DateTime.Now.Date).ToList();

			return vacancyApp;
		}



		public async Task<List<string>> GetAllVacancyApplicants(int vacancyId)
		{
			var result = _dbContext.VacanciesApplications.Include(vac => vac.FkApplicant)
				.Where(vac => vac.FkVacancyId == vacancyId)
				.Select (a=> a.FkApplicant.UserName).ToList();
				 
			return result;
		}
	}
}
