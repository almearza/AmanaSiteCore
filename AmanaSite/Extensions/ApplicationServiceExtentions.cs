using System;
using System.Net.Http.Headers;
using AmanaSite.Data;
using AmanaSite.Interfaces;
using AmanaSite.Remote;
using AmanaSite.Repositories;
using API.Helpers;
using API.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AmanaSite.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServiceExtensions(this IServiceCollection services, IConfiguration config)
        {
            services.AddControllersWithViews(options =>
            {
                options.Filters.Add(typeof(CustomExceptionFilter));
            });
            services.AddDbContext<DContext>(options =>
            {
                options.UseSqlServer(config.GetConnectionString("DefaultConnection"));
            });
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);

            //add httpClient service:
            // services.AddHttpClient<AmanaApi>(c=>{
            //     c.BaseAddress=new Uri(config["AmanaApiUrl"]);
            //     c.DefaultRequestHeaders.Add("Accept","application/json");
            //     c.DefaultRequestHeaders.Authorization=new AuthenticationHeaderValue("Bearer","");
            // });
            services.AddHttpClient<AmanaToken>();
            services.AddHttpClient<AmanaApi>();
            return services;
        }
    }
}