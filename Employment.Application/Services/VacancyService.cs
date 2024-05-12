using AutoMapper;
using Azure;
using Employment.Application.DTOs;
using Employment.Application.IServices;
using Employment.Application.ViewModels;
using Employment.Domain.Common;
using Employment.Domain.Entities;
using Employment.Domain.IRepository;
using Employment.Infra.Data;
using Employment.Infra.Data.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Serilog;
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

        public VacancyService(IVacancyRepository vacancyRepository, IMapper mapper, ILogger<VacancyService> logger) : base(mapper, logger)
        {
            _vacancyRepository = vacancyRepository;
        }

        public async Task<ResponseModel<List<VacancyDTO>>> GetAllVacancies()
        {
            ResponseModel<List<VacancyDTO>> response = new ResponseModel<List<VacancyDTO>>();

            try
            {
                var results = await _vacancyRepository.GetAllVacancies();

                if (results != null && results.Count > 0)
                {
                    response.Result = _mapper.Map<List<VacancyDTO>>(results);
                    response.MessageCodes = Enums.MessageCodes.Success;
                }
                else
                    response.MessageCodes = Enums.MessageCodes.NoDataFound;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                response.MessageCodes = Enums.MessageCodes.InternalServerError;
            }

            return response;

        }
        public async Task<ResponseModel<bool>> AddVacancy(VacancyDTO vacancyDTO)
        {
            ResponseModel<bool> response = new ResponseModel<bool>() { Result = false };

            try
            {
                var result =  await _vacancyRepository.AddVacancy(_mapper.Map<Vacancy>(vacancyDTO));

                if (result)
                {
                    response.Result = true;
                    response.MessageCodes = Enums.MessageCodes.Success;
                }
                else
                {
                    response.Result = false;
                    response.MessageCodes = Enums.MessageCodes.InternalServerError;
                }
                
            }
            catch (Exception Ex)
            {
                _logger.LogError(Ex.Message);
                response.MessageCodes = Enums.MessageCodes.InternalServerError;

            }
            return response;
        }

        public async Task<ResponseModel<bool>> EditVacancy(VacancyDTO vacancyDTO)
        {
            ResponseModel<bool> response = new ResponseModel<bool>() { Result = false};
            
            try
            {
                if (vacancyDTO.Id != 0)
                {
                    var result = await _vacancyRepository.EditVacancy(_mapper.Map<Vacancy>(vacancyDTO));
                    if (result)
                    {
                        response.Result = true;
                        response.MessageCodes = Enums.MessageCodes.Success;
                    }
                }
                else
                {
                    response.MessageCodes = Enums.MessageCodes.BadRequest;
                }
            }
            catch (Exception Ex)
            {
                _logger.LogError(Ex.Message);
                response.MessageCodes = Enums.MessageCodes.InternalServerError;

            }
            return response;
        }

        public async Task<ResponseModel<bool>> DeleteVacancy(int vacancyId)
        {
            ResponseModel<bool> response = new ResponseModel<bool>() { Result = false };

            try
            {
                if (vacancyId != 0)
                {
                    var result =  await _vacancyRepository.DeleteVacancy(vacancyId);
                    if (result)
                    {
                        response.Result = true;
                        response.MessageCodes = Enums.MessageCodes.Success;
                    }
                    else
                        response.MessageCodes = Enums.MessageCodes.InternalServerError;
                }
                else
                    response.MessageCodes = Enums.MessageCodes.BadRequest;
            }
            catch (Exception Ex)
            {
                _logger.LogError(Ex.Message);
                response.MessageCodes = Enums.MessageCodes.InternalServerError;

            }
            return response;
        }

        public async Task<ResponseModel<bool>> DeactivateVacancy(int vacancyId)
        {
			ResponseModel<bool> response = new ResponseModel<bool>() { Result = false };

			try
			{
                if (vacancyId != 0)
                {
                    var result = await _vacancyRepository.DeactivateVacancy(vacancyId);
					if (result)
					{
						response.Result = true;
						response.MessageCodes = Enums.MessageCodes.Success;
					}
					else
					{
						response.MessageCodes = Enums.MessageCodes.BadRequest;
					}
				}

            }
            catch (Exception Ex)
            {
                _logger.LogError(Ex.Message);
				response.MessageCodes = Enums.MessageCodes.InternalServerError;
			}
			return response;

		}

		public async Task<ResponseModel<bool>> PostVacancy(int vacancyId)
        {
			ResponseModel<bool> response = new ResponseModel<bool>() { Result = false };

			try
			{
                if (vacancyId != 0)
                {
                    var result =  await _vacancyRepository.PostVacancy(vacancyId);
					if (result)
					{
						response.Result = true;
						response.MessageCodes = Enums.MessageCodes.Success;
					}
					else
					{
						response.MessageCodes = Enums.MessageCodes.BadRequest;
					}

				}

            }
            catch (Exception Ex)
            {
                _logger.LogError(Ex.Message);

				response.MessageCodes = Enums.MessageCodes.InternalServerError;
			}
			return response;

		}


        public ResponseModel<bool> CheckApplicationMaxNumber(int vacancyId)
        {
            ResponseModel<bool> response = new ResponseModel<bool>() { Result = false };

            try
            {
                var vacancy = _vacancyRepository.GetVacancyById(vacancyId);
                if (vacancy != null)
                {
                    if (vacancy.CurrentNumberOfApplication >= vacancy.MaxNumberOfApplications)
                    {
                        response.Result = true;
                        response.MessageCodes = Enums.MessageCodes.InvalidMaxNumberOfApplication;
                        response.Message = "This Vacancy Has Reached The Maximum Number Of Applications";
                    }
                }
                else
                    response.MessageCodes = Enums.MessageCodes.Success;
            }
            catch (Exception Ex)
            {
                _logger.LogError(Ex.Message);
                response.MessageCodes = Enums.MessageCodes.InternalServerError;
            }
            return response;


        }

        public void Dispose()
        {
        }


    }
}
