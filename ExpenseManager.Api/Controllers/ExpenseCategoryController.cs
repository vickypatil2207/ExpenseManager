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
    public class ExpenseCategoryController : ControllerBase
    {
        private readonly IExpenseCategoryService _expenseCategoryService;

        public ExpenseCategoryController(IExpenseCategoryService expenseCategoryService)
        {
            _expenseCategoryService = expenseCategoryService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(ExpenseCategoryModel expenseCategoryModel)
        {
            var result = await _expenseCategoryService.CreateCategory(expenseCategoryModel);
            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateCategory(int id, ExpenseCategoryModel expenseCategoryModel)
        {
            var result = await _expenseCategoryService.UpdateCategory(id, expenseCategoryModel);
            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var result = await _expenseCategoryService.DeleteCategory(id);
            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            var result = await _expenseCategoryService.GetCategoryById(id);
            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpGet("user/{userId:int}")]
        public async Task<IActionResult> GetCategoriesByUserId(int userId, [FromQuery] BaseSearchModel searchModel)
        {
            var result = await _expenseCategoryService.GetCategoriesByUserId(userId, searchModel);
            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }
    }
}
