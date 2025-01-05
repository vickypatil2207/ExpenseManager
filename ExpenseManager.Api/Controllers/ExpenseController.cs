using ExpenseManager.Api.Service.Interfaces;
using ExpenseManager.Shared.Models;
using ExpenseManager.Shared.Models.SearchModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseManager.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ExpenseController : ControllerBase
    {
        private readonly IExpenseService _expenseService;
        
        public ExpenseController(IExpenseService expenseService)
        {
            _expenseService = expenseService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateExpense(ExpenseModel expenseModel)
        {
            var result = await _expenseService.CreateExpense(expenseModel);
            if (result.Success)
                Ok(result);

            return BadRequest(result);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateExpense(int id, ExpenseModel expenseModel)
        {
            var result = await _expenseService.UpdateExpense(id, expenseModel);
            if (result.Success)
                Ok(result);

            return BadRequest(result);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteExpense(int id)
        {
            var result = await _expenseService.DeleteExpense(id);
            if (result.Success)
                Ok(result);

            return BadRequest(result);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetExpenseById(int id)
        {
            var result = await _expenseService.GetExpenseById(id);
            if (result.Success)
                Ok(result);

            return BadRequest(result);
        }

        [HttpGet("user/{userId:int}/search")]
        public async Task<IActionResult> SearchExpenses(int userId, [FromQuery] ExpenseSearchModel expenseSearchModel)
        {
            var result = await _expenseService.SearchExpenses(userId, expenseSearchModel);
            if (result.Success)
                Ok(result);

            return BadRequest(result);
        }
    }
}
