using SurveyService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SurveyService.WebUI.Models
{
    public class SurveysModel
    {
        public int pages { get; set; }
        public int page { get; set; }
        public List<Survey> surveys { get; set; }
    }
}
