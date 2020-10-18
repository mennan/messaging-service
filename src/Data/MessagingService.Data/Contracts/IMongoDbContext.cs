using MongoDB.Driver;

namespace MessagingService.Data
{
    public interface IMongoDbContext
    {
        IMongoClient Client { get; }
        IMongoDatabase Database { get; }

        IMongoCollection<TDocument> GetCollection<TDocument>(string partitionKey);
    }
}