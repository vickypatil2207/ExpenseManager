using ExpenseManager.Shared.Models;
using ExpenseManager.Web.Services.Interfaces;
using Microsoft.AspNetCore.Components;

namespace ExpenseManager.Web.Pages.Categories
{
    public partial class CreateOrEdit : ComponentBase
    {
        [Parameter]
        public int? Id { get; set; }

        private ExpenseCategoryModel categoryModel = new();

        [Inject]
        private IExpenseCategoryService ExpenseCategoryService { get; set; } = default!;

        [Inject]
        private NavigationManager Navigation { get; set; } = default!;

        protected override async Task OnInitializedAsync()
        {
            if (Id.HasValue)
            {
                var result = await ExpenseCategoryService.GetCategoryById(Id.Value);
                if (result.Success && result.Item != null)
                {
                    categoryModel = result.Item;
                }
            }
        }

        private async Task SaveCategory()
        {
            if (Id.HasValue)
            {
                await ExpenseCategoryService.UpdateCategory(Id.Value, categoryModel);
            }
            else
            {
                categoryModel.UserId = 1; // Replace with actual user ID from authentication context
                await ExpenseCategoryService.CreateCategory(categoryModel);
            }
            Navigation.NavigateTo("/categories");
        }
    }
}