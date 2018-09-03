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
        Task Delete(T item);
        Task<T> Update(T item);
        Task<T> GetItem(string id);
        IEnumerable<T> GetItems();
    }
}
