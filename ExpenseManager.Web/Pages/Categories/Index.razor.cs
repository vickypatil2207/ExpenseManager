using ExpenseManager.Shared;
using ExpenseManager.Shared.Models;
using ExpenseManager.Shared.Models.SearchModels;
using ExpenseManager.Web.Services.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace ExpenseManager.Web.Pages.Categories
{
    public partial class Index : ComponentBase
    {
        public PaginatedList<ExpenseCategoryModel> ExpenseCategories { get; set; } = new PaginatedList<ExpenseCategoryModel>();
        public BaseSearchModel SearchModel { get; set; } = new BaseSearchModel();
        
        private bool IsDeleteConfirmVisible { get; set; }
        private string DeleteConfirmMessage { get; set; } = string.Empty;
        private ExpenseCategoryModel? CategoryToDelete { get; set; }
        
        [Inject]
        private NavigationManager Navigation { get; set; } = default!;

        [Inject]
        private IExpenseCategoryService ExpenseCategoryService { get; set; } = default!;

        protected override async Task OnInitializedAsync()
        {
            await LoadCategories();
        }

        private async Task LoadCategories()
        {
            var userId = 1;
            var result = await ExpenseCategoryService.GetCategoriesByUserId(userId, SearchModel);
            if (result.Success)
                ExpenseCategories = result.Item ?? new PaginatedList<ExpenseCategoryModel>();
        }

        private async Task SearchCategories()
        {
            SearchModel.PageIndex = 1;
            await LoadCategories();
        }

        private async Task OnPageChanged(int page)
        {
            SearchModel.PageIndex = page;
            await LoadCategories();
        }

        private async Task OnPageSizeChanged(int pageSize)
        {
            SearchModel.PageSize = pageSize;
            SearchModel.PageIndex = 1;
            await LoadCategories();
        }

        private async Task HandleSearchKeyDown(KeyboardEventArgs e)
        {
            if (e.Key == "Enter")
            {
                await SearchCategories();
            }
        }

        private void ConfirmDelete(ExpenseCategoryModel category)
        {
            CategoryToDelete = category;
            DeleteConfirmMessage = $"Are you sure you want to delete '{category.Title}'?";
            IsDeleteConfirmVisible = true;
        }

        private async Task DeleteCategory()
        {
            if (CategoryToDelete != null)
            {
                var result = await ExpenseCategoryService.DeleteCategory(CategoryToDelete.Id);
                if (result.Success)
                {
                    await LoadCategories();
                }
            }
            IsDeleteConfirmVisible = false;
            CategoryToDelete = null;
        }

        private void CancelDelete()
        {
            IsDeleteConfirmVisible = false;
            CategoryToDelete = null;
        }

        private void NavigateToAdd()
        {
            Navigation.NavigateTo("/categories/createoredit");
        }        
    }
}