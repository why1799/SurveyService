using SurveyService.DAL.Abstract;
using SurveyService.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace SurveyService.DAL.Concrete
{
    public class SurveyServiceAnswerRepository : IAnswerRepository
    {
        private SurveyServiceDbContext context;
        public SurveyServiceAnswerRepository(SurveyServiceDbContext context)
        {
            this.context = context;
        }

        public async Task<Answer> Create(Answer item)
        {
            var result = await context.Answers.AddAsync(item);
            await context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task Delete(Answer item)
        {
            context.Answers.Remove(item);
            await context.SaveChangesAsync();
        }

        public async Task<Answer> GetItem(string id)
        {
            var result = await context.Answers.FindAsync(id);
            return result;
        }

        public IEnumerable<Answer> GetItems()
        {
            return context.Answers;
        }

        public async Task<Answer> Update(Answer item)
        {
            var result = context.Answers.Update(item);
            await context.SaveChangesAsync();
            return result.Entity;
        }
    }
}
