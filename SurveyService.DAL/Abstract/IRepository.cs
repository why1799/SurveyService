using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace SurveyService.DAL.Abstract
{
    public interface IRepository<T>
    {
        Task<T> Create(T item);
        void CreateRange(ICollection<T> item);
        Task Delete(T item);
        Task DeleteRange(ICollection<T> item);
        Task<T> Update(T item);
        Task<T> GetItem(string id);
        IQueryable<T> GetItems();
    }
}
