using ExpenseManager.Shared;
using ExpenseManager.Shared.Models;
using ExpenseManager.Shared.Models.SearchModels;
using ExpenseManager.Web.Services.Interfaces;
using Microsoft.AspNetCore.Components;

namespace ExpenseManager.Web.Pages.Categories
{
    public partial class Index : ComponentBase
    {
        public PaginatedList<ExpenseCategoryModel> ExpenseCategories { get; set; } = new PaginatedList<ExpenseCategoryModel>();
        public BaseSearchModel SearchModel { get; set; } = new BaseSearchModel();
        
        [Inject]
        private NavigationManager Navigation { get; set; } = default!;

        [Inject]
        private IExpenseCategoryService ExpenseCategoryService { get; set; } = default!;

        protected override async Task OnInitializedAsync()
        {
            var userId = 1;
            var result = await ExpenseCategoryService.GetCategoriesByUserId(userId, SearchModel);
            if (result.Success)
                ExpenseCategories = result.Item ?? new PaginatedList<ExpenseCategoryModel>();
        }
        
        private void NavigateToAdd()
        {
            Navigation.NavigateTo("/categories/createoredit");
        }        
    }
}