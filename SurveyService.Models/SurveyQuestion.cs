using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SurveyService.Models
{
    public class SurveyQuestion
    {
        [Key]
        public string Id { get; set; }
        [ForeignKey(nameof(Survey))]
        public string SurveyId { get; set; }
        public Survey Survey { get; set; }

        public string QuestionText { get; set; }
        public int Order { get; set; }
        public bool IsRequired { get; set; }
        /// <summary>
        /// 4 варианта: radio, checkbox, text, rating,
        /// </summary>
        public int Type { get; set; }
        public bool HasOwnAnswer { get; set; }

        public ICollection<UserAnswer> UserAnswers { get; set; }
        public ICollection<Option> Options { get; set; }
    }
}
