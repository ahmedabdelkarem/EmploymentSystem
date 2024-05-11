
using Employment.Infra.Data.Context;
using Employment.Infra.Data.Repository;
using Employment.Services.Api.Configurations;
using MediatR;
using NetDevPack.Identity.User;
using NetDevPack.Mediator;
using FluentValidation.Results;
using Microsoft.Extensions.Configuration;
using NetDevPack.Identity;
using NetDevPack.Identity.Jwt;
using Microsoft.EntityFrameworkCore;
using MassTransit;
using Microsoft.AspNetCore.Identity;
using Employment.Domain.IRepository;
using Microsoft.AspNetCore.Hosting;
using Serilog;
using Employment.Application.IServices;
using Employment.Application.Services;
using StackExchange.Redis;
using AutoMapper;
using Employment.Application.AutoMapper;
using Employment.Services.Api.AutoMapper;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDatabaseConfiguration(builder.Configuration);

// ASP.NET Identity Settings & JWT
// Default EF Context for Identity (inside of the NetDevPack.Identity)
builder.Services.AddIdentityEntityFrameworkContextConfiguration(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
        b => b.MigrationsAssembly("Employment.Infra.CrossCutting.Identity")));

// Default Identity configuration from NetDevPack.Identity
builder.Services.AddIdentityConfiguration();

// Default JWT configuration from NetDevPack.Identity
builder.Services.AddJwtConfiguration(builder.Configuration, "AppSettings");
// Interactive AspNetUser (logged in)
// NetDevPack.Identity dependency
builder.Services.AddAspNetUserConfiguration();

// AutoMapper Settings
builder.Services.AddAutoMapperConfiguration();

// Swagger Config
builder.Services.AddSwaggerConfiguration();


// Configure logging with Serilog
Log.Logger = new LoggerConfiguration()
	.WriteTo.Console()
	.WriteTo.File("bin/log/log-.txt", rollingInterval: RollingInterval.Day)
	.CreateLogger();
builder.Host.UseSerilog();

//Redis Cache
builder.Services.AddStackExchangeRedisCache(options =>
{
	options.Configuration = "redis-13259.c74.us-east-1-4.ec2.redns.redis-cloud.com:13259"; // Change to your Redis server configuration
	options.InstanceName = "SampleInstance"; // Change to a meaningful instance name
});

//Auto Mapper Configurations
var mapperConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new APIAutoMapper());
    mc.AddProfile(new DBAutoMapper());
});
IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);


//Services
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IVacancyService, VacancyService>();
builder.Services.AddScoped<ICacheService, CacheService>();

// Infra - Data
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IVacancyRepository, VacancyRepository>();
builder.Services.AddScoped<IdentityEmploymentContext>();



var app = builder.Build();  

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
