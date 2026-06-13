using ExpenseManager.Shared;
using ExpenseManager.Shared.Models;
using ExpenseManager.Shared.Models.SearchModels;
using ExpenseManager.Web.Services.Interfaces;

namespace ExpenseManager.Web.Services
{
    public class ExpenseService : IExpenseService
    {
        private readonly ApiClient _apiClient;
        private const string ControllerEndpoint = "/api/expense";
        
        public ExpenseService(ApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public async Task<ServiceResult<ExpenseModel>> CreateExpense(ExpenseModel model)
        {
            return await _apiClient.PostAsync<ExpenseModel>(ControllerEndpoint, model);
        }

        public async Task<ServiceResult<ExpenseModel>> UpdateExpense(int id, ExpenseModel model)
        {
            var endpoint = $"{ControllerEndpoint}/{id}";
            return await _apiClient.PutAsync<ExpenseModel>(endpoint, model);
        }

        public async Task<ServiceResult<ExpenseModel>> DeleteExpense(int id)
        {
            var endpoint = $"{ControllerEndpoint}/{id}";
            return await _apiClient.DeleteAsync<ExpenseModel>(endpoint);
        }

        public async Task<ServiceResult<ExpenseModel>> GetExpenseById(int id)
        {
            var endpoint = $"{ControllerEndpoint}/{id}";
            return await _apiClient.GetAsync<ExpenseModel>(endpoint);
        }

        public async Task<ServiceResult<PaginatedList<ExpenseModel>>> SearchExpenses(int userId, ExpenseSearchModel searchModel)
        {
            var endpoint = $"{ControllerEndpoint}/user/{userId}/search";
            
            var queryString = BuildQueryString(searchModel);
            if (!string.IsNullOrEmpty(queryString))
            {
                endpoint += $"?{queryString}";
            }

            return await _apiClient.GetAsync<PaginatedList<ExpenseModel>>(endpoint);
        }

        private string BuildQueryString(ExpenseSearchModel searchModel)
        {
            var queryParams = new List<string>();

            if (!string.IsNullOrEmpty(searchModel.SearchText))
                queryParams.Add($"searchText={Uri.EscapeDataString(searchModel.SearchText)}");

            queryParams.Add($"pageIndex={searchModel.PageIndex}");
            queryParams.Add($"pageSize={searchModel.PageSize}");

            if (searchModel.UserExpenseCategoryId > 0)
                queryParams.Add($"userExpenseCategoryId={searchModel.UserExpenseCategoryId}");

            if (searchModel.PaymentTypeId > 0)
                queryParams.Add($"paymentTypeId={searchModel.PaymentTypeId}");

            if (searchModel.FromExpenseDate.HasValue)
                queryParams.Add($"fromExpenseDate={searchModel.FromExpenseDate.Value.ToString("yyyy-MM-dd")}");

            if (searchModel.ToExpenseDate.HasValue)
                queryParams.Add($"toExpenseDate={searchModel.ToExpenseDate.Value.ToString("yyyy-MM-dd")}");

            return string.Join("&", queryParams);
        }
    }
}
