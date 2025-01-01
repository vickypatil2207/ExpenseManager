using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseManager.Api.Database.Entities
{
    public class User
    {
        public User()
        {
            UserExpenseCategories = new HashSet<UserExpenseCategory>();
            Expenses = new HashSet<Expense>();
        }

        public int Id { get; set; }
        public string Firstname { get; set; } = string.Empty;
        public string Lastname { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public DateTime? DateOfBirth { get; set; }
        public string Gender { get; set; } = string.Empty;
        public DateTime RegistrationDate { get; set; } = DateTime.Now;
        public bool IsActive { get; set; } = true;

        public IEnumerable<UserExpenseCategory> UserExpenseCategories { get; set; }
        public IEnumerable<Expense> Expenses { get; set; }
    }
}
