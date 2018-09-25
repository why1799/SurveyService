using Microsoft.AspNetCore.Http;
using SurveyService.DAL.Abstract;
using SurveyService.Models;
using System;
using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace SurveyService.WebUI.Helper
{
    static public class UserHelper
    {
        static public User GetCurrentUser(HttpContext context, IUserRepository userRepository)
        {
            ClaimsPrincipal principal = context.User as ClaimsPrincipal;
            using (PrincipalContext pc = new PrincipalContext(ContextType.Domain))
            {
                UserPrincipal up = UserPrincipal.FindByIdentity(pc, principal.Identity.Name);
                
                var user = userRepository.GetItems().SingleOrDefault(x => x.Login == up.Name);
                if (user == null)
                {
                    user = new SurveyService.Models.User() { DisplayName = up.DisplayName, Login = up.Name, Role = "user" };
                    userRepository.Create(user).Wait();
                }
                return user;
            }
        }
        static public User GetUser(HttpContext context)
        {
            string DisplayName, Email;

            ClaimsPrincipal principal = context.User as ClaimsPrincipal;
            using (PrincipalContext pc = new PrincipalContext(ContextType.Domain))
            {
                UserPrincipal up = UserPrincipal.FindByIdentity(pc, principal.Identity.Name);

                DisplayName = up.DisplayName;
                Email = up.EmailAddress;
            }
            return new User { DisplayName = DisplayName, Login = Email, Role = "user" };
        }
    }
}
