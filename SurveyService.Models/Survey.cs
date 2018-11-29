using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        public bool IsActive { get; set; }

        [ForeignKey(nameof(CreatedBy))]
        public string CreatedById { get; set; }
        public User CreatedBy { get; set; }

        public ICollection<SurveyQuestion> SurveyQuestion { get; set; }
        public byte[] Image { get; set; }
        public byte[] AddedImage { get; set; }
    }
}
