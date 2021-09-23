using System;
namespace TheMoney.Shared.UX.Translations
{
    public interface ITranslationsService
    {
        public string GetTranslation(string messageId, params string[] messageParameters);
    }
}
