using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Employment.Domain.Entities;
using Employment.Services.Api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NetDevPack.Identity.Jwt;
using NetDevPack.Identity.Model;
using StackExchange.Redis;
using static StackExchange.Redis.Role;

namespace Employment.Services.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ApiController
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly AppJwtSettings _appJwtSettings;
        protected readonly IMapper mapper;
        protected readonly ILogger<UserController> logger;
        public AccountController(
            SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager,
            IOptions<AppJwtSettings> appJwtSettings,
            IMapper mapper, ILogger<UserController> logger
            ) : base(mapper, logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _appJwtSettings = appJwtSettings.Value;

		}

		[HttpPost]
        [Route("register")]
        public async Task<ActionResult> Register(UserModel registerUser)
        {
			_logger.LogInformation("Begin register API");

			if (!ModelState.IsValid) return CustomResponse(ModelState);

			_logger.LogInformation("ModelState is Valid");

			var user = new IdentityUser
            {
                UserName = registerUser.Email,
                Email = registerUser.Email,
                EmailConfirmed = true,

            };

            var result = await _userManager.CreateAsync(user, registerUser.Password);
            var RoleInsertionresult = await _userManager.AddToRoleAsync(user, registerUser.RoleName);

            _logger.LogInformation("After CreateAsync");

			if (result.Succeeded && RoleInsertionresult.Succeeded)
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
            //#region Token Generation
            //// create claims for the user
            //var claims = new List<Claim>
            // {
            //  new Claim(ClaimTypes.Name, user.UserName)
            // };

            //var roles = await _userManager.GetRolesAsync(user);
            //foreach (var role in roles)
            //{
            //    claims.Add(new Claim(ClaimTypes.Role, role));
            //}

            //// create a signing key
            //var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("PreNOQh4e_qnxdNU_dqdHP1p"));

            //// create a signing credential
            //var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            //// create a token
            //var token = new JwtSecurityToken(
            //    issuer: "Organization.com",
            //    audience: "Organization.com",
            //    claims: claims,
            //    expires: DateTime.Now.AddMinutes(30),
            //    signingCredentials: creds
            //);
            //return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
