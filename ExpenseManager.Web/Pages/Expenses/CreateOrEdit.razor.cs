using ExpenseManager.Shared.Models;
using ExpenseManager.Shared.Models.SearchModels;
using ExpenseManager.Web.Services.Interfaces;
using Microsoft.AspNetCore.Components;

namespace ExpenseManager.Web.Pages.Expenses
{
    public partial class CreateOrEdit : ComponentBase
    {
        [Parameter]
        public int? Id { get; set; }

        private ExpenseModel Expense { get; set; } = new();
        private List<ExpenseCategoryModel> Categories { get; set; } = new();
        private List<PaymentTypeModel> PaymentTypes { get; set; } = new();

        [Inject]
        private IExpenseService ExpenseService { get; set; } = default!;

        [Inject]
        private IExpenseCategoryService ExpenseCategoryService { get; set; } = default!;

        [Inject]
        private IPaymentTypeService PaymentTypeService { get; set; } = default!;

        [Inject]
        private NavigationManager NavigationManager { get; set; } = default!;

        protected override async Task OnInitializedAsync()
        {
            var categoriesTask = LoadCategoriesAsync();
            var paymentTypesTask = LoadPaymentTypesAsync();

            await Task.WhenAll(categoriesTask, paymentTypesTask);

            if (Id.HasValue)
            {
                var result = await ExpenseService.GetExpenseById(Id.Value);
                if (result.Success && result.Item != null)
                {
                    Expense = result.Item;
                }
            }
            else
            {
                Expense = new ExpenseModel
                {
                    ExpenseDate = DateTime.Today
                };
            }
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

        private async Task SaveExpense()
        {
            if (Id.HasValue)
            {
                await ExpenseService.UpdateExpense(Id.Value, Expense);
            }
            else
            {
                Expense.UserId = 1; // Replace with actual user ID from authentication context
                await ExpenseService.CreateExpense(Expense);
            }
            NavigationManager.NavigateTo("/expenses");
        }
    }
}
