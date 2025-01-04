using ExpenseManager.Shared.Models.SearchModels;
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
        public PaginatedList(int pageIndex, int pageSize, int totalCount, string sortColumn, string sortOrder)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            TotalCount = totalCount;
            SortColumn =sortColumn;
            SortOrder = sortOrder;
        }

        public PaginatedList(int totalCount, BaseSearchModel baseSearchModel)
        {
            PageIndex = baseSearchModel.PageIndex;
            PageSize = baseSearchModel.PageSize;
            TotalCount = totalCount;
            SortColumn = baseSearchModel.SortColumn;
            SortOrder = baseSearchModel.SortOrder;
        }

        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public string SortColumn { get; set; } = string.Empty;
        public string SortOrder { get; set; } = string.Empty;
        public IEnumerable<T>? Items { get; set; } 
    }
}
