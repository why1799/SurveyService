using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
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
            //services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            //    .AddCookie(options => //CookieAuthenticationOptions
            //    {
            //        options.AccessDeniedPath = new PathString("/Authorization/Index");
            //        options.LoginPath = new Microsoft.AspNetCore.Http.PathString("/Authorization/Index");
            //    });
            services.AddAuthentication(IISDefaults.AuthenticationScheme).AddCookie();
            services.AddSingleton<IClaimsTransformation, SurveyService.WebUI.Helper.ClaimsTransformer>();
            //services.ConfigureApplicationCookie(options => options.LoginPath = "/Authorization/Index");

            //services.AddScoped<IClaimsTransformation, SurveyService.WebUI.Helper.MyClaimsTranformer>();
            //services.AddTransient<IClaimsTransformation, SurveyService.WebUI.Helper.MyClaimsTranformer>();

            //services.Configure<CookiePolicyOptions>(options =>
            //{
            //    // This lambda determines whether user consent for non-essential cookies is needed for a given request.
            //    options.CheckConsentNeeded = context => true;
            //    options.MinimumSameSitePolicy = SameSiteMode.None;
            //});
            services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminOnly", policy =>
                {
                    policy.RequireClaim("isAdmin");
                });
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
            app.UseAuthentication();
            app.UseStatusCodePages(async context =>
            {
                if (context.HttpContext.Response.StatusCode == 403)
                {
                    context.HttpContext.Response.Redirect("/Authorization/Index?ReturnUrl=" + context.HttpContext.Request.Path + context.HttpContext.Request.QueryString);
                }
            });
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Admin}/{action=Surveys}");
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
