using SurveyService.DAL.Abstract;
using SurveyService.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace SurveyService.DAL.Concrete
{
    public class SurveyServiceUserRepository : IUserRepository
    {
        private SurveyServiceDbContext context;
        public SurveyServiceUserRepository(SurveyServiceDbContext context)
        {
            this.context = context;
        }

        public async Task<User> Create(User item)
        {
            var result = await context.Users.AddAsync(item);
            await context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task Delete(User item)
        {
            context.Users.Remove(item);
            await context.SaveChangesAsync();
        }

        public async Task<User> GetItem(string id)
        {
            var result = await context.Users.FindAsync(id);
            return result;
        }

        public IEnumerable<User> GetItems()
        {
            return context.Users;
        }

        public async Task<User> Update(User item)
        {
            var result = context.Users.Update(item);
            await context.SaveChangesAsync();
            return result.Entity;
        }
    }
}
