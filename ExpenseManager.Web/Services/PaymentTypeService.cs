using ExpenseManager.Shared;
using ExpenseManager.Shared.Models;
using ExpenseManager.Web.Services.Interfaces;

namespace ExpenseManager.Web.Services
{
    public class PaymentTypeService : IPaymentTypeService
    {
        private readonly ApiClient _apiClient;
        private const string ControllerEndpoint = "/api/paymenttype";
        
        public PaymentTypeService(ApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public async Task<ServiceResult<IEnumerable<PaymentTypeModel>>> GetPaymentTypeListAsync()
        {
            var endpoint = $"{ControllerEndpoint}/list";
            return await _apiClient.GetAsync<IEnumerable<PaymentTypeModel>>(endpoint);
        }
    }
}
