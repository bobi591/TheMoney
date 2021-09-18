using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using TheMoney.Modules.Data.Actions.Revolut;
using TheMoney.Shared.Entities;

namespace TheMoney.Modules.Data.Actions
{
    public class ImportAction : ImportActionHandler
    {
        public ImportAction(Shared.Entities.User user, IFormFile dataFile, IRepository repository, ImportSource importSource)
            : base(user, dataFile, repository, importSource)
        {

        }

        public override async Task Execute()
        {
            string importedFileFormat = Path.GetExtension(dataFile.FileName);

            if(importSource == ImportSource.REVOLUT)
            {
                if (importedFileFormat.Equals(".csv"))
                {
                    SetSuccessor(new RevolutCSVImport(user, dataFile, repository, importSource));
                }
            }

            await ExecuteSuccessor();
        }

    }
}
