using ExpenseManager.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace ExpenseManager.Web.Components
{
    public partial class ConfirmBox : ComponentBase
    {
        [Parameter]
    public bool IsVisible { get; set; }

    [Parameter]
    public string Message { get; set; } = "Are you sure?";

    [Parameter]
    public EventCallback OnConfirm { get; set; }

    [Parameter]
    public EventCallback OnCancel { get; set; }

    private async Task OnConfirmClick()
    {
        await OnConfirm.InvokeAsync();
    }

    private async Task OnCancelClick()
    {
        await OnCancel.InvokeAsync();
    }
    }
}