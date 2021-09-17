using System;
using Microsoft.AspNetCore.Mvc;

namespace TheMoney.Shared.UXServices
{
    public interface ITranslationService
    {
        public void ShowWarning(Controller controller, string name, params string[] messageParameters);
        public void ShowError(Controller controller, string name, params string[] messageParameters);
        public void ShowInfo(Controller controller, string name, params string[] messageParameters);
        public string GetTranslation(string name);
    }
}
