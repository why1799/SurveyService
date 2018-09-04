using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SurveyService.Models
{
    public class UserAnswer
    {
        [Key]
        public string Id { get; set; }

        [ForeignKey(nameof(User))]
        public string UserId { get; set; }
        public User User { get; set; }

        [ForeignKey(nameof(SurveyQuestion))]
        public string QuestionId { get; set; }
        public SurveyQuestion SurveyQuestion { get; set; }

        public string OwnAnswerText { get; set; }
        public ICollection<OptionsForAnswer> OptionsForAnswers { get; set; }
    }
}
