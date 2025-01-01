using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseManager.Shared
{
    public class PaginatedList<T>
        where T: class
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public IEnumerable<int> PageList { get; set; } = new List<int>();
        public string SortingColumn { get; set; } = string.Empty;
        public string SortingOrder { get; set; } = string.Empty;
        public IEnumerable<T>? Items { get; set; } 
    }
}
