using ExpenseManager.Api.Database.Entities;
using ExpenseManager.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseManager.Api.Service.Mapper
{
    public class ExpenseMapper
    {
        public static void MapToExpense(ExpenseModel expenseModel, ref Expense expense)
        {
            expense.Id = expenseModel.Id;
            expense.UserId = expenseModel.UserId;
            expense.UserExpenseCategoryId = expenseModel.UserExpenseCategoryId;
            expense.ExpenseDate = expenseModel.ExpenseDate;
            expense.PaymentTypeId = expenseModel.PaymentTypeId;
            expense.Amount = expenseModel.Amount;
            expense.Notes = expenseModel.Notes;
            expense.CreatedDate = expenseModel.CreatedDate ?? DateTime.Now;
            expense.ModifiedDate = expenseModel.ModifiedDate;
        }

        public static Expense MapToExpense(ExpenseModel expenseModel)
        {
            Expense expense = new Expense();
            MapToExpense(expenseModel, ref expense);
            return expense;
        }

        public static ExpenseModel MapToExpenseModel(Expense expense)
        {
            return new ExpenseModel()
            {
                Id = expense.Id,
                UserId = expense.UserId,
                UserExpenseCategoryId = expense.UserExpenseCategoryId,
                ExpenseCategory = expense.UserExpenseCategory?.Title ?? string.Empty,
                ExpenseDate = expense.ExpenseDate,
                PaymentTypeId = expense.PaymentTypeId,
                PaymentType = expense.PaymentType?.Description ?? string.Empty,
                Amount= expense.Amount,
                Notes = expense.Notes,
                CreatedDate = expense.CreatedDate,
                ModifiedDate = expense.ModifiedDate,
            };
        }
    }
}
