using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseManager.Api.Repository.Interfaces
{
    public interface IRepository <T> 
        where T : class
    {
        Task<T> CreateAsync(T item);
        Task<bool> CreateRangeAsync(IEnumerable<T> items);
        Task<T> UpdateAsync(T item);
        Task<bool> DeleteAsync(T item);
        Task<bool> DeleteRangeAsync(IEnumerable<T> items);
        Task<T?> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetListAsync();
        T? FirstOrDefault(Func<T, bool> predicate);
        IEnumerable<T> Search(Func<T, bool> predicate);
        IEnumerable<T> Search(Func<T, bool> predicate, string sortColumn);
        IEnumerable<T> Search(Func<T, bool> predicate, string sortColumn, string sortOrder);
    }
}
