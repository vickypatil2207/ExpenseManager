using System;
using System.Collections.Generic;
using System.Linq;
using ExpenseManager.Shared;
using ExpenseManager.Shared.Models;
using ExpenseManager.Shared.Models.SearchModels;

namespace ExpenseManager.Web.Services.Interfaces
{
    public interface IExpenseService
    {
        Task<ServiceResult<ExpenseModel>> CreateExpense(ExpenseModel model);
        Task<ServiceResult<ExpenseModel>> UpdateExpense(int id, ExpenseModel model);
        Task<ServiceResult<ExpenseModel>> DeleteExpense(int id);
        Task<ServiceResult<ExpenseModel>> GetExpenseById(int id);
        Task<ServiceResult<PaginatedList<ExpenseModel>>> SearchExpenses(int userId, ExpenseSearchModel searchModel);
    }
}