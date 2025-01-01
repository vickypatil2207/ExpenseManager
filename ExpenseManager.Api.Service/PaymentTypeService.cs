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

namespace ExpenseManager.Api.Service
{
    public class PaymentTypeService : IPaymentTypeService
    {
        private readonly IRepository<PaymentType> _paymentTypeRepository; 

        public PaymentTypeService(IRepository<PaymentType> paymentTypeRepository)
        {
            _paymentTypeRepository = paymentTypeRepository;
        }

        public async Task<ServiceResult<IEnumerable<PaymentTypeModel>>> GetPaymentTypeList()
        {
            var result = await _paymentTypeRepository.GetListAsync();
            if (result != null)
            {
                var paymentTypes = result.Select(p => new PaymentTypeModel()
                {
                    Id = p.Id,
                    Description = p.Description
                });
                return ServiceResult<IEnumerable<PaymentTypeModel>>.Ok(paymentTypes);
            }

            return ServiceResult<IEnumerable<PaymentTypeModel>>.Fail("No Payment type records found!");
        }
    }
}
