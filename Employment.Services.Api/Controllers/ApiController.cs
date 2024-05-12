using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using AutoMapper;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Employment.Services.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class ApiController : ControllerBase
    {
        private readonly ICollection<string> _errors = new List<string>();
        protected readonly IMapper _mapper;
        protected readonly ILogger _logger;
        
        public ApiController(IMapper _mapper, ILogger logger)
        {
            this._mapper = _mapper;
            this._logger = logger;
        }
        protected ActionResult CustomResponse(object result = null)
        {
            if (IsOperationValid())
            {
                return Ok(result);
            }

            return BadRequest(new ValidationProblemDetails(new Dictionary<string, string[]>
            {
                { "Messages", _errors.ToArray() }
            }));
        }

        protected ActionResult CustomResponse(ModelStateDictionary modelState)
        {
            var errors = modelState.Values.SelectMany(e => e.Errors);
            foreach (var error in errors)
            {
                AddError(error.ErrorMessage);
            }

            return CustomResponse();
        }

        protected ActionResult CustomResponse(ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors)
            {
                AddError(error.ErrorMessage);
            }

            return CustomResponse();
        }

        protected bool IsOperationValid()
        {
            return !_errors.Any();
        }

        protected void AddError(string erro)
        {
            _errors.Add(erro);
        }

        protected void ClearErrors()
        {
            _errors.Clear();
        }
        protected string? GetTokenData()
        {
            string? userId = User.Claims.FirstOrDefault(o => o.Type == ClaimTypes.NameIdentifier)?.Value.ToString();
            return userId;
        }
    }
}
