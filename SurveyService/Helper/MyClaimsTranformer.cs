using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using SurveyService.DAL.Abstract;
using SurveyService.Models;
using System;
using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SurveyService.WebUI.Helper
{
    //public class MyUserStore : IUserStore<User>
    //{

    //}

	//public class ClaimsTransformer : IClaimsTransformation
	//{
	//	// Can consume services from DI as needed, including scoped DbContexts
	//	public ClaimsTransformer(IHttpContextAccessor httpAccessor) { }
	//	public Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal p)
	//	{
	//		p.AddIdentity(new ClaimsIdentity());
	//		return Task.FromResult(p);
	//	}
	//}
	public class ClaimsTranformer : IClaimsTransformation
	{
		IUserRepository userRepository;
		
		//public async Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
		public async Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
		{
			// Сделать логику авторизации
			// Если нужно ограничить доступ к меотду контроллера, перед методом добавить стороку 
			// [Authorize(Policy = "Admin")]
			if (principal.Identity.Name.ToLower() == "demo\\administrator")
			{
				(principal.Identity as ClaimsIdentity).AddClaim(new Claim("isAdmin", "true"));
			}
			return principal;

		}
	}
}
