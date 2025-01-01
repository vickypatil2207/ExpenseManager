using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseManager.Api.Database.Entities
{
    public class UserExpenseCategory
    {
        public UserExpenseCategory()
        {
            User  = new User();
            Expenses = new HashSet<Expense>();
        }

        public int Id { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        
        public User User { get; set; }
        public IEnumerable<Expense> Expenses { get; set; }
    }
}
