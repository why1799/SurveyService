using SurveyService.DAL.Abstract;
using SurveyService.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace SurveyService.DAL.Concrete
{
    public class SurveyServiceQuestionRepository : IQuestionRepository
    {
        private SurveyServiceDbContext context;
        public SurveyServiceQuestionRepository(SurveyServiceDbContext context)
        {
            this.context = context;
        }
        public async Task<Question> Create(Question item)
        {
            var result = await context.Questions.AddAsync(item);
            await context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task Delete(Question item)
        {
            context.Questions.Remove(item);
            await context.SaveChangesAsync();
        }

        public async Task<Question> GetItem(string id)
        {
            var result = await context.Questions.FindAsync(id);
            return result;
        }

        public IQueryable<Question> GetItems()
        {
            return context.Questions;
        }

        public async Task<Question> Update(Question item)
        {
            var result = context.Questions.Update(item);
            await context.SaveChangesAsync();
            return result.Entity;
        }
    }
}
