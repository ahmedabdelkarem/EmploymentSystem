using AutoMapper;
using Employment.Application.DTOs;
using Employment.Application.IServices;
using Employment.Application.ViewModels;
using Employment.Domain.Entities;
using Employment.Domain.IRepository;
using Employment.Infra.Data.Repository;
using Microsoft.Extensions.Logging;
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
			IVacancyService vacancyService ,IMapper mapper, ILogger logger) : base(mapper, logger)
		{
			_vacanciesApplicationRepository = vacanciesApplicationRepository;
			_vacancyService = vacancyService;
		}

		public async Task<bool> ApplytoVacancy(string userId, int vacancyId)
		{
			try
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
			catch (Exception Ex)
			{
				_logger.LogInformation(Ex.Message);

				throw;
			}
		}

		public  async Task<List<string>> GetAllVacancyApplicants(int vacancyId)
		{
			try
			{

			List<string> applicantsUser = new List<string>();

			if (vacancyId != 0)
			{
				  applicantsUser = await _vacanciesApplicationRepository.GetAllVacancyApplicants(vacancyId);
			}
			return applicantsUser;
			}
			catch (Exception Ex)
			{
				_logger.LogInformation(Ex.Message);

				throw;
			}
		}


		#region ValidationFunctions
		private bool ValidateApplication(string userId, int vacancyId)
		{
			try
			{
			//check Existence of same application with same vacancy
			if (CheckApplicationExist(userId, vacancyId) || CheckApplicationMaxTime(userId) ||
													    !_vacancyService.CheckApplicationMaxNumber(vacancyId))
			{
				return false;
			}

			return true;

			}
			catch (Exception Ex)
			{
				_logger.LogInformation(Ex.Message);

				throw;
			}
		}

		private bool CheckApplicationExist(string userId, int vacancyId)
		{
			try
			{
			var results = _vacanciesApplicationRepository.CheckApplicationExist(userId, vacancyId);
			if (results != null && results.Count > 0)
			{
				return true;
			}
			return false;

			}
			catch (Exception Ex)
			{
				_logger.LogInformation(Ex.Message);

				throw;
			}
		}

		private bool CheckApplicationMaxTime(string userId)
		{
			try
			{
			var results = _vacanciesApplicationRepository.CheckApplicationMaxTime(userId);
			if (results != null && results.Count > 0)
			{
				return true;
			}
			return false;

			}
			catch (Exception Ex)
			{
				_logger.LogInformation(Ex.Message);

				throw;
			}
		}

		#endregion

	}
}
