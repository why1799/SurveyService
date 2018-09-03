﻿using Microsoft.EntityFrameworkCore;
using SurveyService.DAL.Abstract;
using SurveyService.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

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
                .Include(x => x.Question)
                .Include(x => x.Survey)
                .FirstOrDefaultAsync(x => x.Id == result.Entity.Id);
        }

        public async Task Delete(SurveyQuestion item)
        {
            context.SurveyQuestions.Remove(item);
            await context.SaveChangesAsync();
        }

        public async Task<SurveyQuestion> GetItem(string id)
        {
            var result = await context.SurveyQuestions.FindAsync(id);
            return result;
        }

        public IEnumerable<SurveyQuestion> GetItems()
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