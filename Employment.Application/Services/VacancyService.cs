using AutoMapper;
using Employment.Application.DTOs;
using Employment.Application.IServices;
using Employment.Application.ViewModels;
using Employment.Domain.Entities;
using Employment.Domain.IRepository;
using Employment.Infra.Data;
using Employment.Infra.Data.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employment.Application.Services
{


	public class VacancyService : GenericService, IVacancyService
	{
		protected readonly IVacancyRepository _vacancyRepository;

		public VacancyService(IVacancyRepository vacancyRepository, IMapper mapper,ILogger logger) : base(mapper, logger)
		{
			_vacancyRepository = vacancyRepository;
		}

		public async Task<IEnumerable<List<VacancyDTO>>> GetAllVacancies()
		{
			try
			{

			var results = await _vacancyRepository.GetAllVacancies();

			return _mapper.Map<IEnumerable<List<VacancyDTO>>>(results);
			}
			catch (Exception Ex)
			{
				_logger.LogInformation(Ex.Message);

				throw;
			}

		}
		public async Task<bool> AddVacancy(VacancyDTO vacancyDTO)
		{
			try
			{
				return await _vacancyRepository.AddVacancy(_mapper.Map<Vacancy>(vacancyDTO));

			}
			catch (Exception Ex)
			{
				_logger.LogInformation(Ex.Message);

				throw;
			}
		}

		public async Task<bool> EditVacancy(VacancyDTO vacancyDTO)
		{
			try
			{

				if (vacancyDTO != null)
				{
					return await _vacancyRepository.EditVacancy(_mapper.Map<Vacancy>(vacancyDTO));
				}
				return false;

			}
			catch (Exception Ex)
			{
				_logger.LogInformation(Ex.Message);

				throw;
			}
		}

		public async Task<bool> DeleteVacancy(int vacancyId)
		{
			try
			{

				if (vacancyId != 0)
				{
					return await _vacancyRepository.DeleteVacancy(vacancyId);
				}
				return false;
			}
			catch (Exception Ex)
			{
				_logger.LogInformation(Ex.Message);

				throw;
			}
		}

		public async Task<bool> DeactivateVacancy(int vacancyId)
		{
			try
			{
				if (vacancyId != 0)
				{
					return await _vacancyRepository.DeactivateVacancy(vacancyId);
				}
				return false;

			}
			catch (Exception Ex)
			{
				_logger.LogInformation(Ex.Message);

				throw;
			}
		}



		public async Task<bool> PostVacancy(int vacancyId)
		{
			try
			{
				if (vacancyId != 0)
				{
					return await _vacancyRepository.PostVacancy(vacancyId);
				}
				return false;

			}
			catch (Exception Ex)
			{
				_logger.LogInformation(Ex.Message);

				throw;
			}
		}


		public bool CheckApplicationMaxNumber(int vacancyId)
		{
			try
			{
				var vacancy = _vacancyRepository.GetVacancyById(vacancyId);
				if (vacancy != null)
				{
					if (vacancy.CurrentNumberOfApplication >= vacancy.MaxNumberOfApplications)
					{
						return false;
					}
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

		public void Dispose()
		{
		}


	}
}
