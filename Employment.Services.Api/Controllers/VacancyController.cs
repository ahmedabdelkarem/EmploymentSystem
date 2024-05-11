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


		[AllowAnonymous]
		[HttpGet("GetAllVacancies")]
		public async Task<IActionResult> GetAllVacancies()
		{
			try
			{
				_logger.LogInformation("Begin GetAllVacancies API");

				var result = _mapper.Map<List<VacancyModel>>(await _vacancyService.GetAllVacancies());

				if (result != null && result.Count > 0)
				{
					_logger.LogInformation("End GetAllVacancies API");
					return StatusCode(StatusCodes.Status200OK, result);
				}
				else
				{
					_logger.LogInformation("End GetAllVacancies API");
					return StatusCode(StatusCodes.Status500InternalServerError);
				}
			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
			}
		}

		[AllowAnonymous]
		[HttpPost("AddVacancy")]
		public async Task<IActionResult> AddVacancy(VacancyModel vacancyModel)
		{
			try
			{
				_logger.LogInformation("Begin AddVacancy API");

				if (ModelState.IsValid)
				{
					_logger.LogInformation("ModelState Is Valid");

					if (await _vacancyService.AddVacancy(_mapper.Map<VacancyDTO>(vacancyModel)))
					{
						return StatusCode(StatusCodes.Status200OK, true);
					}
					else
					{
						return StatusCode(StatusCodes.Status500InternalServerError);
					}

				}
				_logger.LogInformation("End AddVacancy API");
				return StatusCode(StatusCodes.Status400BadRequest);
			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
			}

		}

		[AllowAnonymous]
		[HttpPut("EditVacancy")]
		public async Task<IActionResult> EditVacancy(VacancyModel vacancyModel)
		{
			try
			{
				_logger.LogInformation("Begin EditVacancy API");

				if (ModelState.IsValid)
				{
					_logger.LogInformation("ModelState Is Valid");

					if (await _vacancyService.EditVacancy(_mapper.Map<VacancyDTO>(vacancyModel)))
					{
						return StatusCode(StatusCodes.Status200OK, true);
					}
					else
					{
						return StatusCode(StatusCodes.Status500InternalServerError);
					}
				}
				_logger.LogInformation("End EditVacancy API");
				return StatusCode(StatusCodes.Status400BadRequest);

			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status400BadRequest, ex.Message);

			}

		}

		[AllowAnonymous]
		[HttpDelete("DeleteVacancy")]
		public async Task<IActionResult> DeleteVacancy(int vacancyId)
		{
			try
			{
				_logger.LogInformation("Begin DeleteVacancy API");
				if (ModelState.IsValid)
				{
					if (await _vacancyService.DeleteVacancy(vacancyId))
					{
						return StatusCode(StatusCodes.Status200OK, true);
					}
					else
					{
						return StatusCode(StatusCodes.Status500InternalServerError);
					}
				}
				_logger.LogInformation("End DeleteVacancy API");
				return StatusCode(StatusCodes.Status400BadRequest);
			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
			}

		}

		//[AllowAnonymous]
		[Authorize(Roles = "Employer")]
		[HttpPost("PostVacancy")]
		public async Task<IActionResult> PostVacancy(int vacancyId)
		{

			try
			{
				_logger.LogInformation("Begin PostVacancy API");

				if (ModelState.IsValid)
				{
					_logger.LogInformation("ModelState Is Valid");

					if (await _vacancyService.PostVacancy(vacancyId))
					{
						return StatusCode(StatusCodes.Status200OK, true);
					}
					else
					{
						return StatusCode(StatusCodes.Status500InternalServerError);
					}
				}
				_logger.LogInformation("End PostVacancy API");
				return StatusCode(StatusCodes.Status400BadRequest);

			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status400BadRequest, ex.Message);

			}


		}

		[AllowAnonymous]
		[HttpPost("DeactivateVacancy")]
		public async Task<IActionResult> DeactivateVacancy(int vacancyId)
		{
			try
			{
				_logger.LogInformation("Begin DeactivateVacancy API");

				if (ModelState.IsValid)
				{
					_logger.LogInformation("ModelState Is Valid");

					if (await _vacancyService.DeactivateVacancy(vacancyId))
					{
						return StatusCode(StatusCodes.Status200OK, true);
					}
					else
					{
						return StatusCode(StatusCodes.Status500InternalServerError);
					}
				}
				_logger.LogInformation("End DeactivateVacancy API");
				return StatusCode(StatusCodes.Status400BadRequest);

			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status400BadRequest, ex.Message);

			}


		}

		[AllowAnonymous]
		[HttpPost("ApplytoVacancy")]
		public async Task<IActionResult> ApplytoVacancy(string userId, int vacancyId)
		{
			try
			{
				_logger.LogInformation("Begin ApplytoVacancy API");

				if (ModelState.IsValid)
				{
					_logger.LogInformation("ModelState Is Valid");

					if (await _vacanciesApplicationService.ApplytoVacancy(userId, vacancyId))
					{
						return StatusCode(StatusCodes.Status200OK, true);
					}
					else
					{
						return StatusCode(StatusCodes.Status500InternalServerError);
					}
				}
				_logger.LogInformation("End ApplytoVacancy API");
				return StatusCode(StatusCodes.Status400BadRequest);

			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status400BadRequest, ex.Message);

			}


		}

		[AllowAnonymous]
		[HttpGet("GetAllVacancyApplicants")]
		public async Task<IActionResult> GetAllVacancyApplicants(int vacancyId)
		{

			try
			{
				_logger.LogInformation("Begin GetAllVacancyApplicants API");

				if (ModelState.IsValid)
				{
					_logger.LogInformation("ModelState Is Valid");

					var result = await _vacanciesApplicationService.GetAllVacancyApplicants(vacancyId);
					if (result != null && result.Count > 0)
					{
						return StatusCode(StatusCodes.Status200OK, result);
					}
					else
					{
						return StatusCode(StatusCodes.Status500InternalServerError);
					}
				}
				_logger.LogInformation("End GetAllVacancyApplicants API");
				return StatusCode(StatusCodes.Status400BadRequest);

			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status400BadRequest, ex.Message);

			}


		}
	}
}

