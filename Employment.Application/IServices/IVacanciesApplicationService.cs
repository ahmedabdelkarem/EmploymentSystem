using Employment.Application.ViewModels;
using Employment.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employment.Application.IServices
{
	public interface IVacanciesApplicationService
	{
        Task<ResponseModel<bool>>  ApplytoVacancy(string userId, int vacancyId);

        Task<ResponseModel<List<string>>> GetAllVacancyApplicants(int vacancyId);
	}
}
