using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.Http;
using ResearchData.Models;
using ResearchData.Models.EF;
using ResearchData.Infrastructure;
using ResearchData.Models.Interfaces;

// using ResearchData.Models.Repositories.UserDB;

namespace ResearchData
{
    public class Startup
    {

        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            //DB & Framework Services
            services.AddDbContext<AppIdentityDbContext>(options => options.UseSqlServer(Configuration["Data:UserDB2:ConnectionString"]));

            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(Configuration["Data:ResearchData2:ConnectionString"]));


            //Register Custom Password (Validation) Services + (built-in validation)
             services.AddTransient<IPasswordValidator<User>, CustomPasswordValidator>();

            //Register Custom Username (Validation) Services + (built-in validation)
            services.AddTransient<IUserValidator<User>, CustomUserValidator>();

            //Identity
            services.AddIdentity<User, IdentityRole>(opt =>
            {
                //Username Options
                opt.User.RequireUniqueEmail = true;

                //Password Options
                opt.Password.RequiredLength = 6;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequireLowercase = false;
                opt.Password.RequireUppercase = false;
                opt.Password.RequireDigit = false;

            }).AddEntityFrameworkStores<AppIdentityDbContext>()
               .AddDefaultTokenProviders();

            //MVC - Route Config
            services.AddMvc(options => options.EnableEndpointRouting = false);
          
            //Application Service Registration
            services.AddTransient<IAccount, EFAccount>();
            services.AddTransient<IAddress, EFAddress>();
            services.AddTransient<IAnswer, EFAnswer>();
            services.AddTransient<IQuestion, EFQuestion>();           
            services.AddTransient<IQuestionSubsection, EFQuestionSubsection>();
            services.AddTransient<IQuestionOption, EFQuestionOption>();
            services.AddTransient<IQuestionType, EFQuestionType>();
            services.AddTransient<ISection, EFSection>();
            services.AddTransient<ISubsection, EFSubsection>();
            services.AddTransient<ISubunit, EFSubunit>();
            services.AddTransient<ISurvey, EFSurvey>();
            services.AddTransient<ISurveyQuestion, EFSurveyQuestion>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddAuthentication();
            //services.AddControllersWithViews();

            // services.AddControllersWithViews(options => { 
            //     options.Filters.Add<Components.ViewBagActionFilter>();
            // });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseStatusCodePages();
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

            //Users
            // app.UseMvcWithDefaultRoute();

            //Users
            app.UseAuthorization();
            app.UseAuthentication();

            // app.UseEndpoints(endpoints =>
            // {
            //     endpoints.MapControllerRoute(
            //         name: "default",
            //         pattern: "{controller=Home}/{action=Index}/{id?}");
            // });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: null,
                    template: "",
                    defaults: new { controller = "Home", action = "Index" }
                    );

                routes.MapRoute(
                    name: null,
                    template: "{HLineItemID}/Page{ItemPage:int}",
                    defaults: new { controller = "HLineItem", action = "List" }
                    );

                routes.MapRoute(
                    name: null,
                    template: "",
                    defaults: new { Controller = "HLineItem", action = "List", itemPage = 1 });

                routes.MapRoute(
                    name: null,
                    template: "{controller}/{action}/{id?}");

            });

            //AppIdentityDbContext.CreateAdminAccount(app.ApplicationServices, Configuration).Wait();
        }
    }
}
