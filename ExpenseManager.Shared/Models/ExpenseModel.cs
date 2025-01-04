using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseManager.Shared.Models
{
    public class ExpenseModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int UserExpenseCategoryId { get; set; }
        public string ExpenseCategory { get; set; } = string.Empty;
        public DateTime ExpenseDate { get; set; }
        public int PaymentTypeId { get; set; }
        public string PaymentType { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public string? Notes { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
