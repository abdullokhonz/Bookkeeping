using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Bookkeeping.Client.Dialogs
{
    public partial class ConfirmDeleteDialog
    {
        [CascadingParameter] IMudDialogInstance MudDialog { get; set; } = default!;

        [Parameter] public string Title { get; set; } = "Подтверждение";
        [Parameter] public string ContentText { get; set; } = "";
        [Parameter] public string ConfirmText { get; set; } = "Да";
        [Parameter] public string CancelText { get; set; } = "Нет";

        private void Confirm() => MudDialog.Close(DialogResult.Ok(true));
        private void Cancel() => MudDialog.Close(DialogResult.Cancel());
    }
}
