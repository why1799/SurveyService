using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SurveyService.Models
{
    public class Option
    {
        [Key]
        public string Id { get; set; }

        [ForeignKey(nameof(SurveyQuestion))]
        public string QuestionId { get; set; }
        public SurveyQuestion SurveyQuestion { get; set; }

        public int Order { get; set; }
        public string Text { get; set; }
        public ICollection<OptionsForAnswer> OptionsForAnswers { get; set; }
    }
}
