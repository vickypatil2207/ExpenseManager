using ExpenseManager.Shared;
using ExpenseManager.Shared.Models;
using ExpenseManager.Shared.Models.SearchModels;
using ExpenseManager.Web.Services.Interfaces;

namespace ExpenseManager.Web.Services
{
    public class ExpenseCategoryService : IExpenseCategoryService
    {
        private readonly ApiClient _apiClient;
        private const string ControllerEndpoint = "/api/expensecategory";
        
        public ExpenseCategoryService(ApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public async Task<ServiceResult<ExpenseCategoryModel>> CreateCategory(ExpenseCategoryModel expenseCategoryModel)
        {
            return await _apiClient.PostAsync<ExpenseCategoryModel>(ControllerEndpoint, expenseCategoryModel);
        }

        public async Task<bool> CreateCategoriesForUserSignup(int userId)
        {
            // This method is typically called after user registration
            // Implement based on your API endpoint requirements
            throw new NotImplementedException();
        }

        public async Task<ServiceResult<ExpenseCategoryModel>> UpdateCategory(int id, ExpenseCategoryModel expenseCategoryModel)
        {
            var endpoint = $"{ControllerEndpoint}/{id}";
            return await _apiClient.PutAsync<ExpenseCategoryModel>(endpoint, expenseCategoryModel);
        }

        public async Task<ServiceResult<ExpenseCategoryModel>> DeleteCategory(int id)
        {
            var endpoint = $"{ControllerEndpoint}/{id}";
            return await _apiClient.DeleteAsync<ExpenseCategoryModel>(endpoint);
        }

        public async Task<ServiceResult<ExpenseCategoryModel>> GetCategoryById(int id)
        {
            var endpoint = $"{ControllerEndpoint}/{id}";
            return await _apiClient.GetAsync<ExpenseCategoryModel>(endpoint);
        }

        public async Task<ServiceResult<PaginatedList<ExpenseCategoryModel>>> GetCategoriesByUserId(int userId, BaseSearchModel baseSearchModel)
        {
            var endpoint = $"{ControllerEndpoint}/user/{userId}";
            
            // Add query parameters for search model
            var queryString = BuildQueryString(baseSearchModel);
            if (!string.IsNullOrEmpty(queryString))
            {
                endpoint += $"?{queryString}";
            }

            return await _apiClient.GetAsync<PaginatedList<ExpenseCategoryModel>>(endpoint);
        }

        /// <summary>
        /// Builds query string from BaseSearchModel
        /// </summary>
        private string BuildQueryString(BaseSearchModel searchModel)
        {
            var queryParams = new List<string>();

            if (!string.IsNullOrEmpty(searchModel.SearchText))
                queryParams.Add($"searchText={Uri.EscapeDataString(searchModel.SearchText)}");

            if (searchModel.PageIndex > 0)
                queryParams.Add($"pageIndex={searchModel.PageIndex}");

            if (searchModel.PageSize > 0)
                queryParams.Add($"pageSize={searchModel.PageSize}");

            return string.Join("&", queryParams);
        }
    }
}