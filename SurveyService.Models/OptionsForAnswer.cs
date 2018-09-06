using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SurveyService.Models
{
    public class OptionsForAnswer
    {
        [Key]
        public string Id { get; set; }

        [ForeignKey(nameof(UserAnswer))]
        public string UserAnswerId { get; set; }
        public UserAnswer UserAnswer { get; set; }

        [ForeignKey(nameof(Option))]
        public string OptionId { get; set; }
        public Option Option { get; set; }
    }
}
