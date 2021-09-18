using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.AspNetCore.Http;
using TheMoney.Shared.Entities;

namespace TheMoney.Modules.Data.Actions.Revolut
{
    public class RevolutCSVImport : ImportActionHandler
    {
        public RevolutCSVImport(Shared.Entities.User user, IFormFile dataFile, IRepository repository, ImportSource importSource)
            : base(user, dataFile, repository, importSource)
        {

        }

        public override async Task Execute()
        {
            using (StreamReader dataFileStreamReader = new StreamReader(dataFile.OpenReadStream()))
            {
                using (CsvReader csvReader = new CsvReader(dataFileStreamReader, CultureInfo.InvariantCulture))
                {
                    csvReader.Context.RegisterClassMap<RevolutCSVModelMap>();
                    IEnumerable<RevolutCSVModel> records = csvReader.GetRecords<RevolutCSVModel>();

                    List<MonetaryTransaction> parsedMonetaryTransactions = new List<MonetaryTransaction>();

                    foreach(RevolutCSVModel record in records)
                    {
                        MonetaryTransaction monetaryTransactionParsedRecord = new MonetaryTransaction()
                        {
                            Currency = record.Currency,
                            DataSource = importSource.ToString(),
                            ImportTimestamp = DateTime.Now,
                            OwnerEmail = user.Email,
                            TransactionTimestamp = DateTime.Parse(record.StartedDate),
                            Value = float.Parse(record.Amount)
                        };

                        parsedMonetaryTransactions.Add(monetaryTransactionParsedRecord);
                    }

                    List<MonetaryTransaction> cleanedUpMonetaryTransactions = RemoveParsedRecordsAlreadyInDatabase(parsedMonetaryTransactions);

                    repository.InsertMonetaryTransactions(cleanedUpMonetaryTransactions);
                }
            }
        }

        private class RevolutCSVModel
        {
            public string Type { get; set; }
            public string Product { get; set; }
            public string StartedDate { get; set; }
            public string CompletedDate { get; set; }
            public string Description { get; set; }
            public string Amount { get; set; }
            public string Fee { get; set; }
            public string Currency { get; set; }
            public string OriginalAmount { get; set; }
            public string OriginalCurrency { get; set; }
            public string SettledAmount { get; set; }
            public string SettledCurrency { get; set; }
            public string State { get; set; }
            public string Balance { get; set; }
        }

        private sealed class RevolutCSVModelMap : ClassMap<RevolutCSVModel>
        {
            public RevolutCSVModelMap()
            {
                AutoMap(CultureInfo.InvariantCulture);

                Map(m => m.StartedDate).Name("Started Date");
                Map(m => m.CompletedDate).Name("Completed Date");
                Map(m => m.OriginalAmount).Name("Original Amount");
                Map(m => m.OriginalCurrency).Name("Original Currency");
                Map(m => m.SettledAmount).Name("Settled Amount");
                Map(m => m.SettledCurrency).Name("Settled Currency");
            }
        }

        private List<MonetaryTransaction> RemoveParsedRecordsAlreadyInDatabase(List<MonetaryTransaction> parsedMonetaryTransactions)
        {
            List<MonetaryTransaction> cleanedUpParsedRecords = new List<MonetaryTransaction>();

            foreach(MonetaryTransaction parsedRecord in parsedMonetaryTransactions)
            {
                bool isNotUnique = repository.GetMonetaryTransactionsWhere
                    (x =>
                        x.OwnerEmail == parsedRecord.OwnerEmail &&
                        x.DataSource == parsedRecord.DataSource &&
                        x.TransactionTimestamp == parsedRecord.TransactionTimestamp &&
                        x.Value == parsedRecord.Value &&
                        x.Currency == parsedRecord.Currency
                    ).Any();

                if(!isNotUnique)
                {
                    cleanedUpParsedRecords.Add(parsedRecord);
                }

            }

            return cleanedUpParsedRecords;
        }
    }
}
