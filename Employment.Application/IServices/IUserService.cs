using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Employment.Application.ViewModels;
using FluentValidation.Results;

namespace Employment.Application.IServices
{
    public interface IUserService : IDisposable
    {
        Task<IEnumerable<UserDto>> GetAll();
        Task<UserDto> GetById(Guid id);
        
        Task<ValidationResult> Register(UserDto customerViewModel);
        Task<ValidationResult> Update(UserDto customerViewModel);
        Task<ValidationResult> Remove(Guid id);

    }
}
