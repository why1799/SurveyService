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
        private IQuestionRepository questionRepository;
        private ISurveyQuestionRepository surveyQuestionRepository;
        private IOptionRepository optionRepository;
        private IOptionsForQuestionRepository optionsForQuestionRepository;

        public AdminController(IUserRepository userRepository,
            ISurveyRepository surveyRepository,
            IQuestionRepository questionRepository,
            ISurveyQuestionRepository surveyQuestionRepository,
            IOptionRepository optionRepository,
            IOptionsForQuestionRepository optionsForQuestionRepository)
        {
            this.userRepository = userRepository;
            this.surveyRepository = surveyRepository;
            this.questionRepository = questionRepository;
            this.surveyQuestionRepository = surveyQuestionRepository;
            this.optionRepository = optionRepository;
            this.optionsForQuestionRepository = optionsForQuestionRepository;
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
            //var survey = await surveyRepository.Create(new Models.Survey { CreatedBy = user.Id, Title="Новый опрос", DateCreated = DateTime.Now });

            return RedirectToAction("Edit", new { id = "ee76b75c-f8e5-480c-8686-5f319e4d29dd"/*survey.Id*/ });
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
                        .ThenInclude(ob => ob.Question)
                            .ThenInclude(ob => ob.OptionsForQuestions)
                                .ThenInclude(ob => ob.Option)
                    .Where(ob => ob.Id == id).FirstOrDefault();



            return View(sur);
        }

        public async Task<JsonResult> AddQuestion(string surveyid)
        {
            var question = await questionRepository.Create(new Question { Text = "Новый вопрос", Type = 1 });
            int order = surveyQuestionRepository.GetItems().Where(x => x.SurveyId == surveyid).Count();
            var surveyquestion = await surveyQuestionRepository.Create(new SurveyQuestion { QuestionId = question.Id, IsRequired = false, Order = order, SurveyId = surveyid});
             return new JsonResult(surveyquestion);
        }

        public async Task<JsonResult> RemoveQuestion(string surveyquestionid)
        {
            var surveyquestion = surveyQuestionRepository.GetItems()
                .Include(x => x.Question)
                .ThenInclude(x => x.OptionsForQuestions)
                .ThenInclude(x => x.Option).FirstOrDefault(x => x.Id == surveyquestionid);

            string surveyid = surveyquestion.SurveyId;

            var question = surveyquestion.Question;
            var optionsforquestions = question.OptionsForQuestions;

            await surveyQuestionRepository.Delete(surveyquestion);
            await questionRepository.Delete(question);
            foreach(var optionsforquestion in optionsforquestions)
            {
                var option = optionsforquestion.Option;
                await optionRepository.Delete(option);
                await optionsForQuestionRepository.Delete(optionsforquestion);
            }
            
            var surveyquestions = surveyQuestionRepository.GetItems().Where(x => x.SurveyId == surveyid && x.Order > surveyquestion.Order).ToList();

            foreach(var surveyques in surveyquestions)
            {
                surveyques.Order--;
                await surveyQuestionRepository.Update(surveyques);
            }

            return new JsonResult(surveyquestionid);
        }

        public async Task<JsonResult> AddOption(string questionid)
        {
            var option = await optionRepository.Create(new Option {Text = "Вариант ответа" });
            await optionsForQuestionRepository.Create(new OptionsForQuestion {OptionId = option.Id, QuestionId = questionid });
            var surveyquestion = questionRepository.GetItems().Include(x => x.SurveyQuestion).FirstOrDefault(x => x.Id == questionid).SurveyQuestion.FirstOrDefault(x => x.QuestionId == questionid);
            return Json(new { option = new Option { Id = option.Id, Text = option.Text }, surveyquestionid = surveyquestion.Id});
        }

        public async Task<JsonResult> RemoveOption(string optionid)
        {

            var option = optionRepository.GetItems().Include(x => x.OptionsForQuestions).FirstOrDefault(x => x.Id == optionid);
            var optionsforquestions = option.OptionsForQuestions;
            await optionRepository.Delete(option);
            foreach (var optionsforquestion in optionsforquestions)
            {
                await optionsForQuestionRepository.Delete(optionsforquestion);
            }

            return new JsonResult(optionid);
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
}