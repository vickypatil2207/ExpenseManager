using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseManager.Api.Database.Entities
{
    public class PaymentType
    {
        public PaymentType()
        {
            Expenses = new List<Expense>();
        }

        public int Id { get; set; }
        public string Description { get; set; } = string.Empty;

        public IEnumerable<Expense> Expenses { get; set; }
    }
}
