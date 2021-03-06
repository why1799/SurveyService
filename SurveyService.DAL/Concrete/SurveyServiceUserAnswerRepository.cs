﻿using SurveyService.DAL.Abstract;
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
            var result = await context.UserAnswers.AddAsync(item);
            await context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task CreateRange(ICollection<UserAnswer> item)
        {
            await context.UserAnswers.AddRangeAsync(item);
            await context.SaveChangesAsync();
        }

        public async Task Delete(UserAnswer item)
        {
            context.UserAnswers.Remove(item);
            await context.SaveChangesAsync();
        }

        public async Task DeleteRange(ICollection<UserAnswer> item)
        {
            foreach (var answer in item)
            {
                context.OptionsForAnswers.RemoveRange(context.OptionsForAnswers.Where(ob => ob.UserAnswerId == answer.Id));
            }
            context.UserAnswers.RemoveRange(item);
            await context.SaveChangesAsync();
        }

        public async Task<UserAnswer> GetItem(string id)
        {
            var result = await context.UserAnswers.FindAsync(id);
            return result;
        }

        public IQueryable<UserAnswer> GetItems()
        {
            return context.UserAnswers;
        }

        public async Task<UserAnswer> Update(UserAnswer item)
        {
            var result = context.UserAnswers.Update(item);
            await context.SaveChangesAsync();
            return result.Entity;
        }
    }
}
