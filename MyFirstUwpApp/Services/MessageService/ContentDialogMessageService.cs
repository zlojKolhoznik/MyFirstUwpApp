using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace MyFirstUwpApp.Services.MessageService
{
    public class ContentDialogMessageService : IMessageService
    {
        public void SendErrorMessage(string message)
        {
            var contentDialog = new ContentDialog
            {
                Title = "Error",
                Content = message,
                CloseButtonText = "OK",
                Background = new Windows.UI.Xaml.Media.SolidColorBrush(Windows.UI.Colors.Red),
                Foreground = new Windows.UI.Xaml.Media.SolidColorBrush(Windows.UI.Colors.White)
            };
            contentDialog.ShowAsync();
        }

        public void ShowPlainMessage(string message)
        {
            var contentDialog = new ContentDialog
            {
                Content = message,
                CloseButtonText = "OK"
            };
            contentDialog.ShowAsync();
        }

        public async Task<MessageResponse> SendPromptAsync(string message)
        {
            var contentDialog = new ContentDialog {
                Content = message,
                CloseButtonText = "No",
                PrimaryButtonText = "Yes"
            };
            var result = await contentDialog.ShowAsync();
            return result == ContentDialogResult.Primary ? MessageResponse.Yes : MessageResponse.No;
        }
    }
}
