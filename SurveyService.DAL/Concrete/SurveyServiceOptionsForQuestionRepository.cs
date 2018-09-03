using SurveyService.DAL.Abstract;
using SurveyService.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace SurveyService.DAL.Concrete
{
    public class SurveyServiceOptionsForQuestionRepository : IOptionsForQuestionRepository
    {
        private SurveyServiceDbContext context;
        public SurveyServiceOptionsForQuestionRepository(SurveyServiceDbContext context)
        {
            this.context = context;
        }
        public async Task<OptionsForQuestion> Create(OptionsForQuestion item)
        {
            var result = await context.OptionsForQuestions.AddAsync(item);
            await context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task Delete(OptionsForQuestion item)
        {
            context.OptionsForQuestions.Remove(item);
            await context.SaveChangesAsync();
        }

        public async Task<OptionsForQuestion> GetItem(string id)
        {
            var result = await context.OptionsForQuestions.FindAsync(id);
            return result;
        }

        public IEnumerable<OptionsForQuestion> GetItems()
        {
            return context.OptionsForQuestions;
        }

        public async Task<OptionsForQuestion> Update(OptionsForQuestion item)
        {
            var result = context.OptionsForQuestions.Update(item);
            await context.SaveChangesAsync();
            return result.Entity;
        }
    }
}
