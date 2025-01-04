using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseManager.Api.Database.Entities
{
    public class Expense
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int UserExpenseCategoryId { get; set; }
        public DateTime ExpenseDate { get; set; }
        public int PaymentTypeId { get; set; }
        public decimal Amount { get; set; }
        public string? Notes { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime? ModifiedDate { get; set; }

        public User? User { get; set; }
        public UserExpenseCategory? UserExpenseCategory { get; set; }
        public PaymentType? PaymentType { get; set; }
    }
}
