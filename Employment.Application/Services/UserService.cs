using Employment.Application.IServices;
using Employment.Application.ViewModels;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employment.Application.Services
{
    public class UserService : IUserService
    {
		private readonly ICacheService _cacheService;
        public UserService(ICacheService cacheService)
        {
			_cacheService = cacheService;
        }

        public void Dispose()
        {
        }
        public async Task<IEnumerable<UserDto>> GetAll()
        {
            try
            {
                //If users saved before 
                var Users = await _cacheService.GetObjectFromCache<List<UserDto>>("AllUsers");
                if (Users == null)
                {
                    //TODO : Get users from DB
                    //Fake Date 
                    var AllUsers = new List<UserDto>();
                    var User1 = new UserDto();
                    User1.Name = "Ahmed";
                    var User2 = new UserDto();
                    User2.Name = "Doaa";
                    AllUsers.Add(User1);
                    AllUsers.Add(User2);
                    var options = new DistributedCacheEntryOptions
                    {
                        AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1)
                    };

                    await _cacheService.SetObjectInCache("AllUsers", AllUsers);

                    return AllUsers;

                }
                return Users;
            }
            catch(Exception ex)
            {
                //_logger
                throw;
            }
        }

      
    }
}
