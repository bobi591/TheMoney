using System;
using Microsoft.AspNetCore.Http;

namespace TheMoney.Modules.Data.Actions
{
    public abstract class DataActionsHandler
    {
        public DataActionsHandler(Shared.Entities.User user, IFormFile dataFile)
        {
            this.user = user;
            this.importedFile = dataFile;
        }

        protected Shared.Entities.User user;
        protected IFormFile importedFile;
        protected DataActionsHandler successor;

        public void SetSuccessor(DataActionsHandler successor)
        {
            this.successor = successor;
        }

        protected void ExecuteSuccessor()
        {
            if (successor != null)
            {
                successor.Execute();
            }
        }

        public abstract void Execute();
    }
}
