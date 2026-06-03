using ExpenseManager.Shared;
using ExpenseManager.Shared.Models;
using ExpenseManager.Shared.Models.SearchModels;

namespace ExpenseManager.Web.Services.Interfaces
{
    public interface IExpenseCategoryService
    {
        Task<ServiceResult<ExpenseCategoryModel>> CreateCategory(ExpenseCategoryModel expenseCategoryModel);
        Task<bool> CreateCategoriesForUserSignup(int userId);
        Task<ServiceResult<ExpenseCategoryModel>> UpdateCategory(int id, ExpenseCategoryModel expenseCategoryModel);
        Task<ServiceResult<ExpenseCategoryModel>> DeleteCategory(int id);
        Task<ServiceResult<ExpenseCategoryModel>> GetCategoryById(int id);
        Task<ServiceResult<PaginatedList<ExpenseCategoryModel>>> GetCategoriesByUserId(int userId, BaseSearchModel baseSearchModel);
    }
}