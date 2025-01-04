using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
        Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate);
        Task<IEnumerable<T>> SearchAsync(Expression<Func<T, bool>> predicate);
        Task<IEnumerable<T>> SearchAsync(Expression<Func<T, bool>> predicate, string sortColumn);
        Task<IEnumerable<T>> SearchAsync(Expression<Func<T, bool>> predicate, string sortColumn, string sortOrder);
    }
}
