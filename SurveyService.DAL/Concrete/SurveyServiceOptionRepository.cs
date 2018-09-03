using SurveyService.DAL.Abstract;
using SurveyService.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace SurveyService.DAL.Concrete
{
    public class SurveyServiceOptionRepository : IOptionRepository
    {
        private SurveyServiceDbContext context;
        public SurveyServiceOptionRepository(SurveyServiceDbContext context)
        {
            this.context = context;
        }
        public async Task<Option> Create(Option item)
        {
            var result = await context.Options.AddAsync(item);
            await context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task Delete(Option item)
        {
            context.Options.Remove(item);
            await context.SaveChangesAsync();
        }

        public async Task<Option> GetItem(string id)
        {
            var result = await context.Options.FindAsync(id);
            return result;
        }

        public IEnumerable<Option> GetItems()
        {
            return context.Options;
        }

        public async Task<Option> Update(Option item)
        {
            var result = context.Options.Update(item);
            await context.SaveChangesAsync();
            return result.Entity;
        }
    }
}
