using System.IdentityModel.Tokens.Jwt;
using System.Numerics;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using AutoMapper;
using Employment.Domain.Entities;
using Employment.Services.Api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NetDevPack.Identity.Jwt;
using NetDevPack.Identity.Model;
using StackExchange.Redis;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static Employment.Domain.Common.Enums;
using static MassTransit.ValidationResultExtensions;
using static StackExchange.Redis.Role;

namespace Employment.Services.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AccountController : ApiController
	{
		private readonly SignInManager<AspNetUser> _signInManager;
		private readonly UserManager<AspNetUser> _userManager;
		private readonly AppJwtSettings _appJwtSettings;
		protected readonly IMapper mapper;
		protected readonly ILogger<AccountController> logger;
		public AccountController(
			SignInManager<AspNetUser> signInManager,
			UserManager<AspNetUser> userManager,
			IOptions<AppJwtSettings> appJwtSettings,
			IMapper mapper, ILogger<AccountController> logger
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

			var user = await _userManager.Users.SingleOrDefaultAsync(u => u.PhoneNumber == registerUser.MobileNumber);
			if(user != null)
				return StatusCode(Convert.ToInt16(MessageCodes.UserAlreadyExists)); 


			user = new AspNetUser
			{
				UserName = registerUser.Name,
				Email = registerUser.Email,
				EmailConfirmed = false,
				PhoneNumber = registerUser.MobileNumber,
				PhoneNumberConfirmed = false,
				ICNumber = registerUser.ICNumber
			};

			var result = await _userManager.CreateAsync(user);
			_logger.LogInformation("After CreateAsync");

			_logger.LogInformation("Error count = " + result.Errors.Count());

			foreach (var error in result.Errors)
			{
				AddError(error.Description);
			}
			return StatusCode(Convert.ToInt16(MessageCodes.Success), user);

		}


		[Route("VerifyMobile")]
		public async Task<ActionResult> VerifyMobile(int ICNumber)
		{
			var user = await _userManager.Users.SingleOrDefaultAsync(u => u.ICNumber == ICNumber);
			if (user == null)
			{
				return StatusCode(StatusCodes.Status401Unauthorized);
			}

			//TODO: Send SMS code to user.PhoneNumber
			return StatusCode(Convert.ToInt16(MessageCodes.Success), true);
		}

		[HttpPost]
		[Route("VerifySMSCode")]
		public async Task<ActionResult> VerifySMSCode(int ICNumber, string code)
		{
			try
			{

				var user = await _userManager.Users.SingleOrDefaultAsync(u => u.ICNumber == ICNumber);
				if (user == null)
				{
					return StatusCode(StatusCodes.Status401Unauthorized);
				}
				//TODO: Verify with the 3rd party the code with the mobile number
				user.PhoneNumberConfirmed = true;
				await _userManager.UpdateAsync(user);

			}
			catch (Exception ex)
			{
				_logger.LogError(ex.Message, ex);
				return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
			}
			return StatusCode(Convert.ToInt16(MessageCodes.Success), true);
		}
		
		[Route("VerifyEmail")]
		public async Task<ActionResult> VerifyEmail(string Email)
		{
			return StatusCode(Convert.ToInt16(MessageCodes.Success), true);
		}
		[HttpPost]
		[Route("VerifySMSCode")]
		public async Task<ActionResult> VerifyEmailCode(string Email, string code)
		{
			try
			{
				//TODO: Verify with the 3rd party the code with the Email

				var user = await _userManager.Users.SingleOrDefaultAsync(u => u.Email == Email);
				if (user == null)
				{
					return StatusCode(StatusCodes.Status401Unauthorized);
				}
				user.EmailConfirmed = true;
				await _userManager.UpdateAsync(user);

			}
			catch (Exception ex)
			{
				_logger.LogError(ex.Message, ex);
				return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
			}
			return StatusCode(Convert.ToInt16(MessageCodes.Success), true);
		}

		[HttpPost]
		[Route("setPolicyConfirmed")]
		public async Task<ActionResult> setPolicyConfirmed(string userID)
		{
			try
			{
				var user = await _userManager.FindByIdAsync(userID)  ;
				if (user == null)
				{
					return StatusCode(StatusCodes.Status401Unauthorized);
				}
				user.PolicyConfirmed = true;
				await _userManager.UpdateAsync(user);

			}
			catch (Exception ex)
			{
				_logger.LogError(ex.Message, ex);
				return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
			}
			return StatusCode(Convert.ToInt16(MessageCodes.Success), true);
		}

		[HttpPost]
		[Route("setPinNumber")]
		public async Task<ActionResult> setPinNumber(string userID,int PinNumber)
		{
			try
			{
				var user = await _userManager.FindByIdAsync(userID)  ;
				if (user == null)
				{
					return StatusCode(StatusCodes.Status401Unauthorized);
				}
				user.PinNumber = PinNumber;
				await _userManager.UpdateAsync(user);

			}
			catch (Exception ex)
			{
				_logger.LogError(ex.Message, ex);
				return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
			}
			return StatusCode(Convert.ToInt16(MessageCodes.Success), true);
		}


		[HttpPost]
		[Route("setBiometricData")]
		public async Task<ActionResult> setBiometricData(string userID, byte[] BiometricData)
		{
			try
			{
				var user = await _userManager.FindByIdAsync(userID)  ;
				if (user == null)
				{
					return StatusCode(StatusCodes.Status401Unauthorized);
				}
				user.BiometricData = BiometricData;
				await _userManager.UpdateAsync(user);

			}
			catch (Exception ex)
			{
				_logger.LogError(ex.Message, ex);
				return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
			}
			return StatusCode(Convert.ToInt16(MessageCodes.Success), true);
		}

		[Route("GetUserData")]
		public async Task<ActionResult> GetUserData(string userID)
		{

			try
			{
				var user = await _userManager.FindByIdAsync(userID) ;
				if (user == null)
				{
					return StatusCode(StatusCodes.Status401Unauthorized);
				}

				return StatusCode(Convert.ToInt16(MessageCodes.Success), user);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex.Message, ex);
				return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
			}
		}

		private async Task<string> GetFullJwt(IdentityUser user)
		{
			try
			{
				#region Token Generation
				// create claims for the user
				var claims = new List<Claim>
			 {
			  new Claim(ClaimTypes.Name, user.UserName),
			  new Claim(ClaimTypes.NameIdentifier, user.Id)

			 };

				//var roles = await _userManager.GetRolesAsync(user);
				//foreach (var role in roles)
				//{
				//	claims.Add(new Claim(ClaimTypes.Role, role));
				//}

				// create a signing key
				var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("PreNOQh4e_qnxdNU_dqdHP1p_rUsdm28"));
				//var signinKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("this is my custom Secret key for authentication"));

				// create a signing credential
				var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

				// create a token
				var token = new JwtSecurityToken(
					issuer: "MyEnvironment",
					audience: "https://localhost",
					claims: claims,
					expires: DateTime.Now.AddMinutes(30),
					signingCredentials: creds
				);
				string TokenString = new JwtSecurityTokenHandler().WriteToken(token);
				//var userRole = token.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

				return TokenString;
				#endregion

			}
			catch (Exception EX)
			{
				_logger.LogError(EX.Message, EX);
				throw;
			}


		}
	}

}

