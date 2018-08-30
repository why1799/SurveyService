using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SurveyService.Models
{
    public class User
    {
        [Key]
        public string Id { get; set; }
        public string Login { get; set; }
        public string DisplayName { get; set; }
    }
}
