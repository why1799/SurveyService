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
    public class SurveyController : Controller
    {
        ISurveyRepository survey;
        IUserAnswerRepository userAnswer;
        IUserRepository userRepository;

        public SurveyController(ISurveyRepository survey,
        IUserAnswerRepository userAnswer,
        IUserRepository userRepository)
        {
            this.survey = survey;
            this.userAnswer = userAnswer;
            this.userRepository = userRepository;
        }
        //GET: /Survey/Index?id=SurveyId
        [Authorize(Roles = "admin")]
        public IActionResult Index(string id, bool anew = false)
        {
            var user = userRepository.GetItems().SingleOrDefault(x => x.Login == Helper.UserHelper.GetUser(HttpContext).Login);
            if(userAnswer.GetItems().Include(ob => ob.SurveyQuestion).Any(ob => ob.SurveyQuestion.SurveyId == id && ob.UserId == user.Id) && anew == false) // Проверка на прохождееие опроса
            {
                return RedirectToAction("SurveyCompleted", new { @surveyId = id });
            }
            Models.Survey model;
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
                foreach (var item in model.SurveyQuestion)
                { // Узнать как сделать выборку правильно!
                    item.UserAnswers = item.UserAnswers.Where(ob => ob.UserId == user.Id).ToList();
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
        public IActionResult SaveResult([FromBody]Newtonsoft.Json.Linq.JObject data)
        {
            var user = userRepository.GetItems().SingleOrDefault(x => x.Login == Helper.UserHelper.GetUser(HttpContext).Login);
            if (user == null)
            {
                user = Helper.UserHelper.GetUser(HttpContext);
                userRepository.Create(user).Wait();
            }
            string surveyId = data.GetValue("surveyId").ToString();
            if(data.GetValue("anew").ToString() == true.ToString()) // проверяем, это первое прохождение опроса или нет
            {
                userAnswer.DeleteRange(userAnswer.GetItems().Where(ob => ob.UserId == user.Id).ToList()).Wait();
            }
            List<Models.UserAnswer> answersList = new List<Models.UserAnswer>(); 
            foreach (var item in data.GetValue("data"))
            {
                var answer = answersList.Where(ob => ob.QuestionId == item.First["questionId"].ToString()).FirstOrDefault();
                if (answer == null)
                {
                    answer = new Models.UserAnswer() { QuestionId = item.First["questionId"].ToString(), UserId = user.Id, OptionsForAnswers = new List<Models.OptionsForAnswer>() };
                    answersList.Add(answer);
                }
                if (item.First["type"].ToString() == "custom") // кастомный ответ (пользователь вводит текст в поле ответа)
                {
                    answer.OwnAnswerText = item.First["text"].ToString();
                }
                else // стандартный ответ (выбран из списка ответов)
                {
                    answer.OptionsForAnswers.Add(new Models.OptionsForAnswer() { OptionId = item.First["optionId"].ToString() });
                }
            }
            userAnswer.CreateRange(answersList);
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