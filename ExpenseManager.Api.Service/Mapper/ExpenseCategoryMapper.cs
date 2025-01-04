using ExpenseManager.Api.Database.Entities;
using ExpenseManager.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseManager.Api.Service.Mapper
{
    public class ExpenseCategoryMapper
    {
        public static void MapToUserExpenseCategory(ExpenseCategoryModel expenseCategoryModel, ref UserExpenseCategory userExpenseCategory)
        {
            userExpenseCategory.Id = expenseCategoryModel.Id;
            userExpenseCategory.UserId = expenseCategoryModel.UserId;
            userExpenseCategory.Title = expenseCategoryModel.Title;
            userExpenseCategory.Description = expenseCategoryModel.Description;
        }

        public static UserExpenseCategory MapToUserExpenseCategory(ExpenseCategoryModel expenseCategoryModel)
        {
            var userExpenseCategory = new UserExpenseCategory();
            MapToUserExpenseCategory(expenseCategoryModel, ref userExpenseCategory);
            return userExpenseCategory;
        }

        public static ExpenseCategoryModel MapToExpenseCategoryModel(UserExpenseCategory userExpenseCategory)
        {
            return new ExpenseCategoryModel()
            {
                Id = userExpenseCategory.Id,
                UserId = userExpenseCategory.UserId,
                Title = userExpenseCategory.Title,
                Description = userExpenseCategory.Description
            };
        }

        public static UserExpenseCategory MapToUserExpenseCategory(int userId, DefaultExpenseCategory defaultExpenseCategory)
        {
            return new UserExpenseCategory()
            {
                UserId = userId,
                Title = defaultExpenseCategory.Title,
                Description = defaultExpenseCategory.Description
            };
        }
    }
}
