using ExpenseManager.Api.Database.Entities;
using ExpenseManager.Api.Repository.Interfaces;
using ExpenseManager.Api.Service.Interfaces;
using ExpenseManager.Shared.Models;
using ExpenseManager.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpenseManager.Api.Service.Mapper;
using Azure.Core;
using System.Linq.Expressions;
using ExpenseManager.Shared.Models.SearchModels;

namespace ExpenseManager.Api.Service
{
    public class ExpenseCategoryService : IExpenseCategoryService
    {
        private readonly IRepository<DefaultExpenseCategory> _defaultExpenseCategoryRepository;
        private readonly IRepository<UserExpenseCategory> _userExpenseCategoryRepository;

        public ExpenseCategoryService(IRepository<DefaultExpenseCategory> defaultExpenseCategoryRepsotiroy,
            IRepository<UserExpenseCategory> userExpenseCategoryRepsotiroy)
        {
            _defaultExpenseCategoryRepository = defaultExpenseCategoryRepsotiroy;
            _userExpenseCategoryRepository = userExpenseCategoryRepsotiroy;
        }

        public async Task<ServiceResult<ExpenseCategoryModel>> CreateCategory(ExpenseCategoryModel expenseCategoryModel)
        {
            var userExpenseCategory = ExpenseCategoryMapper.MapToUserExpenseCategory(expenseCategoryModel);
            var result = await _userExpenseCategoryRepository.CreateAsync(userExpenseCategory);
            if (result.Id > 0)
            {
                var expenseCategoryCreated = await _userExpenseCategoryRepository.GetByIdAsync(result.Id);
                if (expenseCategoryCreated != null)
                {
                    return ServiceResult<ExpenseCategoryModel>.Ok(ExpenseCategoryMapper.MapToExpenseCategoryModel(expenseCategoryCreated));
                }
            }

            return ServiceResult<ExpenseCategoryModel>.Fail("Something went wrong saving Expense category details!", expenseCategoryModel);
        }

        public async Task<bool> CreateCategoriesForUserSignup(int userId)
        {
            var defaultExpenseCategoryList = await _defaultExpenseCategoryRepository.GetListAsync();
            if (defaultExpenseCategoryList.Any())
            {
                var userExpenseCategoryList = defaultExpenseCategoryList.Select(dec => ExpenseCategoryMapper.MapToUserExpenseCategory(userId, dec));
                return await _userExpenseCategoryRepository.CreateRangeAsync(userExpenseCategoryList);
            }

            return false;
        }

        public async Task<ServiceResult<ExpenseCategoryModel>> UpdateCategory(int id, ExpenseCategoryModel expenseCategoryModel)
        {
            var expenseCategoryResult = await _userExpenseCategoryRepository.GetByIdAsync(id);
            if (expenseCategoryResult != null)
            {
                ExpenseCategoryMapper.MapToUserExpenseCategory(expenseCategoryModel, ref expenseCategoryResult);
                await _userExpenseCategoryRepository.UpdateAsync(expenseCategoryResult);

                var expenseCategoryUpdated = await _userExpenseCategoryRepository.GetByIdAsync(expenseCategoryModel.Id);
                if (expenseCategoryUpdated != null)
                {

                    return ServiceResult<ExpenseCategoryModel>.Ok(ExpenseCategoryMapper.MapToExpenseCategoryModel(expenseCategoryUpdated));
                }
            }

            return ServiceResult<ExpenseCategoryModel>.Fail($"Could not find Expense category details for Id = {id}!", expenseCategoryModel);
        }

        public async Task<ServiceResult<ExpenseCategoryModel>> DeleteCategory(int id)
        {
            var expenseCategoryResult = await _userExpenseCategoryRepository.GetByIdAsync(id);
            if (expenseCategoryResult != null)
            {
                await _userExpenseCategoryRepository.DeleteAsync(expenseCategoryResult);
                return ServiceResult<ExpenseCategoryModel>.Ok(ExpenseCategoryMapper.MapToExpenseCategoryModel(expenseCategoryResult));
            }

            return ServiceResult<ExpenseCategoryModel>.Fail($"Could not find Expense category details for Id = {id}!");
        }

        public async Task<ServiceResult<ExpenseCategoryModel>> GetCategoryById(int id)
        {
            var expenseCategoryResult = await _userExpenseCategoryRepository.GetByIdAsync(id);
            if (expenseCategoryResult != null)
            {
                return ServiceResult<ExpenseCategoryModel>.Ok(ExpenseCategoryMapper.MapToExpenseCategoryModel(expenseCategoryResult));
            }

            return ServiceResult<ExpenseCategoryModel>.Fail($"Could not find Expense category details for Id = {id}!");
        }

        public async Task<ServiceResult<PaginatedList<ExpenseCategoryModel>>> GetCategoriesByUserId(int userId, BaseSearchModel baseSearchModel)
        {
            var counts = await _userExpenseCategoryRepository.CountAsync(uec => uec.UserId == userId);
            if (counts > 0)
            {
                var expenseCategoryList = await _userExpenseCategoryRepository.SearchAsync((uec => uec.UserId == userId), 
                                    baseSearchModel.PageIndex, 
                                    baseSearchModel.PageSize, 
                                    baseSearchModel.SortColumn, 
                                    baseSearchModel.SortOrder);
                if (expenseCategoryList.Any())
                {
                    var paginatedList = new PaginatedList<ExpenseCategoryModel>(counts, baseSearchModel);
                    paginatedList.Items = expenseCategoryList.Select(e => ExpenseCategoryMapper.MapToExpenseCategoryModel(e));
                    return ServiceResult<PaginatedList<ExpenseCategoryModel>>.Ok(paginatedList);
                }
            }

            return ServiceResult<PaginatedList<ExpenseCategoryModel>>.Fail($"Could not find Expense category records for User Id = {userId}");
        }
    }
}
