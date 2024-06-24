using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFirstUwpApp.Services.MessageService
{
    public class MessageDialogService : IMessageService
    {
        public void ShowErrorMessage(string message)
        {
            var messageDialog = new Windows.UI.Popups.MessageDialog(message)
            {
                Title = "Error"
            };
            messageDialog.Commands.Add(new Windows.UI.Popups.UICommand("Ok"));
            messageDialog.ShowAsync();
        }

        public void ShowPlainMessage(string message)
        {
            var messageDialog = new Windows.UI.Popups.MessageDialog(message);
            messageDialog.Commands.Add(new Windows.UI.Popups.UICommand("Ok"));
            messageDialog.ShowAsync();
        }

        public async Task<MessageResponse> ShowPromptAsync(string message, PromptType type)
        {
            var messageDialog = new Windows.UI.Popups.MessageDialog(message);
            switch (type)
            {
                case PromptType.YesNo:
                    messageDialog.Commands.Add(new Windows.UI.Popups.UICommand("Yes"));
                    messageDialog.Commands.Add(new Windows.UI.Popups.UICommand("No"));
                    break;
                case PromptType.YesNoCancel:
                    messageDialog.Commands.Add(new Windows.UI.Popups.UICommand("Yes"));
                    messageDialog.Commands.Add(new Windows.UI.Popups.UICommand("No"));
                    messageDialog.Commands.Add(new Windows.UI.Popups.UICommand("Cancel"));
                    break;
                case PromptType.OkCancel:
                    messageDialog.Commands.Add(new Windows.UI.Popups.UICommand("Ok"));
                    messageDialog.Commands.Add(new Windows.UI.Popups.UICommand("Cancel"));
                    break;
            }
            var result = await messageDialog.ShowAsync();
            switch (result.Label)
            {
                case "Yes":
                    return MessageResponse.Yes;
                case "No":
                    return MessageResponse.No;
                case "Ok":
                    return MessageResponse.Ok;
                case "Cancel":
                    return MessageResponse.Cancel;
                default:
                    return MessageResponse.Cancel;
            }
        }
    }
}
