using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFirstUwpApp.Services.MessageService
{
    public interface IMessageService
    {
        Task ShowPlainMessageAsync(string message);
        Task ShowErrorMessageAsync(string message);
        Task<MessageResponse> ShowPromptAsync(string message, PromptType type);
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
