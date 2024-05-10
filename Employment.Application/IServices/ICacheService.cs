using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Employment.Application.ViewModels;
using FluentValidation.Results;

namespace Employment.Application.IServices
{
    public interface ICacheService : IDisposable
    {
		Task<T> GetObjectFromCache<T>(string key);

		Task SetObjectInCache<T>(string key, T value);


	}
}
