using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SurveyService.DAL.Abstract;

namespace SurveyService.WebUI.Controllers
{
    public class SurveyController : Controller
    {
        ISurveyRepository survey;
        IAnswerRepository answer;
        IOptionRepository option;
        IOptionsForQuestionRepository optionsForQuestion;

        public SurveyController(ISurveyRepository survey, IAnswerRepository answer, ISurveyQuestionRepository surveyQuestion, 
            IOptionRepository option, IOptionsForQuestionRepository optionsForQuestion)
        {
            this.survey = survey;
            this.answer = answer;
            this.option = option;
            this.optionsForQuestion = optionsForQuestion;
        }
        //GET: /Survey/Index?id=SurveyId
        public IActionResult Index(int? id)
        {
            var model = survey.GetItems()
                    .Include(ob => ob.SurveyQuestion)
                        .ThenInclude(ob => ob.Question)
                            .ThenInclude(ob => ob.OptionsForQuestions)
                                .ThenInclude(ob => ob.Option)
                    .Where(ob => ob.Id == id.ToString()).FirstOrDefault();
            model.SurveyQuestion = model.SurveyQuestion.OrderBy(ob => ob.Order).ToList();
            foreach (var item in model.SurveyQuestion)
            {
                item.Question.OptionsForQuestions = item.Question.OptionsForQuestions.OrderBy(ob => ob.Order).ToList();
            }
            return View(model);
        }
        [HttpPost]
        public IActionResult SaveResult([FromBody]Newtonsoft.Json.Linq.JObject data)
        {
            foreach (var item in data)
            {
                string userId = item.Value["userId"].ToString();
                if (item.Value["type"].ToString() == "custom")
                {
                //    answer.Create(new SurveyService.Models.Answer() {
                //        OptionsForQuestion = new SurveyService.Models.OptionsForQuestion()
                //        {
                //            Option = new SurveyService.Models.Option() { Text = item.Value["text"].ToString() },
                //            Order = 999,
                //            QuestionId = item.Value["questionId"].ToString()
                //        },
                //        UserId = userId
                //    }).Wait();
                }
                else
                {
                    //optionId = item.Value["optionId"].ToString();
                    answer.Create(new SurveyService.Models.Answer() { SelectedOptionId = item.Value["optionId"].ToString(), UserId = userId }).Wait();
                }
            }

            return Json(new { ok = true, newUrl = Url.Action("SurveyCompleted") });
        }
        public IActionResult SurveyCompleted()
        {
            return View();
        }
    }
}