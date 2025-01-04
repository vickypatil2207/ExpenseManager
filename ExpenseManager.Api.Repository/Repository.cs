using ExpenseManager.Api.Database;
using ExpenseManager.Api.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseManager.Api.Repository
{
    public class Repository<T> : IRepository<T>
        where T : class
    {
        private readonly ExpenseManagerDBContext _expenseManagerDBContext;
        private readonly DbSet<T> _dbSet;

        public Repository(ExpenseManagerDBContext expenseManagerDBContext)
        {
            _expenseManagerDBContext = expenseManagerDBContext;
            _dbSet = _expenseManagerDBContext.Set<T>();
        }

        public async Task<T> CreateAsync(T item)
        {
            await _expenseManagerDBContext.AddAsync(item);            
            await _expenseManagerDBContext.SaveChangesAsync();
            return item;
        }

        public async Task<bool> CreateRangeAsync(IEnumerable<T> items)
        {
            await _expenseManagerDBContext.AddRangeAsync(items);
            var result = await _expenseManagerDBContext.SaveChangesAsync();
            return result > 0;
        }

        public async Task<T> UpdateAsync(T item)
        {
            _expenseManagerDBContext.Update(item);
            await _expenseManagerDBContext.SaveChangesAsync();
            return item;
        }

        public async Task<bool> DeleteAsync(T item)
        {
            _expenseManagerDBContext.Remove(item);
            var result = await _expenseManagerDBContext.SaveChangesAsync();
            return result > 0;
        }

        public async Task<bool> DeleteRangeAsync(IEnumerable<T> items)
        {
            _expenseManagerDBContext.RemoveRange(items);
            var result = await _expenseManagerDBContext.SaveChangesAsync();
            return result > 0;
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _expenseManagerDBContext.FindAsync<T>(id);
        }

        public async Task<IEnumerable<T>> GetListAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<int> CountAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.Where(predicate).CountAsync();
        }

        public async Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate)
        {
            var list = await SearchAsync(predicate);
            return list.FirstOrDefault();
        }

        public async Task<IEnumerable<T>> SearchAsync(Expression<Func<T, bool>> predicate)
        {
            return await SearchAsync(predicate, 0, 0, string.Empty, string.Empty);
        }

        public async Task<IEnumerable<T>> SearchAsync(Expression<Func<T, bool>> predicate, int pageIndex, int pageSize)
        {
            return await SearchAsync(predicate, pageIndex, pageSize, string.Empty, string.Empty);
        }

        public async Task<IEnumerable<T>> SearchAsync(Expression<Func<T, bool>> predicate, int pageIndex, int pageSize, string sortColumn)
        {
            return await SearchAsync(predicate, pageIndex, pageSize, sortColumn, "asc");
        }

        public async Task<IEnumerable<T>> SearchAsync(Expression<Func<T, bool>> predicate, int pageIndex, int pageSize, string sortColumn, string sortOrder)
        {
            IQueryable<T> query = _dbSet.Where(predicate);

            if (string.IsNullOrWhiteSpace(sortColumn) && pageIndex == 0)
            {
                return await query.ToListAsync();
            }

            if (string.IsNullOrEmpty(sortColumn))
            {
                return await query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
            }

            var parameter = Expression.Parameter(typeof(T), "x");
            var property = Expression.Property(parameter, sortColumn);
            var lambda = Expression.Lambda(property, parameter);

            var typeArgs = new Type[] { typeof(T), property.Type };
            var method = typeof(Queryable).GetMethods()
                .Where(m => m.Name == "OrderBy" && m.IsGenericMethod)
                .First()
                .MakeGenericMethod(typeArgs);

            if (sortOrder.ToLower() == "desc")
            {
                method = typeof(Queryable).GetMethods()
                    .Where(m => m.Name == "OrderByDescending" && m.IsGenericMethod)
                    .First()
                    .MakeGenericMethod(typeArgs);
            }

            var newQuery = (IQueryable<T>?)method.Invoke(null, new object[] { query, lambda });
            if (newQuery == null)
            {
                throw new ApplicationException("Search Query resulted to null, cannot execute query!");
            }

            if (pageIndex > 0)
            {
                newQuery = newQuery.Skip((pageIndex - 1) * pageSize).Take(pageSize);
            }
                        
            return await newQuery.ToListAsync();
        }
    }
}
