using ExpenseManager.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace ExpenseManager.Web.Components
{
    public partial class Pagination<T> : ComponentBase where T : class
    {
        [Parameter]
        public PaginatedList<T>? PaginatedList { get; set; }

        [Parameter]
        public EventCallback<int> OnPageChangedCallback { get; set; }

        [Parameter]
        public EventCallback<int> OnPageSizeChangedCallback { get; set; }

        private List<int> PageSizes { get; } = new List<int> { 10, 25, 50, 100 };

        private int TotalPages => PaginatedList != null 
            ? (int)Math.Ceiling(PaginatedList.TotalCount / (double)PaginatedList.PageSize) 
            : 0;

        private int StartPage
        {
            get
            {
                if (PaginatedList == null) return 1;
                int start = PaginatedList.PageIndex - 2;
                return start > 0 ? start : 1;
            }
        }

        private int EndPage
        {
            get
            {
                if (PaginatedList == null) return 1;
                int end = PaginatedList.PageIndex + 2;
                return end < TotalPages ? end : TotalPages;
            }
        }

        private async Task OnPageChanged(int page)
        {
            if (PaginatedList != null && page >= 1 && page <= TotalPages && page != PaginatedList.PageIndex)
            {
                await OnPageChangedCallback.InvokeAsync(page);
            }
        }

        private async Task OnPageSizeChanged(ChangeEventArgs e)
        {
            if (e.Value != null && int.TryParse(e.Value.ToString(), out int newSize))
            {
                await OnPageSizeChangedCallback.InvokeAsync(newSize);
            }
        }
    }
}