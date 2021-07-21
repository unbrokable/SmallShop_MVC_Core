using BLL.Interfaces;
using DAL;
using DAL.Interfaces;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Shop.Data;
using Shop.Interfaces;
using Shop.Models;
using Shop.Models.Validators;
using Shop.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop
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
            services.AddDbContext<ApplicationContext>();
            services.AddControllersWithViews().AddFluentValidation();

            services.AddTransient<IValidator<RegistrationModel>, RegistrationValidator>();
            services.AddTransient<IValidator<PasswordModel>, PasswordModelValidator>();
            services.AddTransient<IValidator<UserModel>, UserModelValidator>();


            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme);
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IAuthorizationServiceShop, AuthorizationService>();
            services.AddTransient<IPurchaseService, PurchaseService>();
            services.AddTransient<IShopService, ShopService>();

            services.AddTransient<IRepository, Repository>();
            
            services.AddSession();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options => 
            {
                    options.LoginPath = new Microsoft.AspNetCore.Http.PathString("/Authorization/Login");
            });
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
            app.UseSession();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
