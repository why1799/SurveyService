using Microsoft.EntityFrameworkCore;
using SurveyService.Models;

namespace SurveyService.DAL
{
    public class SurveyServiceDbContext : DbContext
    {
        public SurveyServiceDbContext(DbContextOptions<SurveyServiceDbContext> options) : base(options) {}
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Option> Options { get; set; }
        public DbSet<OptionsForQuestion> OptionsForQuestions { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Survey> Surveys { get; set; }
        public DbSet<SurveyQuestion> SurveyQuestions { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OptionsForQuestion>().HasOne(x => x.Option).WithMany(x => x.OptionsForQuestions).HasForeignKey(x => x.OptionId);
            modelBuilder.Entity<OptionsForQuestion>().HasOne(x => x.Question).WithMany(x => x.OptionsForQuestions).HasForeignKey(x => x.QuestionId);
            modelBuilder.Entity<SurveyQuestion>().HasKey(x => new { x.QuestionId, x.SurveyId });
            modelBuilder.Entity<SurveyQuestion>().HasOne(x => x.Question).WithMany(x => x.SurveyQuestion).HasForeignKey(x => x.QuestionId);
            modelBuilder.Entity<SurveyQuestion>().HasOne(x => x.Survey).WithMany(x => x.SurveyQuestion).HasForeignKey(x => x.SurveyId);
        }
    }
}