using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SurveyService.DAL.Abstract;
using System.DirectoryServices.AccountManagement;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;


namespace SurveyService.WebUI.Controllers
{
    public class AuthorizationController : Controller
    {
        IUserRepository userRepository;

        public AuthorizationController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        [ValidateAntiForgeryToken]
        public IActionResult Index()
        {
            string DisplayName, Email;

            ClaimsPrincipal principal = HttpContext.User as ClaimsPrincipal;
            using (PrincipalContext pc = new PrincipalContext(ContextType.Domain))
            {
                UserPrincipal up = UserPrincipal.FindByIdentity(pc, principal.Identity.Name);

                DisplayName = up.DisplayName;
                Email = up.EmailAddress;
                var user = userRepository.GetItems().SingleOrDefault(x => x.Login == Email);
                if (user == null)
                {
                    user = new Models.User() { DisplayName = DisplayName, Login = Email, Role = "user" };
                    userRepository.Create(user).Wait();
                }
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, user.DisplayName),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role)
                };
                ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id)).Wait();
            }
            return View();
        }
    }
}