using AutoMapper;
using Employment.Application.DTOs;
using Employment.Application.IServices;
using Employment.Application.ViewModels;
using Employment.Domain.Entities;
using Employment.Domain.IRepository;
using Employment.Infra.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employment.Application.Services
{
	public class VacanciesApplicationService : GenericService, IVacanciesApplicationService
	{
		protected readonly IVacanciesApplicationRepository _vacanciesApplicationRepository;
		protected readonly IVacancyService _vacancyService; 

		public VacanciesApplicationService(IVacanciesApplicationRepository vacanciesApplicationRepository,
			IVacancyService vacancyService ,IMapper mapper) : base(mapper)
		{
			_vacanciesApplicationRepository = vacanciesApplicationRepository;
			_vacancyService = vacancyService;
		}

		public async Task<bool> ApplytoVacancy(string userId, int vacancyId)
		{

			if ((!string.IsNullOrEmpty(userId)) && vacancyId != 0)
			{
				if (ValidateApplication(userId, vacancyId))
				{
					VacanciesApplicationDTO dto = new VacanciesApplicationDTO
					{
						ApplicationDate = DateTime.Now,
						FkApplicantId = userId,
						FkVacancyId = vacancyId
					};
					return await _vacanciesApplicationRepository.ApplytoVacancy(_mapper.Map<VacanciesApplication>(dto));
				}



			}
			return false;
		}

		public  async Task<List<string>> GetAllVacancyApplicants(int vacancyId)
		{
			List<string> applicantsUser = new List<string>();

			if (vacancyId != 0)
			{
				  applicantsUser = await _vacanciesApplicationRepository.GetAllVacancyApplicants(vacancyId);
			}
			return applicantsUser;
		}


		#region ValidationFunctions
		private bool ValidateApplication(string userId, int vacancyId)
		{
			//check Existence of same application with same vacancy
			if (CheckApplicationExist(userId, vacancyId) || CheckApplicationMaxTime(userId) ||
													    !_vacancyService.CheckApplicationMaxNumber(vacancyId))
			{
				return false;
			}

			return true;
		}

		private bool CheckApplicationExist(string userId, int vacancyId)
		{
			var results = _vacanciesApplicationRepository.CheckApplicationExist(userId, vacancyId);
			if (results != null && results.Count > 0)
			{
				return true;
			}
			return false;
		}

		private bool CheckApplicationMaxTime(string userId)
		{

			var results = _vacanciesApplicationRepository.CheckApplicationMaxTime(userId);
			if (results != null && results.Count > 0)
			{
				return true;
			}
			return false;
		}

		#endregion

	}
}
