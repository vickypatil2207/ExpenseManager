using ExpenseManager.Shared;
using ExpenseManager.Shared.Models;
using ExpenseManager.Shared.Models.SearchModels;
using ExpenseManager.Web.Services.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace ExpenseManager.Web.Pages.Expenses
{
    public partial class Index : ComponentBase
    {
        private ExpenseSearchModel SearchModel { get; set; } = new();
        private PaginatedList<ExpenseModel> Expenses { get; set; } = new();
        private List<ExpenseCategoryModel> Categories { get; set; } = new();
        private List<PaymentTypeModel> PaymentTypes { get; set; } = new();
        private int? ExpenseToDelete { get; set; }
        private bool IsLoading { get; set; }
        private bool IsDeleteConfirmVisible { get; set; }
        private string DeleteConfirmMessage { get; set; } = string.Empty;

        [Inject]
        private IExpenseService ExpenseService { get; set; } = default!;

        [Inject]
        private IPaymentTypeService PaymentTypeService { get; set; } = default!;

        [Inject]
        private IExpenseCategoryService ExpenseCategoryService { get; set; } = default!;

        [Inject]
        private NavigationManager NavigationManager { get; set; } = default!;

        protected override async Task OnInitializedAsync()
        {
            var expensesTask = LoadExpensesAsync();
            var categoriesTask = LoadCategoriesAsync();
            var paymentTypesTask = LoadPaymentTypesAsync();

            await Task.WhenAll(expensesTask, categoriesTask, paymentTypesTask);
        }

        private async Task LoadExpensesAsync()
        {
            IsLoading = true;
            var userId = 1;
            var result = await ExpenseService.SearchExpenses(userId, SearchModel);
            Expenses = result.Item ?? new PaginatedList<ExpenseModel>();
            IsLoading = false;
        }

        private async Task LoadCategoriesAsync()
        {
            var userId = 1;
            var result = await ExpenseCategoryService.GetCategoriesByUserId(userId, new BaseSearchModel());
            Categories = result.Item?.Items?.ToList() ?? new List<ExpenseCategoryModel>();
        }

        private async Task LoadPaymentTypesAsync()
        {
            var result = await PaymentTypeService.GetPaymentTypeListAsync();
            PaymentTypes = result.Item?.ToList() ?? new List<PaymentTypeModel>();
        }

        private async Task SearchExpenses()
        {
            SearchModel.PageIndex = 1;
            await LoadExpensesAsync();
        }

        private async Task OnPageChanged(int pageIndex)
        {
            SearchModel.PageIndex = pageIndex;
            await LoadExpensesAsync();
        }

        private async Task OnPageSizeChanged(int pageSize)
        {
            SearchModel.PageSize = pageSize;
            SearchModel.PageIndex = 1;
            await LoadExpensesAsync();
        }

        private async Task OnCategoryFilterChanged(int? categoryId)
        {
            SearchModel.UserExpenseCategoryId = categoryId ?? 0;
            SearchModel.PageIndex = 1;
            await LoadExpensesAsync();
        }

        private async Task OnPaymentTypeFilterChanged(int? paymentTypeId)
        {
            SearchModel.PaymentTypeId = paymentTypeId ?? 0;
            SearchModel.PageIndex = 1;
            await LoadExpensesAsync();
        }

        private async Task OnFromDateChanged(DateTime? date)
        {
            SearchModel.FromExpenseDate = date;
            SearchModel.PageIndex = 1;
            await LoadExpensesAsync();
        }

        private async Task OnToDateChanged(DateTime? date)
        {
            SearchModel.ToExpenseDate = date;
            SearchModel.PageIndex = 1;
            await LoadExpensesAsync();
        }

        private async Task HandleSearchKeyDown(KeyboardEventArgs e)
        {
            if (e.Key == "Enter")
            {
                await SearchExpenses();
            }
        }

        private void NavigateToAdd()
        {
            NavigationManager.NavigateTo("/expenses/createoredit");
        }

        private void ConfirmDelete(ExpenseModel expense)
        {
            ExpenseToDelete = expense.Id;
            DeleteConfirmMessage = $"Are you sure you want to delete expense '{expense.ExpenseCategory}'?";
            IsDeleteConfirmVisible = true;
        }

        private async Task DeleteExpense()
        {
            if (ExpenseToDelete.HasValue)
            {
                var result = await ExpenseService.DeleteExpense(ExpenseToDelete.Value);
                if (result.Success)
                {
                    await LoadExpensesAsync();
                }
            }
            IsDeleteConfirmVisible = false;
            ExpenseToDelete = null;
        }

        private void CancelDelete()
        {
            IsDeleteConfirmVisible = false;
            ExpenseToDelete = null;
        }
    }
}
