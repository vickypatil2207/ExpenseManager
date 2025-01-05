using ExpenseManager.Api.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseManager.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentTypeController : ControllerBase
    {
        private readonly IPaymentTypeService _paymentTypeService;
        public PaymentTypeController(IPaymentTypeService paymentTypeService) 
        { 
            _paymentTypeService = paymentTypeService;
        }

        [HttpGet("list")]
        public async Task<IActionResult> GetList()
        {
            var serviceResult = await _paymentTypeService.GetPaymentTypeList();
            if (serviceResult.Success)
                return Ok(serviceResult);

            return BadRequest(serviceResult);
        }
    }
}
