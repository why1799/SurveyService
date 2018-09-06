using Microsoft.EntityFrameworkCore;
using SurveyService.Models;

namespace SurveyService.DAL
{
    public class SurveyServiceDbContext : DbContext
    {
        public SurveyServiceDbContext(DbContextOptions<SurveyServiceDbContext> options) : base(options) {}
        public DbSet<Option> Options { get; set; }
        public DbSet<OptionsForAnswer> OptionsForAnswers { get; set; }
        public DbSet<UserAnswer> UserAnswers { get; set; }
        public DbSet<Survey> Surveys { get; set; }
        public DbSet<SurveyQuestion> SurveyQuestions { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<OptionsForAnswer>().HasOne(x => x.Option).WithMany(x => x.OptionsForQuestions).HasForeignKey(x => x.OptionId);
            //modelBuilder.Entity<OptionsForAnswer>().HasOne(x => x.Question).WithMany(x => x.OptionsForQuestions).HasForeignKey(x => x.QuestionId);
            //modelBuilder.Entity<SurveyQuestion>().HasOne(x => x.Question).WithMany(x => x.SurveyQuestion).HasForeignKey(x => x.QuestionId);
            //modelBuilder.Entity<SurveyQuestion>().HasOne(x => x.Survey).WithMany(x => x.SurveyQuestion).HasForeignKey(x => x.SurveyId);
        }
    }
}