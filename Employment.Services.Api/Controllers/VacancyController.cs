using AutoMapper;
using Employment.Application.DTOs;
using Employment.Application.IServices;
using Employment.Application.Services;
using Employment.Application.ViewModels;
using Employment.Domain.Entities;
using Employment.Services.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Employment.Services.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class VacancyController : ApiController
    {
        private readonly IVacancyService _vacancyService;
        public VacancyController(IVacancyService vacancyService ,IMapper mapper, ILogger<VacancyController> logger)
            : base(mapper, logger)
        {
            _vacancyService = vacancyService;
        }

        [AllowAnonymous]
        [HttpGet("GetAllVacancies")]
        public async Task<IEnumerable<List<VacancyModel>>> GetAllVacancies()
        {
            _logger.LogInformation("Begin GetAllVacancies API");

            var result =  await _vacancyService.GetAllVacancies();
            return _mapper.Map<IEnumerable<List<VacancyModel>>>(result);
        }

        [AllowAnonymous]
        [HttpGet("AddVacancy")]
        public async Task<IEnumerable<bool>> AddVacancy(VacancyModel vacancyModel)
        {
            _logger.LogInformation("Begin AddVacancy API");

            return await _vacancyService.AddVacancy(_mapper.Map<VacancyDTO>(vacancyModel));
            
        }

        [AllowAnonymous]
        [HttpGet("EditVacancy")]
        public async Task<IEnumerable<bool>> EditVacancy(VacancyModel vacancyModel)
        {
            _logger.LogInformation("Begin EditVacancy API");

            return await _vacancyService.EditVacancy(_mapper.Map<VacancyDTO>(vacancyModel));
        }

        [AllowAnonymous]
        [HttpGet("DeleteVacancy")]
        public async Task<IEnumerable<bool>> DeleteVacancy(int vacancyId)
        {
            _logger.LogInformation("Begin DeleteVacancy API");

            return await _vacancyService.DeleteVacancy(vacancyId);
        }

        [AllowAnonymous]
        [HttpGet("PostVacancy")]
        public async Task<IEnumerable<bool>> PostVacancy(int vacancyId)
        {
            _logger.LogInformation("Begin PostVacancy API");

            return await _vacancyService.PostVacancy(vacancyId);
        }

        [AllowAnonymous]
        [HttpGet("DeactivateVacancy")]
        public async Task<IEnumerable<bool>> DeactivateVacancy(int vacancyId)
        {
            _logger.LogInformation("Begin DeactivateVacancy API");

            return await _vacancyService.DeactivateVacancy(vacancyId);
        }

        [AllowAnonymous]
        [HttpGet("ApplytoVacancy")]
        public async Task<IEnumerable<bool>> ApplytoVacancy(int userId , int vacancyId)
        {
            _logger.LogInformation("Begin ApplytoVacancy API");

            return await _vacancyService.ApplytoVacancy(userId, vacancyId);
        }
        [AllowAnonymous]
        [HttpGet("GetAllVacancyApplicants")]
        public async Task<IEnumerable<List<UserModel>>> GetAllVacancyApplicants(int vacancyId)
        {
            _logger.LogInformation("Begin GetAllVacancyApplicants API");

            var result =  await _vacancyService.GetAllVacancyApplicants( vacancyId);
            return _mapper.Map<IEnumerable<List<UserModel>>>(result);

        }
    }
}

