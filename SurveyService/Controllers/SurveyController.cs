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
        ISurveyQuestionRepository surveyQuestion;
        IQuestionRepository question;
        IOptionsForQuestionRepository optionsForQuestion;
        IOptionRepository option;

        public SurveyController(ISurveyRepository survey, ISurveyQuestionRepository surveyQuestion, IQuestionRepository question,
        IOptionsForQuestionRepository optionsForQuestion, IOptionRepository option)
        {
            this.survey = survey;
            this.surveyQuestion = surveyQuestion;
            this.question = question;
            this.optionsForQuestion = optionsForQuestion;
            this.option = option;
        }
        //GET: /Survey/Index?id=SurveyId
        public IActionResult Index(int? id)
        {
            var sur = survey.GetItems().Include(ob => ob.SurveyQuestion).Include(ob => ob.SurveyQuestion.First().Question).Where(ob => ob.Id == id.ToString()).FirstOrDefault();
            //var surq = surveyQuestion.GetItems().Include(ob => ob.Question).Include(ob => ob.Survey).Where(ob => ob.SurveyId == id.ToString()).ToList();
            return View();
        }
    }
}