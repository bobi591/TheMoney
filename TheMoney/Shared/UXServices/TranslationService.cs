using System;
using Microsoft.AspNetCore.Mvc;
using TheMoney.Shared.UXServices.Messages;

namespace TheMoney.Shared.UXServices
{
    public class TranslationService : ITranslationService
    {
        private IMessagesResource messagesResource;

        public TranslationService(IMessagesResource messagesResource)
        {
            this.messagesResource = messagesResource;
        }

        public void ShowError(Controller controller, string name, params string[] messageParameters)
        {
            controller.TempData["ERROR"] = messagesResource.GetMessage(name, messageParameters);
        }

        public void ShowInfo(Controller controller, string name, params string[] messageParameters)
        {
            controller.TempData["INFO"] = messagesResource.GetMessage(name, messageParameters);
        }

        public void ShowWarning(Controller controller, string name, params string[] messageParameters)
        {
            controller.TempData["WARNING"] = messagesResource.GetMessage(name, messageParameters);
        }

        public string GetTranslation(string name)
        {
            return messagesResource.GetMessage(name);
        }
    }
}
