using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SurveyService.Models
{
    public class Option
    {
        [Key]
        public string Id { get; set; }
        public string Text { get; set; }
        public ICollection<OptionsForQuestion> OptionsForQuestions { get; set; }
    }
}
