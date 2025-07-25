﻿using System;
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
        Task<T?> GetByIdAsync(int id, params Expression<Func<T, object>>[] includeProperties);
        Task<IEnumerable<T>> GetListAsync();
        Task<int> CountAsync(Expression<Func<T, bool>> predicate);
        Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate);
        Task<IEnumerable<T>> SearchAsync(Expression<Func<T, bool>> predicate);
        Task<IEnumerable<T>> SearchAsync(Expression<Func<T, bool>> predicate, int pageIndex, int pageSize);
        Task<IEnumerable<T>> SearchAsync(Expression<Func<T, bool>> predicate, int pageIndex, int pageSize, string sortColumn);
        Task<IEnumerable<T>> SearchAsync(Expression<Func<T, bool>> predicate, int pageIndex, int pageSize, string sortColumn, string sortOrder);
        Task<IEnumerable<T>> SearchAsync(Expression<Func<T, bool>> predicate, int pageIndex, int pageSize, string sortColumn, string sortOrder, params Expression<Func<T, object>>[]? includeProperties);
    }
}
