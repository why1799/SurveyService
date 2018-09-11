using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using SurveyService.DAL.Abstract;
using SurveyService.WebUI.Helper;
using SurveyService.Models;
using Microsoft.EntityFrameworkCore;

namespace SurveyService.WebUI.Controllers
{
    public class AdminController : Controller
    {
        private IUserRepository userRepository;
        private ISurveyRepository surveyRepository;
        private ISurveyQuestionRepository surveyQuestionRepository;
        private IOptionRepository optionRepository;

        public AdminController(IUserRepository userRepository,
            ISurveyRepository surveyRepository,
            ISurveyQuestionRepository surveyQuestionRepository,
            IOptionRepository optionRepository)
        {
            this.userRepository = userRepository;
            this.surveyRepository = surveyRepository;
            this.surveyQuestionRepository = surveyQuestionRepository;
            this.optionRepository = optionRepository;
        }

        // GET: Admin
        public ActionResult Index()
        {

            return View();
        }

        // GET: Admin/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Admin/Create
        public async Task<ActionResult> Create()
        {
            var user = UserHelper.GetUser(HttpContext);
            var finduser = userRepository.GetItems().SingleOrDefault(x => x.Login == user.Login);
            if (finduser == null)
            {
                user = await userRepository.Create(user);
            }
            else
            {
                user = finduser;
            }
            //var survey = await surveyRepository.Create(new Models.Survey { CreatedById = user.Id, Title="Новый опрос", DateCreated = DateTime.Now });

            return RedirectToAction("Edit", new { id = "c7f0f21e-7c7c-45bf-a149-aa7602db9432"/*survey.Id*/ });
        }

        public async Task<ActionResult> Edit(string id)
        {
            var user = UserHelper.GetUser(HttpContext);
            var finduser = userRepository.GetItems().SingleOrDefault(x => x.Login == user.Login);
            if (finduser == null)
            {
                user = await userRepository.Create(user);
            }
            else
            {
                user = finduser;
            }

            var sur = surveyRepository.GetItems()
                    .Include(ob => ob.SurveyQuestion)
                                .ThenInclude(ob => ob.Options)
                    .Where(ob => ob.Id == id).FirstOrDefault();



            return View(sur);
        }

        public async Task<JsonResult> AddQuestion(string surveyid)
        {
            int order = surveyQuestionRepository.GetItems().Where(x => x.SurveyId == surveyid).Count();
            var question = await surveyQuestionRepository.Create(new SurveyQuestion { QuestionText = "Новый вопрос", HasOwnAnswer = false, IsRequired = false, SurveyId = surveyid, Type = 0, Order = order});
            return new JsonResult(new { surveyquestionid = question.Id, text = question.QuestionText });
        }

        public async Task<JsonResult> RemoveQuestion(string surveyquestionid)
        {
            var question = surveyQuestionRepository.GetItems().Include(x => x.Options).FirstOrDefault(x => x.Id == surveyquestionid);
            await surveyQuestionRepository.Delete(question);

            var options = question.Options;
            foreach (var option in options)
            {
                await optionRepository.Delete(option);
            }

            var questions = surveyQuestionRepository.GetItems().Where(x => x.SurveyId == question.SurveyId && x.Order > question.Order).ToList();
            foreach(var curquestion in questions)
            {
                curquestion.Order--;
                await surveyQuestionRepository.Update(curquestion);
            }

            return new JsonResult(surveyquestionid);
        }

        public async Task<JsonResult> UpQuestion(string questionid)
        {
            var firstquestion = await surveyQuestionRepository.GetItem(questionid);
            var secondquestion = surveyQuestionRepository.GetItems().FirstOrDefault(x => x.SurveyId == firstquestion.SurveyId && x.Order + 1 == firstquestion.Order);

            if (secondquestion == null)
            {
                return new JsonResult(null);
            }

            firstquestion.Order--;
            await surveyQuestionRepository.Update(firstquestion);

            secondquestion.Order++;
            await surveyQuestionRepository.Update(secondquestion);


            return new JsonResult(new
            {
                firstid = firstquestion.Id,
                secondid = secondquestion.Id
            });
        }

        public async Task<JsonResult> DownQuestion(string questionid)
        {
            var firstquestion = await surveyQuestionRepository.GetItem(questionid);
            var secondquestion = surveyQuestionRepository.GetItems().FirstOrDefault(x => x.SurveyId == firstquestion.SurveyId && x.Order - 1 == firstquestion.Order);

            if (secondquestion == null)
            {
                return new JsonResult(null);
            }

            firstquestion.Order++;
            await surveyQuestionRepository.Update(firstquestion);

            secondquestion.Order--;
            await surveyQuestionRepository.Update(secondquestion);


            return new JsonResult(new
            {
                firstid = firstquestion.Id,
                secondid = secondquestion.Id
            });
        }

        public async Task<JsonResult> AddOption(string surveyquestionid)
        {
            var order = optionRepository.GetItems().Where(x => x.QuestionId == surveyquestionid).Count();
            var option = await optionRepository.Create(new Option {Text = "Вариант ответа", Order = order, QuestionId = surveyquestionid });
            return Json(new { option = new Option { Id = option.Id, Text = option.Text }, surveyquestionid });
        }

        public async Task<JsonResult> RemoveOption(string optionid)
        {
            var option = await optionRepository.GetItem(optionid);
            await optionRepository.Delete(option);

            var options = optionRepository.GetItems().Where(x => x.QuestionId == option.QuestionId && x.Order > option.Order).ToList();
            foreach (var curoption in options)
            {
                curoption.Order--;
                await optionRepository.Update(curoption);
            }

            return new JsonResult(optionid);
        }

        public async Task<JsonResult> UpOption(string optionid)
        {
            var firstoption = await optionRepository.GetItem(optionid);
            var secondoption = optionRepository.GetItems().FirstOrDefault(x => x.QuestionId == firstoption.QuestionId && x.Order + 1 == firstoption.Order);

            if (secondoption == null)
            {
                return new JsonResult(null);
            }

            firstoption.Order--;
            await optionRepository.Update(firstoption);

            secondoption.Order++;
            await optionRepository.Update(secondoption);


            return new JsonResult(new {
                firstid = firstoption.Id,
                secondid = secondoption.Id
            });
        }

        public async Task<JsonResult> DownOption(string optionid)
        {
            var firstoption = await optionRepository.GetItem(optionid);
            var secondoption = optionRepository.GetItems().FirstOrDefault(x => x.QuestionId == firstoption.QuestionId && x.Order - 1 == firstoption.Order);

            if (secondoption == null)
            {
                return new JsonResult(null);
            }

            firstoption.Order++;
            await optionRepository.Update(firstoption);

            secondoption.Order--;
            await optionRepository.Update(secondoption);


            return new JsonResult(new
            {
                firstid = firstoption.Id,
                secondid = secondoption.Id
            });
        }

        [HttpPost]
        public async Task<JsonResult> Save(string id, string title, string description, string[][] lines)
        {
            var survey = await surveyRepository.GetItem(id);
            survey.Title = title;
            if(description == null)
            {
                description = "";
            }
            survey.Description = description;
            await surveyRepository.Update(survey);

            for(int i = 0; i < lines.Length; i++)
            {
                var question = await surveyQuestionRepository.GetItem(lines[i][0]);
                question.QuestionText = lines[i][1];
                question.Type = int.Parse(lines[i][2]);
                question.HasOwnAnswer = bool.Parse(lines[i][3]);
                question.IsRequired = bool.Parse(lines[i][4]);
                await surveyQuestionRepository.Update(question);
                for(int j = 5; j < lines[i].Length; j += 2)
                {
                    var option = await optionRepository.GetItem(lines[i][j]);
                    option.Text = lines[i][j + 1];
                    await optionRepository.Update(option);
                }
            }
            return Json(new { data = 0 });
        }

        /*// POST: Admin/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }*/

        /* // GET: Admin/Edit/5
         public ActionResult Edit(int id)
         {
             return View();
         }

         // POST: Admin/Edit/5
         [HttpPost]
         [ValidateAntiForgeryToken]
         public ActionResult Edit(int id, IFormCollection collection)
         {
             try
             {
                 // TODO: Add update logic here

                 return RedirectToAction(nameof(Index));
             }
             catch
             {
                 return View();
             }
         }*/

        // GET: Admin/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Admin/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
    public class TestLines
    {
        public string[] lines { get; set; }
    }
}