using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Employment.Application.IServices;
using Employment.Application.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetDevPack.Identity.Authorization;

namespace Employment.Services.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ApiController
    {
        private readonly IUserService _userService;
		private readonly ILogger<UserController> _logger;

		public UserController(IUserService userService, ILogger<UserController> logger)
        {
            _userService = userService;
			_logger = logger;

		}

        [AllowAnonymous]
        [HttpGet("GetAll")]
        public async Task<IEnumerable<UserDto>> Get()
        {
            _logger.LogInformation("Begin GetAll users API");

            return await _userService.GetAll();

		}

		[AllowAnonymous]
        [HttpGet("GetUserById")]
        public async Task<UserDto> Get(Guid id)
        {
			_logger.LogInformation("Begin GetUserById API");

			return await _userService.GetById(id);
        }

        [CustomAuthorize("Customers", "Write")]
        [HttpPost("RegisterUser")]
        public async Task<IActionResult> Post([FromBody]UserDto customerViewModel)
        {
			_logger.LogInformation("Begin RegisterUser API");

			return !ModelState.IsValid ? CustomResponse(ModelState) : CustomResponse(await _userService.Register(customerViewModel));
        }

        [CustomAuthorize("Customers", "Write")]
        [HttpPut("UpdateUser")]
        public async Task<IActionResult> Put([FromBody]UserDto customerViewModel)
        {
			_logger.LogInformation("Begin UpdateUser API");

			return !ModelState.IsValid ? CustomResponse(ModelState) : CustomResponse(await _userService.Update(customerViewModel));
        }

        [CustomAuthorize("Customers", "Remove")]
        [HttpDelete("RemoveUser")]
        public async Task<IActionResult> Delete(Guid id)
        {
			_logger.LogInformation("Begin RemoveUser API");

			return CustomResponse(await _userService.Remove(id));
        }

    }
}
