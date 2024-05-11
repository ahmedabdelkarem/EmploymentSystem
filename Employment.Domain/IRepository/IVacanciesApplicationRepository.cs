using Employment.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employment.Domain.IRepository
{
	public interface IVacanciesApplicationRepository
	{

		Task<bool> ApplytoVacancy(VacanciesApplication vacanciesApplication);

		List<VacanciesApplication> CheckApplicationExist(string userId, int vacancyId);

		List<VacanciesApplication> CheckApplicationMaxTime(string userId);

		Task<List<string>> GetAllVacancyApplicants(int vacancyId);
	}
}
