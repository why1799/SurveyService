using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace SurveyService.WebUI.Middleware
{
    public class AuthorizationMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthorizationMiddleware(RequestDelegate next)
        {
            this._next = next;
        }
        
        public async Task InvokeAsync(HttpContext context)
        {
            if(context.User.HasClaim("FirstUser", "true") && !context.Request.Path.ToString().Contains("StartSettings"))
            {
                context.Response.Redirect("StartSettings/Index");
                //await _next.Invoke(context);
            }
            else
            {
                await _next.Invoke(context);
            }
        }
    }
}
