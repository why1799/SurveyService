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

        public AdminController(IUserRepository userRepository, ISurveyRepository surveyRepository, IQuestionRepository questionRepository, ISurveyQuestionRepository surveyQuestionRepository)
        {
            this.userRepository = userRepository;
            this.surveyRepository = surveyRepository;
            this.questionRepository = questionRepository;
            this.surveyQuestionRepository = surveyQuestionRepository;
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



            return View(await surveyRepository.GetItem(id));
        }

        public async   Task<JsonResult> AddQuestion(string surveyid)
        {
            /*var waitingTask0 = surveyRepository.GetItem(surveyid);
            waitingTask0.Wait();
            var survey = waitingTask0.Result;*/


            var question = await questionRepository.Create(new Question { Text = "Новый вопрос", Type = 0 });
            var surveyquestion = await surveyQuestionRepository.Create(new SurveyQuestion { SurveyId = surveyid, QuestionId = question.Id, IsRequired = false, Order = 0/*Тут сделать нормальную нумерацию*/});
            //var surveyquestion = waitingTask2.Result;//;.  .Where(ob => ob.SurveyId == surveyid).ToList();

            //var sth = surveyquestion.Survey.Id;

            return new JsonResult(surveyquestion);
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