using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseManager.Shared.Models.SearchModels
{
    public class BaseSearchModel
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public string SortColumn { get; set; } = string.Empty;
        public string SortOrder { get; set; } = "asc";
    }
}
