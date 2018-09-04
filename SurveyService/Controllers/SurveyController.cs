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
        ISurveyQuestionRepository surveyQuestion;

        public SurveyController(ISurveyRepository survey, IAnswerRepository answer, ISurveyQuestionRepository surveyQuestion)
        {
            this.survey = survey;
            this.answer = answer;
            this.surveyQuestion = surveyQuestion;
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
            return View(model);
        }
        [HttpPost]
        public void SetResult([FromBody]Newtonsoft.Json.Linq.JObject data)
        {
            foreach (var item in data)
            {
                
                string userId = item.Value["userId"].ToString();
                string optionId = item.Value["optionId"].ToString();
                answer.Create(new SurveyService.Models.Answer() { SelectedOptionId = optionId, UserId = userId }).Wait();
                // TODO добавить редирект 
            }
        }
    }
}