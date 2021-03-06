using DataContext.Context;
using IoC;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Models.Entities.User;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Utilities.Convertors;

namespace Presentation
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            #region Session
            services.AddSession(options => {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
            });
            #endregion

            #region PersianCalender

            CultureInfo.DefaultThreadCurrentCulture
              = CultureInfo.DefaultThreadCurrentUICulture
              = PersianDateExtensionMethods.GetPersianCulture();

            #endregion

            services.AddControllersWithViews();

            #region Context
            services.AddDbContext<KhadamatContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("KhadamatConnection")));
            #endregion

            #region IdentityServices
            services.AddIdentity<User, IdentityRole>(option =>
            {
                option.Password.RequireDigit = false;
                option.Password.RequireLowercase = false;
                option.Password.RequireUppercase = false;
                option.Password.RequireNonAlphanumeric = false;



            })
           .AddEntityFrameworkStores<KhadamatContext>()
           .AddDefaultTokenProviders();
            #endregion

            #region Ioc

            RegisterServices(services);

            #endregion

            #region HttpContext
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            #endregion
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseSession(); 
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(

                    name: "MyAreas",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                );
                endpoints.MapControllerRoute(

                    name: "Default",
                    pattern: "{controller=Home}/{action=Index}/{id?}"
                );
            });
        }
        public static void RegisterServices(IServiceCollection services)
        {
            DependencyContainer.RegisterServices(services);

        }
    }
}
