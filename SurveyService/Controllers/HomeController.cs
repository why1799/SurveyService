using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SurveyService.DAL;
using SurveyService.DAL.Abstract;
using SurveyService.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using System.DirectoryServices.AccountManagement;

namespace SurveyService.Controllers
{
    public class HomeController : Controller
    {
        private IAnswerRepository answerRepository;
        private IUserRepository userRepository;

        public HomeController(IAnswerRepository answerRepository, IUserRepository userRepository)
        {
            this.answerRepository = answerRepository;
            this.userRepository = userRepository;
        }
        public async Task<IActionResult> Index()
        {
            //context.Users.Add(new User { Login = "login", DisplayName="Соска" });
            //var user = await userRepository.Create(new User { Login = "login2", DisplayName = "Соска2" });
            //var user = userRepository.GetItems().First(x => x.Login == "login3") ;
            //await userRepository.Delete(user);

              

            //return View(await userRepository.GetItem(user.Id));
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";
            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Test()
        {
            try
            {
                var user = (WindowsIdentity)HttpContext.User.Identity;

                ClaimsPrincipal principal = HttpContext.User as ClaimsPrincipal;
                using (PrincipalContext pc = new PrincipalContext(ContextType.Domain))
                {
                    UserPrincipal up = UserPrincipal.FindByIdentity(pc, principal.Identity.Name);

                    var DisplayName =  up.DisplayName;
                    // or return up.GivenName + " " + up.Surname;
                }



                /*await context.Response
                             .WriteAsync($"User: {user.Name}\tState: {user.ImpersonationLevel}\n");*/

                WindowsIdentity.RunImpersonated(user.AccessToken, () =>
                {
                    //
                    var impersonatedUser = WindowsIdentity.GetCurrent();
                    /*string fullname = HttpContext.Current.GetOwinContext()
    .GetUserManager<ApplicationUserManager>()
    .FindById(HttpContext.Current.User.Identity.GetUserId()).FullName;*/
                    var message =
                        $"User: {impersonatedUser.Name}\tState: {impersonatedUser.ImpersonationLevel}";
                    //impersonatedUserю
                    ViewData["Message"] = message;



                });
            }
            catch (Exception e)
            {
                ViewData["Message"] = "Что-то не получилось";
            }


            return View();
        }
    }

    public class TestUser : IdentityUser
    {
    }

    public class TestDbContext : IdentityDbContext<TestUser>
    {
        public TestDbContext(DbContextOptions<TestDbContext> options)
        : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
