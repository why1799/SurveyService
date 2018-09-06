using SurveyService.DAL.Abstract;
using SurveyService.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace SurveyService.DAL.Concrete
{
    public class SurveyServiceUserAnswerRepository : IUserAnswerRepository
    {
        private SurveyServiceDbContext context;
        public SurveyServiceUserAnswerRepository(SurveyServiceDbContext context)
        {
            this.context = context;
        }
        public async Task<UserAnswer> Create(UserAnswer item)
        {
            var result = await context.Questions.AddAsync(item);
            await context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task Delete(UserAnswer item)
        {
            context.Questions.Remove(item);
            await context.SaveChangesAsync();
        }

        public async Task<UserAnswer> GetItem(string id)
        {
            var result = await context.Questions.FindAsync(id);
            return result;
        }

        public IQueryable<UserAnswer> GetItems()
        {
            return context.Questions;
        }

        public async Task<UserAnswer> Update(UserAnswer item)
        {
            var result = context.Questions.Update(item);
            await context.SaveChangesAsync();
            return result.Entity;
        }
    }
}
