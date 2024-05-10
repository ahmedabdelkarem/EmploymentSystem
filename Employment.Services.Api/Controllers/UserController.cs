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

        public UserController(IUserService userService, IMapper mapper) : base(mapper)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpGet("GetAll")]
        public async Task<IEnumerable<UserDto>> Get()
        {
            return await _userService.GetAll();
        }

        [AllowAnonymous]
        [HttpGet("GetUserById")]
        public async Task<UserDto> Get(Guid id)
        {
            return await _userService.GetById(id);
        }

        [CustomAuthorize("Customers", "Write")]
        [HttpPost("RegisterUser")]
        public async Task<IActionResult> Post([FromBody]UserDto customerViewModel)
        {
            return !ModelState.IsValid ? CustomResponse(ModelState) : CustomResponse(await _userService.Register(customerViewModel));
        }

        [CustomAuthorize("Customers", "Write")]
        [HttpPut("UpdateUser")]
        public async Task<IActionResult> Put([FromBody]UserDto customerViewModel)
        {
            return !ModelState.IsValid ? CustomResponse(ModelState) : CustomResponse(await _userService.Update(customerViewModel));
        }

        [CustomAuthorize("Customers", "Remove")]
        [HttpDelete("RemoveUser")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return CustomResponse(await _userService.Remove(id));
        }

    }
}
