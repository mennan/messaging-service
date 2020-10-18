using System.Linq;
using System.Reflection;
using MongoDB.Driver;

namespace MessagingService.Data
{
    public class MessagingServiceContext : IMongoDbContext
    {
        public IMongoClient Client { get; }
        public IMongoDatabase Database { get; }
        
        public MessagingServiceContext(string connectionString, string databaseName)
        {
            Client = new MongoClient(connectionString);
            Database = Client.GetDatabase(databaseName);
        }
        
        public virtual IMongoCollection<TDocument> GetCollection<TDocument>(string partitionKey)
        {
            return Database.GetCollection<TDocument>(partitionKey);
        }
    }
}