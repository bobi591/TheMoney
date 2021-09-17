using System;
using MongoDB.Driver;
using TheMoney.Shared.Entities;

namespace TheMoney.Datastore.Databases.MongoDB.Application
{
    public sealed class MongoDBServices
    {
        private MongoClient _mongoClient;
        private IMongoDatabase _applicationDatabase;

        /* Tables/Collections declarations (TDocument is shared entity) */
        public IMongoCollection<Shared.Entities.User> Users { get; }
        public IMongoCollection<Shared.Entities.Chart> Charts { get; set; }
        public IMongoCollection<Shared.Entities.MonetaryTransaction> MonetaryTransactions { get; set; }

        public MongoDBServices(IMongoDBSettings applicationDatabaseSettings)
        {
            _mongoClient = new MongoClient(applicationDatabaseSettings.ConnectionString);
            _applicationDatabase = _mongoClient.GetDatabase(applicationDatabaseSettings.DatabaseName);

            Users = _applicationDatabase.GetCollection<User>("users");
            Charts = _applicationDatabase.GetCollection<Chart>("charts");
            MonetaryTransactions = _applicationDatabase.GetCollection<MonetaryTransaction>("monetarytransactions");
        }

        public IMongoCollection<object> GetDocumentCollection(EntityBase applicationDocument)
        {
            string documentTypeName = applicationDocument.GetType().Name;
            return _applicationDatabase.GetCollection<object>(documentTypeName);
        }
    }
}
