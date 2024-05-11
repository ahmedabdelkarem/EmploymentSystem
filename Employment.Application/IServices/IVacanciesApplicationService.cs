using Employment.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employment.Application.IServices
{
	public interface IVacanciesApplicationService
	{
		Task<bool> ApplytoVacancy(string userId, int vacancyId);

		Task<List<string>> GetAllVacancyApplicants(int vacancyId);
	}
}
