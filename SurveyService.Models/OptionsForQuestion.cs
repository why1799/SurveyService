using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SurveyService.Models
{
    public class OptionsForQuestion
    {
        [Key]
        public string Id { get; set; }
        [ForeignKey(nameof(Question))]
        public string QuestionId { get; set; }
        [ForeignKey(nameof(Option))]
        public string OptionId { get; set; }

        public Question Question { get; set; }
        public Option Option { get; set; }
    }
}
