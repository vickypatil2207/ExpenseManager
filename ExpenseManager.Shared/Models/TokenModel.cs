using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseManager.Shared.Models
{
    public class TokenModel
    {
        public string Token { get; set; } = string.Empty;
        public UserModel UserDetails { get; set; } = new UserModel();
    }
}
