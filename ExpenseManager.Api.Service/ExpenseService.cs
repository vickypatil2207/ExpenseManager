using ExpenseManager.Api.Database.Entities;
using ExpenseManager.Api.Repository.Interfaces;
using ExpenseManager.Api.Service.Interfaces;
using ExpenseManager.Shared.Models;
using ExpenseManager.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ExpenseManager.Api.Service.Mapper;
using ExpenseManager.Shared.Models.SearchModels;
using ExpenseManager.Shared.Helpers;

namespace ExpenseManager.Api.Service
{
    public class ExpenseService : IExpenseService
    {
        private readonly IRepository<Expense> _expenseRepository;

        public ExpenseService(IRepository<Expense> expenseRepository)
        {
            _expenseRepository = expenseRepository;
        }

        public async Task<ServiceResult<ExpenseModel>> CreateExpense(ExpenseModel expenseModel)
        {
            var expense = ExpenseMapper.MapToExpense(expenseModel);
            var result = await _expenseRepository.CreateAsync(expense);
            if (result.Id > 0)
            {
                var expenseCreated = await _expenseRepository.GetByIdAsync(result.Id, e => e.UserExpenseCategory, e => e.PaymentType);
                if (expenseCreated != null)
                {
                    return ServiceResult<ExpenseModel>.Ok(ExpenseMapper.MapToExpenseModel(expenseCreated));
                }
            }

            return ServiceResult<ExpenseModel>.Fail("Something went wrong while saving Expense details!", expenseModel);
        }

        public async Task<ServiceResult<ExpenseModel>> UpdateExpense(int id, ExpenseModel expenseModel)
        {
            var expenseResult = await _expenseRepository.GetByIdAsync(id);
            if (expenseResult != null)
            {
                ExpenseMapper.MapToExpense(expenseModel, ref expenseResult);
                await _expenseRepository.UpdateAsync(expenseResult);

                var expenseUpdated = await _expenseRepository.GetByIdAsync(id, e => e.UserExpenseCategory, e => e.PaymentType);
                if (expenseUpdated != null)
                {
                    return ServiceResult<ExpenseModel>.Ok(ExpenseMapper.MapToExpenseModel(expenseUpdated));
                }
            }

            return ServiceResult<ExpenseModel>.Fail($"Could not find expense details for Id = {id}!", expenseModel);
        }

        public async Task<ServiceResult<ExpenseModel>> DeleteExpense(int id)
        {
            var expenseResult = await _expenseRepository.GetByIdAsync(id, e => e.UserExpenseCategory, e => e.PaymentType);
            if (expenseResult != null)
            {   
                await _expenseRepository.DeleteAsync(expenseResult);
                return ServiceResult<ExpenseModel>.Ok(ExpenseMapper.MapToExpenseModel(expenseResult));
            }

            return ServiceResult<ExpenseModel>.Fail($"Could not find expense details for Id = {id}!");
        }

        public async Task<ServiceResult<ExpenseModel>> GetExpenseById(int id)
        {
            var expenseResult = await _expenseRepository.GetByIdAsync(id, e => e.UserExpenseCategory, e => e.PaymentType);
            if (expenseResult != null)
            {
                return ServiceResult<ExpenseModel>.Ok(ExpenseMapper.MapToExpenseModel(expenseResult));
            }

            return ServiceResult<ExpenseModel>.Fail($"Could not find expense details for Id = {id}!");
        }

        public async Task<ServiceResult<PaginatedList<ExpenseModel>>> SearchExpenses(int userId, ExpenseSearchModel expenseSearchModel)
        {
            var paginatedList = new PaginatedList<ExpenseModel>()
            {
                PageIndex = expenseSearchModel.PageIndex,
                PageSize = expenseSearchModel.PageSize,
                SortColumn = expenseSearchModel.SortColumn,
                SortOrder = expenseSearchModel.SortOrder
            };

            var counts = await _expenseRepository.CountAsync(e => e.UserId == userId);
            if (counts > 0)
            {
                paginatedList.TotalCount = counts;

                var expenseResult = await _expenseRepository.SearchAsync(BuildFilterExpression(userId, expenseSearchModel),
                                            expenseSearchModel.PageIndex,
                                            expenseSearchModel.PageSize,
                                            expenseSearchModel.SortColumn,
                                            expenseSearchModel.SortOrder,
                                            e => e.UserExpenseCategory,
                                            e => e.PaymentType);
                if (expenseResult.Any())
                {
                    paginatedList.Items = expenseResult.Select(e => ExpenseMapper.MapToExpenseModel(e));
                    return ServiceResult<PaginatedList<ExpenseModel>>.Ok(paginatedList);
                }
            }

            return ServiceResult<PaginatedList<ExpenseModel>>.Fail($"Could not find expense records for User Id = {userId}!", paginatedList);
        }

        private Expression<Func<Expense, bool>> BuildFilterExpression(int userId, ExpenseSearchModel expenseSearchModel)
        {
            Expression<Func<Expense, bool>> predicate = e => e.UserId == userId;

            if (expenseSearchModel.UserExpenseCategoryId > 0)
            {
                predicate = predicate.AndAlso(e => e.UserExpenseCategoryId == expenseSearchModel.UserExpenseCategoryId);
            }

            if (expenseSearchModel.PaymentTypeId > 0)
            {
                predicate = predicate.AndAlso(e => e.PaymentTypeId == expenseSearchModel.PaymentTypeId);
            }

            if (expenseSearchModel.FromExpenseDate.HasValue)
            {
                predicate = predicate.AndAlso(e => e.ExpenseDate >= expenseSearchModel.FromExpenseDate);
            }

            if (expenseSearchModel.ToExpenseDate.HasValue)
            {
                predicate = predicate.AndAlso(e => e.ExpenseDate >= expenseSearchModel.ToExpenseDate);
            }

            return predicate;
        }
    }
}
