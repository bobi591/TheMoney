using System;
using System.Resources;
using System.Text;

namespace TheMoney.Shared.UXServices.Messages
{
    public class MessagesResource : IMessagesResource
    {
        private ResourceManager resourceManager;

        public MessagesResource(ResourceManager resourceManager)
        {
            this.resourceManager = resourceManager;
        }

        public string GetMessage(string name, params string[] parameters)
        {
            if (parameters != null)
            {
                return string.Format(resourceManager.GetString(name), parameters);
            }
            else
            {
                return resourceManager.GetString(name);
            }
        }
    }
}
