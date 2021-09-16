using System.Collections.Generic;
using System.Globalization;
using AmanaSite.Extensions;
using API.Extensions;
using GoogleReCaptcha.V3;
using GoogleReCaptcha.V3.Interface;
// using AspNetCore.ReCaptcha;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace AmanaSite
{
    public class Startup
    {
        private readonly IConfiguration _config;
        public Startup(IConfiguration config)
        {
            this._config = config;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplicationServiceExtensions(_config);
            services.AddIdentityServiceExtensions(_config);
            services.AddCors();

            services.AddLocalization(options => options.ResourcesPath = "Resources");

            services.AddControllersWithViews()
                .AddViewLocalization()
                .AddDataAnnotationsLocalization();
            // .AddDataAnnotationsLocalization(options =>
            // {
            //     options.DataAnnotationLocalizerProvider = (type, factory) =>
            //         factory.Create(typeof(SharedResource));
            // });
            services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new[] { new CultureInfo("ar"), new CultureInfo("en"), new CultureInfo("ur") };

                options.DefaultRequestCulture = new RequestCulture(culture: "ar", uiCulture: "ar");
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
                options.RequestCultureProviders =
                new List<IRequestCultureProvider> { new QueryStringRequestCultureProvider(), new CookieRequestCultureProvider() };
            });
            services.AddHttpClient<ICaptchaValidator, GoogleReCaptchaValidator>();
            // services.AddReCaptcha(_config.GetSection("ReCaptcha"));



        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseCors(policy => policy
           .AllowCredentials()//this because we recieve token as query pram in signalR
           .AllowAnyHeader()
           .AllowAnyMethod()
           .WithOrigins("https://localhost:4200"));



            app.UseAuthentication();
            app.UseAuthorization();

            //var locOptions = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();

            app.UseRequestLocalization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

        }
    }
}
