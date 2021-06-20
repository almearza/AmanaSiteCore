using System.Text;
using System.Threading.Tasks;
using AmanaSite.Data;
using AmanaSite.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace API.Extensions
{
    public static class IdentityServiceExtensions
    {
        public static IServiceCollection AddIdentityServiceExtensions(this IServiceCollection services, IConfiguration config)
        {

            services.AddIdentityCore<AppUser>(opt =>
            {
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequireUppercase=true;
            }
            )
            .AddRoles<AppRole>()
            .AddRoleManager<RoleManager<AppRole>>()
            .AddSignInManager<SignInManager<AppUser>>()
            .AddRoleValidator<RoleValidator<AppRole>>()
            .AddEntityFrameworkStores<DContext>()
            .AddTokenProvider<DataProtectorTokenProvider<AppUser>>(TokenOptions.DefaultProvider);


            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
           .AddJwtBearer(options =>
           {
               options.TokenValidationParameters = new TokenValidationParameters
               {
                   ValidateIssuerSigningKey = true,
                   IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"])),
                   ValidateIssuer = false,
                   ValidateAudience = false,
               };
               //    options.Events=new JwtBearerEvents{
               //        OnMessageReceived=context=>{
               //            var accessToken =context.Request.Query["access_token"];
               //            var path = context.Request.Path;
               //            if(!string.IsNullOrEmpty(accessToken) && path.StartsWithSegments("/hubs")){//from startup
               //                context.Token= accessToken;
               //            }
               //            return Task.CompletedTask;
               //        }
               //    };
           });
            services.AddAuthorization(opt =>
            {
                opt.AddPolicy("AdminLevel", p => p.RequireRole("AdminLevel"));
                opt.AddPolicy("NewsLevel", p => p.RequireRole("AdminLevel", "NormalLevel"));
                opt.AddPolicy("MobFaLevel", p => p.RequireRole("AdminLevel", "MobFaLevel"));
                opt.AddPolicy("AdsLevel", p => p.RequireRole("AdminLevel", "AdsLevel"));
                opt.AddPolicy("SurveyLevel", p => p.RequireRole("AdminLevel", "SurveyLevel"));
                opt.AddPolicy("AllLevels", p => p.RequireRole("AdminLevel", "NormalLevel", "MobFaLevel", "AdsLevel", "SurveyLevel"));

            });
            return services;
        }
    }
}