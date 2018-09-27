using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using SurveyService.DAL.Abstract;
using SurveyService.DAL.Concrete;
using SurveyService.Models;
using System;
using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using SurveyService.DAL;

namespace SurveyService.WebUI.Helper
{
    public class ClaimsTranformer : IClaimsTransformation
    {
        public static IServiceProvider ServiceProvider { get; set; }

        public async Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
        {
            using (var services = ServiceProvider.CreateScope())
            {
                var userRepository = services.ServiceProvider.GetRequiredService<IUserRepository>();

                using (PrincipalContext pc = new PrincipalContext(ContextType.Domain))
                {
                    UserPrincipal up = UserPrincipal.FindByIdentity(pc, principal.Identity.Name);

                    var user = userRepository.GetItems().SingleOrDefault(x => x.Login == up.Name);
                    if (user == null)
                    {
                        if (userRepository.GetItems().Count() == 0)
                        {
                            (principal.Identity as ClaimsIdentity).AddClaim(new Claim("FirstUser", "true"));
                        }
                        else
                        {
                            user = new User() { DisplayName = up.DisplayName, Login = up.Name, Role = "user" };
                            await userRepository.Create(user);
                        }
                    }
                    else
                    {
                        if (user.Role == "admin")
                        {
                            (principal.Identity as ClaimsIdentity).AddClaim(new Claim("isAdmin", "true"));
                        }
                    }
                }
                return principal;
            }
        }
    }
}
