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
        void ShowErrorMessage(string message);
        Task<MessageResponse> ShowPrompt(string message, PromptType type);
    }

    public enum MessageResponse
    {
        Yes,
        No,
        Ok,
        Cancel
    }

    public enum PromptType
    {
        YesNo,
        YesNoCancel,
        OkCancel
    }
}
