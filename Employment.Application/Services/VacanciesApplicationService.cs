using AutoMapper;
using Azure;
using Employment.Application.DTOs;
using Employment.Application.IServices;
using Employment.Application.ViewModels;
using Employment.Domain.Common;
using Employment.Domain.Entities;
using Employment.Domain.IRepository;
using Employment.Infra.Data.Repository;
using Microsoft.Extensions.Logging;
using Serilog;
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
            IVacancyService vacancyService, IMapper mapper, ILogger<VacanciesApplicationService> logger) : base(mapper, logger)
        {
            _vacanciesApplicationRepository = vacanciesApplicationRepository;
            _vacancyService = vacancyService;
        }

        public async Task<ResponseModel<bool>> ApplytoVacancy(string userId, int vacancyId)
        {
            ResponseModel<bool> response = new ResponseModel<bool>();
            try
            {
                if ((!string.IsNullOrEmpty(userId)) && vacancyId != 0)
                {
                    response = ValidateApplication(userId, vacancyId);

                    if (response.Result)
                    {
                        VacanciesApplicationDTO dto = new VacanciesApplicationDTO
                        {
                            ApplicationDate = DateTime.Now,
                            FkApplicantId = userId,
                            FkVacancyId = vacancyId

                        };

                        var result = await _vacanciesApplicationRepository.ApplytoVacancy(_mapper.Map<VacanciesApplication>(dto));
                        if (result)
                        {
                            response.Result = true;
                            response.MessageCodes = Enums.MessageCodes.Success;
                        }
                        else
                            response.MessageCodes = Enums.MessageCodes.InternalServerError;

                    }
                   
                }
                else
                { 
                    response.Result = false;
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

        public async Task<ResponseModel<List<string>>>  GetAllVacancyApplicants(int vacancyId)
        {
            ResponseModel<List<string>> response = new ResponseModel<List<string>>();

            try
            {

                if (vacancyId != 0)
                {
                    var result = await _vacanciesApplicationRepository.GetAllVacancyApplicants(vacancyId);
                    if (result != null && result.Count > 0)
                    {
                        response.Result = result;
                        response.MessageCodes = Enums.MessageCodes.Success;
                    }
                    else
                        response.MessageCodes = Enums.MessageCodes.NoDataFound;
                }
                else
                {
                    response.MessageCodes = Enums.MessageCodes.BadRequest;
                }
            }
            catch (Exception Ex)
            {
                _logger.LogError(Ex.Message,Ex);
                response.MessageCodes = Enums.MessageCodes.InternalServerError;

            }
            return response;

        }


        #region ValidationFunctions
        private ResponseModel<bool> ValidateApplication(string userId, int vacancyId)
        {
            ResponseModel<bool> response = new ResponseModel<bool>() { Result = true};
            try
            {

                var applicationExistResult = CheckApplicationExist(userId, vacancyId);
                var checkApplicationMaxTime = CheckApplicationMaxTime(userId);
                var checkApplicationMaxNumber = _vacancyService.CheckApplicationMaxNumber(vacancyId);

                if (CheckApplicationExist(userId, vacancyId).Result)
                {
                    response.Result = applicationExistResult.Result;
                    response.MessageCodes = applicationExistResult.MessageCodes;
                    response.Message = applicationExistResult.Message;
                    return response;
                }
                else if (CheckApplicationMaxTime(userId).Result)
                {
                    response.Result = applicationExistResult.Result;
                    response.MessageCodes = applicationExistResult.MessageCodes;
                    response.Message = applicationExistResult.Message;
                    return response;
                }
                else if (!_vacancyService.CheckApplicationMaxNumber(vacancyId).Result)
                {
                    response.Result = applicationExistResult.Result;
                    response.MessageCodes = applicationExistResult.MessageCodes;
                    response.Message = applicationExistResult.Message;
                    return response;
                }

                response.MessageCodes = Enums.MessageCodes.Success;

            }
            catch (Exception Ex)
            {
                _logger.LogError(Ex.Message);
                response.MessageCodes = Enums.MessageCodes.InternalServerError;
            }

            return response;
        }

        private ResponseModel<bool> CheckApplicationExist(string userId, int vacancyId)
        {
            ResponseModel<bool> response = new ResponseModel<bool>() { Result = false};
            try
            {
                var results = _vacanciesApplicationRepository.CheckApplicationExist(userId, vacancyId);
                if (results != null && results.Count > 0)
                {
                    response.Result = true;
                    response.MessageCodes = Enums.MessageCodes.DataFound;
                    response.Message = "Vacancy Application Already Exist";
                }
                else
                {
                    response.MessageCodes = Enums.MessageCodes.Success;
                    response.Message = "Vacancy Application Not Exist";

                }
            }
            catch (Exception Ex)
            {
                _logger.LogError(Ex.Message);
                response.MessageCodes = Enums.MessageCodes.InternalServerError;
            }
            return response;
        }

        private ResponseModel<bool> CheckApplicationMaxTime(string userId)
        {
            ResponseModel<bool> response = new ResponseModel<bool>() { Result = false };

            try
            {
                var results = _vacanciesApplicationRepository.CheckApplicationMaxTime(userId);
                if (results != null && results.Count > 0)
                {
                    response.Result = true;
                    response.MessageCodes = Enums.MessageCodes.ApplicationExistTodayWithSameApplicant;
                    response.Message = "Another Application Already Exist Today For Same Applicant";
                }
                else
                {
                    response.MessageCodes = Enums.MessageCodes.Success;
                    response.Message = "Application Not Exist Today For Same Applicant";
                }

            }
            catch (Exception Ex)
            {
                _logger.LogError(Ex.Message);
                response.MessageCodes = Enums.MessageCodes.InternalServerError;
            }
            return response;

        }

        #endregion

    }
}
