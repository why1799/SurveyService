using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SurveyService.Models
{
    public class Answer
    {
        [Key]
        public string Id { get; set; }
        [ForeignKey(nameof(User))]
        public string UserId { get; set; }

        [ForeignKey(nameof(OptionsForQuestion))]
        public string SelectedOptionId { get; set; }

        public User User { get; set; }
        public OptionsForQuestion OptionsForQuestion { get;set;}
    }
}
