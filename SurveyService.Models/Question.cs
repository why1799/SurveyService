using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SurveyService.Models
{
    public class Question
    {
        [Key]
        public string Id { get; set; }
        public string Text { get; set; }
        public int Type { get; set; }
        public ICollection<OptionsForQuestion> OptionsForQuestions { get; set; }
    }
}
