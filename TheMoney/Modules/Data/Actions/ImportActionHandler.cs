using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using TheMoney.Shared.Entities;

namespace TheMoney.Modules.Data.Actions
{
    public abstract class ImportActionHandler
    {
        public ImportActionHandler(Shared.Entities.User user, IFormFile dataFile, IRepository repository, ImportSource importSource)
        {
            this.user = user;
            this.dataFile = dataFile;
            this.repository = repository;
            this.importSource = importSource;
        }

        protected Shared.Entities.User user;
        protected IFormFile dataFile;
        protected ImportActionHandler successor;
        protected IRepository repository;
        protected ImportSource importSource;

        public void SetSuccessor(ImportActionHandler successor)
        {
            this.successor = successor;
        }

        protected async Task ExecuteSuccessor()
        {
            if (successor != null)
            {
                await successor.Execute();
            }
        }

        public abstract Task Execute();
    }

    public enum ImportSource
    {
        REVOLUT,
        DSKBANK
    }
}
