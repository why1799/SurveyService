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
using Microsoft.AspNetCore.Authorization;
using System.Text;

namespace SurveyService.WebUI.Controllers
{
    public class AdminController : Controller
    {
        private IUserRepository userRepository;
        private ISurveyRepository surveyRepository;
        private ISurveyQuestionRepository surveyQuestionRepository;
        private IOptionRepository optionRepository;
        private IOptionsForAnswerRepository optionsForAnswerRepository;
        private IUserAnswerRepository userAnswerRepository;

        public AdminController(IUserRepository userRepository,
            ISurveyRepository surveyRepository,
            ISurveyQuestionRepository surveyQuestionRepository,
            IOptionRepository optionRepository,
            IOptionsForAnswerRepository optionsForAnswerRepository,
            IUserAnswerRepository userAnswerRepository)
        {
            this.userRepository = userRepository;
            this.surveyRepository = surveyRepository;
            this.surveyQuestionRepository = surveyQuestionRepository;
            this.optionRepository = optionRepository;
            this.optionsForAnswerRepository = optionsForAnswerRepository;
            this.userAnswerRepository = userAnswerRepository;
        }

        [Authorize(Policy = "Admin")]
        public ActionResult Index()
        {
            var surveys = surveyRepository.GetItems().Include(x => x.CreatedBy).OrderByDescending(x => x.DateCreated).ToList();
            return View(surveys);
        }


        // GET: Admin/Create
        [Authorize(Policy = "Admin")]
        public async Task<ActionResult> Create()
        {
            /*var user = UserHelper.GetUser(HttpContext, userRepository);

            var survey = await surveyRepository.Create(new Survey { CreatedById = user.Id, Title="Новый опрос", DateCreated = DateTime.UtcNow });
            return RedirectToAction("Edit", new { id = survey.Id });*/
            return View();
        }

        public async Task<JsonResult> RemoveSurvey(string id)
        {
            var survey = surveyRepository.GetItems().Include(x => x.SurveyQuestion).ThenInclude(y => y.Options).ThenInclude(z => z.OptionsForAnswers).Include(x => x.SurveyQuestion).ThenInclude(a => a.UserAnswers).FirstOrDefault(x => x.Id == id);
            foreach (var question in survey.SurveyQuestion.ToList())
            {
                foreach (var option in question.Options.ToList())
                {
                    foreach (var optionsforanswer in option.OptionsForAnswers.ToList())
                    {
                        await optionsForAnswerRepository.Delete(optionsforanswer);
                    }
                    await optionRepository.Delete(option);
                }
                foreach (var useranswers in question.UserAnswers.ToList())
                {
                    await userAnswerRepository.Delete(useranswers);
                }
                await surveyQuestionRepository.Delete(question);
            }
            await surveyRepository.Delete(survey);
            return new JsonResult(id);
        }

        [Authorize(Policy = "Admin")]
        public async Task<ActionResult> Edit(string id)
        {
           // var user = UserHelper.GetUser(HttpContext, userRepository);

            var sur = surveyRepository.GetItems()
                    .Include(ob => ob.SurveyQuestion)
                                .ThenInclude(ob => ob.Options)
                    .Where(ob => ob.Id == id).FirstOrDefault();



            return View(sur);
        }

        public async Task<JsonResult> AddQuestion(string surveyid)
        {
            int order = surveyQuestionRepository.GetItems().Where(x => x.SurveyId == surveyid).Count();
            var question = await surveyQuestionRepository.Create(new SurveyQuestion { QuestionText = "Новый вопрос", HasOwnAnswer = false, IsRequired = false, SurveyId = surveyid, Type = 0, Order = order });
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
            foreach (var curquestion in questions)
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
            var option = await optionRepository.Create(new Option { Text = "Вариант ответа", Order = order, QuestionId = surveyquestionid });
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


            return new JsonResult(new
            {
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

        public async Task<JsonResult> GotAnswers(string id)
        {
            var survey = surveyRepository.GetItems().Include(x => x.SurveyQuestion).ThenInclude(a => a.UserAnswers).FirstOrDefault(x => x.Id == id);
            bool got = false;
            foreach(var question in survey.SurveyQuestion)
            {
                if(question.UserAnswers.Count > 0)
                {
                    got = true;
                    break;
                }
            }
            return Json( got );
        }

        [HttpPost]
        public async Task<JsonResult> Save(string id, string title, string description, string[][] lines, string image)
        {
            Survey survey;
            byte[] dataimage = Convert.FromBase64String(image);
            if (id == null)
            {
                var user = UserHelper.GetUser(HttpContext, userRepository);
                survey = await surveyRepository.Create(new Survey { Title = title, Description = description, DateCreated = DateTime.UtcNow, CreatedById = user.Id, Image = dataimage });
                id = survey.Id;
            }
            else
            {
                survey = surveyRepository.GetItems().Include(x => x.SurveyQuestion).FirstOrDefault(x => x.Id == id);
                survey.Title = title;
                survey.Description = description;
                survey.Image = dataimage;
                await surveyRepository.Update(survey);
                List<SurveyQuestion> questions = new List<SurveyQuestion>();
                foreach(var question in survey.SurveyQuestion)
                {
                    bool found = false;
                    for(int i = 0; !found && i < lines.Length; i++)
                    {
                        if(lines[i][0] == question.Id)
                        {
                            found = true;
                        }
                    }
                    if(!found)
                    {
                        questions.Add(question);
                    }
                }

                foreach(var question in questions)
                {
                    await RemoveQuestion(question.Id);
                }
            }

            for (int i = 0; i < lines.Length; i++)
            {
                SurveyQuestion question;
                if (lines[i][0].IndexOf("newquest") == 0)
                {
                    question = await surveyQuestionRepository.Create(new SurveyQuestion
                    {
                        SurveyId = id,
                        QuestionText = lines[i][1],
                        Type = int.Parse(lines[i][2]),
                        HasOwnAnswer = bool.Parse(lines[i][3]),
                        IsRequired = bool.Parse(lines[i][4]),
                        Order = i
                    });
                }
                else
                {
                    question = await surveyQuestionRepository.GetItem(lines[i][0]);
                    question.QuestionText = lines[i][1];
                    question.Type = int.Parse(lines[i][2]);
                    question.HasOwnAnswer = bool.Parse(lines[i][3]);
                    question.IsRequired = bool.Parse(lines[i][4]);
                    question.Order = i;
                    await surveyQuestionRepository.Update(question);
                }

                List<Option> options = new List<Option>();

                foreach(var option in optionRepository.GetItems().Where(x => x.QuestionId == question.Id))
                {
                    bool found = false;
                    for (int j = 5; !found && j < lines[i].Length; j += 2)
                    {
                        if (lines[i][j] == option.Id)
                        {
                            found = true;
                        }
                    }
                    if (!found)
                    {
                        options.Add(option);
                    }
                }

                foreach (var option in options)
                {
                    await optionRepository.Delete(option);
                }

                for (int j = 5, ord = 0; j < lines[i].Length; j += 2, ord++)
                {
                    if (lines[i][j].IndexOf("newopt") == 0)
                    {
                        await optionRepository.Create(new Option { QuestionId = question.Id, Text = lines[i][j + 1], Order = ord });
                    }
                    else
                    {
                        var option = await optionRepository.GetItem(lines[i][j]);
                        option.Text = lines[i][j + 1];
                        option.Order = ord;
                        await optionRepository.Update(option);
                    }
                }
            }
            return Json(new { id });
        }

        [Authorize(Policy = "Admin")]
        public ActionResult Surveys(int page = 1, string id = "")
        {
            int surveysperpage = 10;
            //var surveys = surveyRepository.GetItems().ToList();
            var surveys = surveyRepository.GetItems().Include(x => x.CreatedBy).OrderBy(x => x.DateCreated).ToList();
            surveys.Reverse();
            if(id != "")
            {
                int index = surveys.FindIndex(x => x.Id == id) + 1;
                page = (int)Math.Ceiling(index / (double)surveysperpage);
            }
            if (page < 1)
            {
                page = 1;
            }
            int pages = (int)Math.Ceiling(surveys.Count / (double)surveysperpage);
            if(page > pages)
            {
                page = pages;
            }
            return View(new Models.SurveysModel { page = page, pages = pages, surveys = surveys.Skip((page - 1) * surveysperpage).Take(surveysperpage).ToList() });
        }

        
        public async Task<JsonResult> GetResult(string id)
        {
            bool ok = false;
            StringBuilder builder = new StringBuilder();
            var survey = surveyRepository.GetItems().Include(x => x.SurveyQuestion).ThenInclude(y => y.UserAnswers).ThenInclude(z => z.User).Include(x => x.SurveyQuestion).ThenInclude(y => y.Options).ThenInclude(z => z.OptionsForAnswers).FirstOrDefault(x => x.Id == id);
            foreach(var question in survey.SurveyQuestion.OrderBy(x => x.Order))
            {
                if(ok || question.UserAnswers.Count > 0)
                {
                    ok = true;
                }
                foreach(var useranswer in question.UserAnswers)
                {
                    if(useranswer.OptionsForAnswers == null)
                    {
                        builder.AppendLine(String.Format("{0};{1};{2};{3};", question.QuestionText, useranswer.OwnAnswerText, useranswer.User.DisplayName, useranswer.User.Login));
                    }
                    else
                    {
                        foreach(var optionsforanswer in useranswer.OptionsForAnswers)
                        {
                            builder.AppendLine(String.Format("{0};{1};{2};{3};", question.QuestionText, optionsforanswer.Option.Text, useranswer.User.DisplayName, useranswer.User.Login));
                        }
                    }
                }
            }

            if (!ok)
            {
                return Json(null);
            }
            return Json(new { name = survey.Title, data = builder.ToString() });
        }
    }
}