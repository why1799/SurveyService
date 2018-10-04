using SurveyService.DAL.Abstract;
using SurveyService.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace SurveyService.DAL.Concrete
{
    public class SurveyServiceOptionsForAnswerRepository : IOptionsForAnswerRepository
    {
        private SurveyServiceDbContext context;
        public SurveyServiceOptionsForAnswerRepository(SurveyServiceDbContext context)
        {
            this.context = context;
        }
        public async Task<OptionsForAnswer> Create(OptionsForAnswer item)
        {
            var result = await context.OptionsForAnswers.AddAsync(item);
            await context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task CreateRange(ICollection<OptionsForAnswer> item)
        {
            await context.OptionsForAnswers.AddRangeAsync(item);
            await context.SaveChangesAsync();
        }

        public async Task Delete(OptionsForAnswer item)
        {
            context.OptionsForAnswers.Remove(item);
            await context.SaveChangesAsync();
        }

        public async Task DeleteRange(ICollection<OptionsForAnswer> item)
        {
            context.OptionsForAnswers.RemoveRange(item);
            await context.SaveChangesAsync();
        }

        public async Task<OptionsForAnswer> GetItem(string id)
        {
            var result = await context.OptionsForAnswers.FindAsync(id);
            return result;
        }

        public IQueryable<OptionsForAnswer> GetItems()
        {
            return context.OptionsForAnswers;
        }

        public async Task<OptionsForAnswer> Update(OptionsForAnswer item)
        {
            var result = context.OptionsForAnswers.Update(item);
            await context.SaveChangesAsync();
            return result.Entity;
        }
    }
}
