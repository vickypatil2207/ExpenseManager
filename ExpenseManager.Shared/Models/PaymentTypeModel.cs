using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseManager.Shared.Models
{
    public class PaymentTypeModel
    {
        public int Id { get; set; }
        public string Description { get; set; } = string.Empty;
    }
}
