using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.DirectoryServices.AccountManagement;
using SurveyService.DAL.Abstract;

namespace SurveyService.WebUI.Controllers
{
    public class StartSettingsController : Controller
    {
        IUserRepository userRepository;
        public StartSettingsController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Completed(string adminLogin)
        {
            using (var context = new PrincipalContext(ContextType.Domain, "demo")) //TODO domain name
            {
                var user = UserPrincipal.FindByIdentity(context, adminLogin);
                if (user != null)
                {
                    userRepository.Create(new SurveyService.Models.User() { Login = user.Name, DisplayName = user.DisplayName, Role = "admin" });
                    return Redirect("/Admin/Surveys");
                }
                else
                {
                    // TODO Непредвиденная ошибка добавления пользователя.
                    return Redirect("/StartSettings/Index");
                }
            }
        }

        [HttpPost]
        public JsonResult CheckLogin([FromBody]Newtonsoft.Json.Linq.JObject data)
        {
            if (String.IsNullOrEmpty(data.GetValue("login").ToString()))
            {
                return Json(new { success = false });
            }
            using (var context = new PrincipalContext(ContextType.Domain, "demo")) //TODO domain name
            {
                var user = UserPrincipal.FindByIdentity(context, data.GetValue("login").ToString());
                if(user != null)
                {
                    return Json(new { success = true, name = user.DisplayName });
                }
                else
                {
                    return Json(new { success = false });
                }
            }
        }
    }
}