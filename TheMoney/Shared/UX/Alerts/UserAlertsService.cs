using System;
using Microsoft.AspNetCore.Mvc;
using TheMoney.Shared.UX.Translations;

namespace TheMoney.Shared.UX.Alerts
{
    public class UserAlertsService : IUserAlertsService
    {
        public Controller controller;
        public ITranslationsService translationsService;

        public UserAlertsService(Controller currentController, ITranslationsService translationsService)
        {
            this.controller = currentController;
            this.translationsService = translationsService;
        }

        void IUserAlertsService.ShowError(string messageId, params string[] messageParameters)
        {
            controller.TempData["ERROR"] = translationsService.GetTranslation(messageId, messageParameters);
        }

        void IUserAlertsService.ShowInfo(string messageId, params string[] messageParameters)
        {
            controller.TempData["INFO"] = translationsService.GetTranslation(messageId, messageParameters);
        }

        void IUserAlertsService.ShowWarning(string messageId, params string[] messageParameters)
        {
            controller.TempData["WARNING"] = translationsService.GetTranslation(messageId, messageParameters);
        }
    }
}
