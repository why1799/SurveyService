using SurveyService.DAL.Abstract;
using SurveyService.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace SurveyService.DAL.Concrete
{
    public class SurveyServiceSurveyRepository : ISurveyRepository
    {
        private SurveyServiceDbContext context;
        public SurveyServiceSurveyRepository(SurveyServiceDbContext context)
        {
            this.context = context;
        }
        public async Task<Survey> Create(Survey item)
        {
            var result = await context.Surveys.AddAsync(item);
            await context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task Delete(Survey item)
        {
            context.Surveys.Remove(item);
            await context.SaveChangesAsync();
        }

        public async Task<Survey> GetItem(string id)
        {
            var result = await context.Surveys.FindAsync(id);
            return result;
        }

        public IQueryable<Survey> GetItems()
        {
            return context.Surveys;
        }

        public async Task<Survey> Update(Survey item)
        {
            var result = context.Surveys.Update(item);
            await context.SaveChangesAsync();
            return result.Entity;
        }
    }
}
