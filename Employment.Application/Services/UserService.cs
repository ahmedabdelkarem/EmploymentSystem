using Employment.Application.IServices;
using Employment.Application.ViewModels;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employment.Application.Services
{
    public class UserService : IUserService
    {
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<UserDto>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<UserDto> GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<ValidationResult> Register(UserDto customerViewModel)
        {
            throw new NotImplementedException();
        }

        public Task<ValidationResult> Remove(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<ValidationResult> Update(UserDto customerViewModel)
        {
            throw new NotImplementedException();
        }
    }
}
