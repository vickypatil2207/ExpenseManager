using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseManager.Shared.Models.SearchModels
{
    public class ExpenseSearchModel : BaseSearchModel
    {
        public int UserExpenseCategoryId { get; set; }
        public int PaymentTypeId { get; set; }
        public DateTime? FromExpenseDate { get; set; }
        public DateTime? ToExpenseDate { get; set; }
    }
}
