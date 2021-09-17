using System;
namespace TheMoney.Datastore.Databases.MongoDB.Application
{
    public sealed class MongoDBSettings : IMongoDBSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

    public interface IMongoDBSettings
    {
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
