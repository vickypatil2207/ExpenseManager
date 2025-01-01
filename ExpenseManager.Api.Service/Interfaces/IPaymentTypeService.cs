using ExpenseManager.Shared;
using ExpenseManager.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseManager.Api.Service.Interfaces
{
    public interface IPaymentTypeService
    {
        Task<ServiceResult<IEnumerable<PaymentTypeModel>>> GetPaymentTypeList();
    }
}
