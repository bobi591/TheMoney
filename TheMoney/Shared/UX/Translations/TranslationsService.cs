using System;
using System.Resources;

namespace TheMoney.Shared.UX.Translations
{
    public class TranslationsService : ITranslationsService
    {
        private ResourceManager resourceManager;

        public TranslationsService(ResourceManager resourceManager)
        {
            this.resourceManager = resourceManager;
        }

        string ITranslationsService.GetTranslation(string messageId, params string[] messageParameters)
        {
            if (messageParameters != null)
            {
                return string.Format(resourceManager.GetString(messageId), messageParameters);
            }
            else
            {
                return resourceManager.GetString(messageId);
            }
        }
    }
}
