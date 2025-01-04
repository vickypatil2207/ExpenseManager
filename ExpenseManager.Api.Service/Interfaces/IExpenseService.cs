using ExpenseManager.Shared;
using ExpenseManager.Shared.Models;
using ExpenseManager.Shared.Models.SearchModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseManager.Api.Service.Interfaces
{
    public interface IExpenseService
    {
        Task<ServiceResult<ExpenseModel>> CreateExpense(ExpenseModel expenseModel);
        Task<ServiceResult<ExpenseModel>> UpdateExpense(int id, ExpenseModel expenseModel);
        Task<ServiceResult<ExpenseModel>> DeleteExpense(int id);
        Task<ServiceResult<ExpenseModel>> GetExpenseById(int id);
        Task<ServiceResult<PaginatedList<ExpenseModel>>> SearchExpenses(int userId, ExpenseSearchModel expenseSearchModel);
    }
}
