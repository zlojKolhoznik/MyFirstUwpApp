using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFirstUwpApp.Services.MessageService
{
    public interface IMessageService
    {
        void ShowPlainMessage(string message);
        void SendErrorMessage(string message);
        Task<MessageResponse> SendPromptAsync(string message);
    }

    public enum MessageResponse
    {
        Yes,
        No
    }
}
