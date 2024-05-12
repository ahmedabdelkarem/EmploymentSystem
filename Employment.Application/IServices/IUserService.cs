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
        
       

    }
}
