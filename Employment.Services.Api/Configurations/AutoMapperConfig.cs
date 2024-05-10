using System;
using Employment.Application.AutoMapper;
using Employment.Application.AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace Employment.Services.Api.Configurations
{
    public static class AutoMapperConfig
    {
        public static void AddAutoMapperConfiguration(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            //services.AddAutoMapper(typeof(DomainToViewModelMappingProfile), typeof(ViewModelToDomainMappingProfile));
        }
    }
}