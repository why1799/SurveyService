using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SurveyService.Models
{
    [Table("SurveyQuestion")]
    public class SurveyQuestion
    {
        [Key]
        public string Id { get; set; }
        [ForeignKey(nameof(Survey))]
        public string SurveyId { get; set; }
        public Survey Survey { get; set; }
        [ForeignKey(nameof(Question))]
        public string QuestionId { get; set; }
        public Question Question { get; set; }

        public int Order { get; set; }
        public bool IsRequired { get; set; }
    }
}
