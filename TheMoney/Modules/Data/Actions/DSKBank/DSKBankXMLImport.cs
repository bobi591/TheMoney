using System;
using System.IO;
using System.Xml;
using Microsoft.AspNetCore.Http;

namespace TheMoney.Modules.Data.Actions.DSKBank
{
    public class DSKBankXMLImport : DataActionsHandler
    {
        public DSKBankXMLImport(Shared.Entities.User user, IFormFile dataFile) : base(user, dataFile)
        {

        }

        public override void Execute()
        {
            var nodes = GetXmlDocumentFromImportedFile().ChildNodes;

            ExecuteSuccessor();
        }

        private XmlDocument GetXmlDocumentFromImportedFile()
        {
            XmlDocument importedXmlDocument = new XmlDocument();

            using (Stream importedFileStream = importedFile.OpenReadStream())
            {
                using (XmlReader xmlReader = XmlReader.Create(importedFileStream))
                {
                    importedXmlDocument.Load(xmlReader);
                }
            }

            return importedXmlDocument;
        }
    }
}
