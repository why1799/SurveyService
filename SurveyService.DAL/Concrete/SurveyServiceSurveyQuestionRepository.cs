using Microsoft.EntityFrameworkCore;
using SurveyService.DAL.Abstract;
using SurveyService.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace SurveyService.DAL.Concrete
{
    public class SurveyServiceSurveyQuestionRepository : ISurveyQuestionRepository
    {
        private SurveyServiceDbContext context;
        public SurveyServiceSurveyQuestionRepository(SurveyServiceDbContext context)
        {
            this.context = context;
        }
        public async Task<SurveyQuestion> Create(SurveyQuestion item)
        {
            var result = await context.SurveyQuestions.AddAsync(item);
            await context.SaveChangesAsync();
            return await context.SurveyQuestions
                //.Include(x => x.Question)
                .Include(x => x.Survey)
                .FirstOrDefaultAsync(x => x.Id == result.Entity.Id);
        }

        public void CreateRange(ICollection<SurveyQuestion> item)
        {
            context.SurveyQuestions.AddRangeAsync(item).Wait();
            context.SaveChangesAsync().Wait();
        }

        public async Task Delete(SurveyQuestion item)
        {
            context.SurveyQuestions.Remove(item);
            await context.SaveChangesAsync();
        }

        public async Task DeleteRange(ICollection<SurveyQuestion> item)
        {
            context.SurveyQuestions.RemoveRange(item);
            await context.SaveChangesAsync();
        }

        public async Task<SurveyQuestion> GetItem(string id)
        {
            var result = await context.SurveyQuestions.FindAsync(id);
            return result;
        }

        public IQueryable<SurveyQuestion> GetItems()
        {
            return context.SurveyQuestions;
        }

        public async Task<SurveyQuestion> Update(SurveyQuestion item)
        {
            var result = context.SurveyQuestions.Update(item);
            await context.SaveChangesAsync();
            return result.Entity;
        }
    }
}
