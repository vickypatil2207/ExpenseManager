using ExpenseManager.Shared;
using ExpenseManager.Shared.Models;

namespace ExpenseManager.Web.Services.Interfaces
{
    public interface IPaymentTypeService
    {
        Task<ServiceResult<IEnumerable<PaymentTypeModel>>> GetPaymentTypeListAsync();
    }
}
