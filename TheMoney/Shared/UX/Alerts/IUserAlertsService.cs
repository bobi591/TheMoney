using System;
namespace TheMoney.Shared.UX.Alerts
{
    public interface IUserAlertsService
    {
        public void ShowError(string messageId, params string[] messageParameters);
        public void ShowInfo(string messageId, params string[] messageParameters);
        public void ShowWarning(string messageId, params string[] messageParameters);
    }
}
