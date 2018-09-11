using Microsoft.AspNetCore.Authentication;
using SurveyService.DAL.Abstract;
using System;
using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SurveyService.WebUI.Helper
{
    public class MyClaimsTranformer : IClaimsTransformation
    {
        IUserRepository userRepository;
        MyClaimsTranformer(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }
        public async Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
        {
            string DisplayName, Email;
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
                return new ClaimsPrincipal(id);
            }
        }
    }
}
