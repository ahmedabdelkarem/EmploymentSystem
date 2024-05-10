using Employment.Application.IServices;
using Microsoft.Extensions.Caching.Distributed;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Employment.Application.Services
{
	public class CacheService : ICacheService
	{
		private readonly IDistributedCache _cache;
		public CacheService(IDistributedCache cache)
		{
			_cache = cache;
		}

		public void Dispose()
		{
			throw new NotImplementedException();
		}

		public async Task<T> GetObjectFromCache<T>(string key)
		{
			var cachedValue = await _cache.GetStringAsync(key);

			if (cachedValue != null)
			{
				return JsonSerializer.Deserialize<T>(cachedValue);
			}

			return default;
		}

		public async Task SetObjectInCache<T>(string key, T value)
		{
			var options = new DistributedCacheEntryOptions
			{
				AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10)
			};

			var serializedValue = JsonSerializer.Serialize(value);

			await _cache.SetStringAsync(key, serializedValue, options);
		}
	}
}
