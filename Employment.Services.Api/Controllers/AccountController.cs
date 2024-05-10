using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using NetDevPack.Identity.Jwt;
using NetDevPack.Identity.Model;

namespace Employment.Services.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ApiController
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly AppJwtSettings _appJwtSettings;

		public AccountController(
            SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager,
            IOptions<AppJwtSettings> appJwtSettings
			)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _appJwtSettings = appJwtSettings.Value;
		}

		[HttpPost]
        [Route("register")]
        public async Task<ActionResult> Register(RegisterUser registerUser)
        {
			_logger.LogInformation("Begin register API");

			if (!ModelState.IsValid) return CustomResponse(ModelState);

			_logger.LogInformation("ModelState is Valid");

			var user = new IdentityUser
            {
                UserName = registerUser.Email,
                Email = registerUser.Email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, registerUser.Password);
			_logger.LogInformation("After CreateAsync");

			if (result.Succeeded)
            {
				_logger.LogInformation("result Succeeded");

				return CustomResponse(GetFullJwt(user.Email));
            }
			_logger.LogInformation("Error count = "+ result.Errors.Count() );

			foreach (var error in result.Errors)
            {
                AddError(error.Description);
            }

            return CustomResponse();
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginUser loginUser)
        {
			_logger.LogInformation("Begin login API");

			if (!ModelState.IsValid) return CustomResponse(ModelState);
			_logger.LogInformation("ModelState is Valid");

			var result = await _signInManager.PasswordSignInAsync(loginUser.Email, loginUser.Password, false, true);
			_logger.LogInformation("After PasswordSignInAsync");

			if (result.Succeeded)
            {
				_logger.LogInformation("result Succeeded");

				var fullJwt = GetFullJwt(loginUser.Email);
				_logger.LogInformation("After GetFullJwt");

				return CustomResponse(fullJwt);
            }

            if (result.IsLockedOut)
            {
				_logger.LogInformation("result Is LockedOut");

				AddError("This user is temporarily blocked");
                return CustomResponse();
            }

            AddError("Incorrect user or password");
            return CustomResponse();
        }

        private string GetFullJwt(string email)
        {
            return new JwtBuilder()
                .WithUserManager(_userManager)
                .WithJwtSettings(_appJwtSettings)
                .WithEmail(email)
                .WithJwtClaims()
                .WithUserClaims()
                .WithUserRoles()
                .BuildToken();
        }
    }
}
