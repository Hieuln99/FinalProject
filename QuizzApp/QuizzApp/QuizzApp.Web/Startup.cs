using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using QuizzApp.Data.DbContext;
using QuizzApp.Data.Entities;
using QuizzApp.Repository.Infrastructures;
using QuizzApp.Web.Mapping;
using System;

namespace QuizzApp.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
           .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, configOptions =>
           {
               configOptions.LoginPath = "/Account/Login"; // 401: unauhoried
               configOptions.AccessDeniedPath = "/Account/Denied"; // 403 : forbinden
               configOptions.ReturnUrlParameter = "returnUrl";
               configOptions.SlidingExpiration = true;
           });

            services.ConfigureApplicationCookie(config =>
            {
                //config.Cookie.HttpOnly = true;
                config.ExpireTimeSpan = TimeSpan.FromSeconds(560);
            });


            services.AddRazorPages();
            services.AddControllersWithViews();

            string dbConnectionString = Configuration.GetConnectionString("QuizzAppDb");
            services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(dbConnectionString));

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddMvc(options =>
                options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute()));
            services.AddAutoMapper(typeof(Maps));

            /* services.AddDefaultIdentity<User>(options =>
             {
                 options.Password.RequiredLength = 1;
             }).AddEntityFrameworkStores<AppDbContext>();*/
            services.AddIdentity<User, Role>()
                     .AddEntityFrameworkStores<AppDbContext>()
                     .AddTokenProvider<DataProtectorTokenProvider<User>>(TokenOptions.DefaultProvider)
                     .AddDefaultUI();

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
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();

                endpoints.MapAreaControllerRoute(
                    name: "Admin",
                    areaName: "Admin",
                    pattern: "Admin/{controller=Answers}/{action=Index}/{id?}");
                
                endpoints.MapAreaControllerRoute(
                    name: "User",
                    areaName: "UserArea",
                    pattern: "UserArea/{controller=Courses}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Privacy}/{id?}");
            });
        }
    }
}
