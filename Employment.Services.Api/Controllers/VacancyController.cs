using AutoMapper;
using Employment.Application.DTOs;
using Employment.Application.IServices;
using Employment.Application.Services;
using Employment.Application.ViewModels;
using Employment.Domain.Entities;
using Employment.Services.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Net;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Employment.Domain.Common;
using static MassTransit.ValidationResultExtensions;

namespace Employment.Services.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class VacancyController : ApiController
    {
        private readonly IVacancyService _vacancyService;
        private readonly IVacanciesApplicationService _vacanciesApplicationService;

        public VacancyController(IVacancyService vacancyService, IVacanciesApplicationService vacanciesApplicationService
            , IMapper mapper, ILogger<VacancyController> logger)
            : base(mapper, logger)
        {
            _vacancyService = vacancyService;
            _vacanciesApplicationService = vacanciesApplicationService;

        }


        [Authorize(Roles = "Employer")]
        [HttpGet("GetAllVacancies")]
        public async Task<IActionResult> GetAllVacancies()
        {
            try
            {
                _logger.LogInformation("Begin GetAllVacancies API");

                var result = await _vacancyService.GetAllVacancies();

                return StatusCode(Convert.ToInt16(result.MessageCodes), result);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [Authorize(Roles = "Employer")]
        [HttpPost("AddVacancy")]
        public async Task<IActionResult> AddVacancy(VacancyModel vacancyModel)
        {
            try
            {
                _logger.LogInformation("Begin AddVacancy API");

                if (ModelState.IsValid)
                {
                    _logger.LogInformation("ModelState Is Valid");

                    var result = await _vacancyService.AddVacancy(_mapper.Map<VacancyDTO>(vacancyModel));

                    return StatusCode(Convert.ToInt16(result.MessageCodes), result);

                }
                else
                {
                    _logger.LogInformation("End AddVacancy API");
                    return StatusCode(StatusCodes.Status400BadRequest);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

        [Authorize(Roles = "Employer")]
        [HttpPut("EditVacancy")]
        public async Task<IActionResult> EditVacancy(VacancyModel vacancyModel)
        {
            try
            {
                _logger.LogInformation("Begin EditVacancy API");

                if (ModelState.IsValid)
                {
                    _logger.LogInformation("ModelState Is Valid");

                    var result = await _vacancyService.EditVacancy(_mapper.Map<VacancyDTO>(vacancyModel));
                    return StatusCode(Convert.ToInt16(result.MessageCodes), result);

                }
                else
                {
                    _logger.LogInformation("End EditVacancy API");
                    return StatusCode(StatusCodes.Status400BadRequest);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);

            }

        }

        [Authorize(Roles = "Employer")]
        [HttpDelete("DeleteVacancy")]
        public async Task<IActionResult> DeleteVacancy(int vacancyId)
        {
            try
            {
                _logger.LogInformation("Begin DeleteVacancy API");

                if (ModelState.IsValid)
                {
                    var result = await _vacancyService.DeleteVacancy(vacancyId);

                    return StatusCode(Convert.ToInt16(result.MessageCodes), result);
                }
                else
                {
                    _logger.LogInformation("End DeleteVacancy API");
                    return StatusCode(StatusCodes.Status400BadRequest);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

        [Authorize(Roles = "Employer")]
        [HttpPost("PostVacancy")]
        public async Task<IActionResult> PostVacancy(int vacancyId)
        {

            try
            {
                _logger.LogInformation("Begin PostVacancy API");

                if (await _vacancyService.PostVacancy(vacancyId))
                {
                    _logger.LogInformation("End PostVacancy API");
                    return StatusCode(StatusCodes.Status200OK, true);
                }
                else
                {
                    _logger.LogInformation("End PostVacancy API");
                    return StatusCode(StatusCodes.Status400BadRequest);
                }



            }
            catch (Exception ex)
            {

                _logger.LogError(ex.Message, ex);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);

            }


        }

        [Authorize(Roles = "Employer")]
        [HttpPost("DeactivateVacancy")]
        public async Task<IActionResult> DeactivateVacancy(int vacancyId)
        {
            try
            {
                _logger.LogInformation("Begin DeactivateVacancy API");


                if (await _vacancyService.DeactivateVacancy(vacancyId))
                {
                    _logger.LogInformation("End DeactivateVacancy API");
                    return StatusCode(StatusCodes.Status200OK, true);
                }
                else
                {
                    _logger.LogInformation("End DeactivateVacancy API");
                    return StatusCode(StatusCodes.Status400BadRequest);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

        [Authorize(Roles = "Employer")]
        [HttpGet("GetAllVacancyApplicants")]
        public async Task<IActionResult> GetAllVacancyApplicants(int vacancyId)
        {

            try
            {
                _logger.LogInformation("Begin GetAllVacancyApplicants API");


                var result = await _vacanciesApplicationService.GetAllVacancyApplicants(vacancyId);
                if (result != null && result.Result.Count() > 0)
                {
                    _logger.LogInformation("End GetAllVacancyApplicants API");
                    return StatusCode(StatusCodes.Status200OK, result);
                }
                else
                {
                    _logger.LogInformation("End GetAllVacancyApplicants API");
                    return StatusCode(StatusCodes.Status200OK, result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);

            }

        }

        [Authorize(Roles = "Applicant")]
        [HttpPost("ApplytoVacancy")]
        public async Task<IActionResult> ApplytoVacancy(int vacancyId)
        {
            try
            {
                _logger.LogInformation("Begin ApplytoVacancy API");

                if (ModelState.IsValid)
                {
                    _logger.LogInformation("ModelState Is Valid");

                    string? userID = GetTokenData();
                    if (string.IsNullOrEmpty(userID))
                    {
                        return Unauthorized();
                    }
                    var result = await _vacanciesApplicationService.ApplytoVacancy(userID, vacancyId);
                    if (result.Result)
                    {
                        return StatusCode(StatusCodes.Status200OK, true);
                    }
                    else
                    {
                        //return StatusCode(StatusCodes.Status400BadRequest);
                        AddError("Request is not valid");
                        return CustomResponse();
                    }
                }
                _logger.LogInformation("End ApplytoVacancy API");
                return StatusCode(StatusCodes.Status400BadRequest);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);

            }


        }


    }
}

