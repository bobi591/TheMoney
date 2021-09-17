using System;
namespace TheMoney.Shared.UXServices.Messages
{
    public interface IMessagesResource
    {
        public string GetMessage(string name, params string[] parameters);
    }
}
