using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.IISIntegration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SurveyService.DAL;
using SurveyService.DAL.Abstract;
using SurveyService.DAL.Concrete;

namespace SurveyService
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
            services.AddAuthentication(IISDefaults.AuthenticationScheme);

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<SurveyServiceDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            //services.AddTransient<IAnswerRepository, SurveyServiceAnswerRepository>();
            services.AddTransient<IOptionRepository, SurveyServiceOptionRepository>();
            services.AddTransient<IOptionsForAnswerRepository, SurveyServiceOptionsForAnswerRepository>();
            services.AddTransient<IUserAnswerRepository, SurveyServiceUserAnswerRepository>();
            services.AddTransient<ISurveyRepository, SurveyServiceSurveyRepository>();
            services.AddTransient<ISurveyQuestionRepository, SurveyServiceSurveyQuestionRepository>();
            services.AddTransient<IUserRepository, SurveyServiceUserRepository>();

            /* services.AddIdentity<Controllers.TestUser, IdentityRole>()
     .AddEntityFrameworkStores<Controllers.TestDbContext>()
     .AddDefaultTokenProviders();*/

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
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

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Admin}/{action=Create}");
            });
            //app.UseMvc(routes =>
            //{
            //    routes.MapRoute(
            //        name: "redirectToSurvey",
            //        template: "{controller=Survey}/{action=index}/{id?}/{anew?}");
            //});
        }
    }
}
