using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SurveyService.Models
{
    public class Survey
    {
        [Key]
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string BannerUrl { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateExpires { get; set; }
        public DateTime DateCreated { get; set; }
        public string CreatedBy { get; set; }
        public ICollection<SurveyQuestion> SurveyQuestion { get; set; }

    }
}
