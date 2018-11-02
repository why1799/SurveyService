using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SurveyService.DAL.Abstract;

namespace SurveyService.WebUI.Controllers
{
    public class DisplaySurveyController : Controller
    {
        ISurveyRepository survey;
        IUserAnswerRepository userAnswer;
        IUserRepository userRepository;

        public DisplaySurveyController(ISurveyRepository survey,
        IUserAnswerRepository userAnswer,
        IUserRepository userRepository)
        {
            this.survey = survey;
            this.userAnswer = userAnswer;
            this.userRepository = userRepository;
        }
        //GET: /DisplaySurvey/Index?id=SurveyId
        //[Authorize(Policy = "Admin")]
        public IActionResult Index(string id, bool anew = false)
        {
            var user = Helper.UserHelper.GetUser(HttpContext, userRepository);
            
            if(userAnswer.GetItems().Include(ob => ob.SurveyQuestion).Any(ob => ob.SurveyQuestion.SurveyId == id && ob.UserId == user.Id) && anew == false) // Проверка на прохождееие опроса
            {
                return RedirectToAction("SurveyCompleted", new { @surveyId = id });
            }
            SurveyService.Models.Survey model;
            // Не очень красивый кусок кода с выборкой из базы ( узнать как сделать лучше) 
            if (anew)
            {
                 model = survey.GetItems()
                    .Include(ob => ob.SurveyQuestion)
                        .ThenInclude(ob => ob.UserAnswers)
                            .ThenInclude(ob => ob.OptionsForAnswers)
                    .Include(ob => ob.SurveyQuestion)
                        .ThenInclude(ob => ob.Options)
                    .SingleOrDefault(ob => ob.Id == id);
                if (model != null) // TODO Отрефакторить
                {
                    foreach (var item in model?.SurveyQuestion)
                    { // Узнать как сделать выборку правильно!
                        item.UserAnswers = item.UserAnswers.Where(ob => ob.UserId == user.Id).ToList();
                    }
                }
            }
            else
            {
                 model = survey.GetItems()
                    .Include(ob => ob.SurveyQuestion)
                        .ThenInclude(ob => ob.Options)
                    .SingleOrDefault(ob => ob.Id == id);
            }
            // конец некрасивого куска кода

            if(model == null) // Не найден опрос
            {
                Response.StatusCode = 500;
                return View();
                // TODO redirect
                return RedirectToAction("Error");
            }
            // сортируем все ( тоже узнать как правильно) это поидее надо загнать в блок выборки, чтобы из базы срау все приходило уже отсортированное
            model.SurveyQuestion = model.SurveyQuestion.OrderBy(ob => ob.Order).ToList();
            foreach (var item in model.SurveyQuestion)
            {
                item.Options = item.Options.OrderBy(ob => ob.Order).ToList();
            }

            ViewBag.anew = anew; // повторное прохождение опроса или нет
            ViewBag.userId = user.Id;
            return View(model);
        }
        /// <summary>
        /// Сохранение результатов со страницы опроса
        /// </summary>
        /// <param name="data">данные</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> SaveResult([FromBody]Newtonsoft.Json.Linq.JObject data)
        {

            if (data == null)
            {
                return Json(new { ok = false, newUrl = "/Error" }); // редиректнули на страниц "Опрос пройден";
            }
            var user = Helper.UserHelper.GetUser(HttpContext, userRepository);
            string surveyId = data.GetValue("surveyId").ToString();
            if(data.GetValue("anew").ToString() == true.ToString()) // проверяем, это первое прохождение опроса или нет
            {
                userAnswer.DeleteRange(userAnswer.GetItems().Where(ob => ob.UserId == user.Id).ToList()).Wait();
            }
            List<SurveyService.Models.UserAnswer> answersList = new List<SurveyService.Models.UserAnswer>(); 
            foreach (var item in data.GetValue("data"))
            {
                var answer = answersList.Where(ob => ob.QuestionId == item.First["questionId"].ToString()).FirstOrDefault();
                if (answer == null)
                {
                    answer = new SurveyService.Models.UserAnswer() { QuestionId = item.First["questionId"].ToString(), UserId = user.Id, OptionsForAnswers = new List<SurveyService.Models.OptionsForAnswer>() };
                    answersList.Add(answer);
                }
                if (item.First["type"].ToString() == "custom") // кастомный ответ (пользователь вводит текст в поле ответа)
                {
                    answer.OwnAnswerText = item.First["text"].ToString();
                }
                else // стандартный ответ (выбран из списка ответов)
                {
                    answer.OptionsForAnswers.Add(new SurveyService.Models.OptionsForAnswer() { OptionId = item.First["optionId"].ToString() });
                }
            }
            await userAnswer.CreateRange(answersList);
            return Json(new { ok = true, newUrl = Url.Action("SurveyCompleted", new { @surveyId = surveyId }) }); // редиректнули на страниц "Опрос пройден"
        }
        /// <summary>
        /// Отображение страницы: "Опрос пройден"
        /// </summary>
        /// <param name="surveyId">ид опроса</param>
        /// <returns></returns>
        public IActionResult SurveyCompleted(string surveyId)
        {
            ViewBag.surveyId = surveyId;
            return View();
        }
    }
}