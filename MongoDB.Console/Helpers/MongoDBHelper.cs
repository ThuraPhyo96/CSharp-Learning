using MongoDB.Bson;
using MongoDB.Driver;

namespace MongoDB.Console.Helpers
{
    public class MongoDBHelper
    {
        private readonly IMongoCollection<BsonDocument> _collection;

        public MongoDBHelper(string connectionString, string dbName, string collectionName)
        {
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase(dbName);
            _collection = database.GetCollection<BsonDocument>(collectionName);
        }

        // Insert document
        public async Task InsertAsync(BsonDocument document)
        {
            await _collection.InsertOneAsync(document);
        }

        // Read all document
        public async Task<List<BsonDocument>> GetAllAsync()
        {
            return await _collection.Find(new BsonDocument()).ToListAsync();
        }

        // Update document
        public async Task<long> UpdateAsync(string id, BsonDocument document)
        {
            // Define the perdicate
            var filter = Builders<BsonDocument>.Filter.Eq("_id", ObjectId.Parse(id));

            // Define the updated fields
            var update = Builders<BsonDocument>.Update.Set("title", document["title"]);

            // Update
            var result = await _collection.UpdateOneAsync(filter, update);

            return result.ModifiedCount;
        }

        public async Task<long> DeleteAsync(string id)
        {
            // Define the perdicate
            var filter = Builders<BsonDocument>.Filter.Eq("_id", ObjectId.Parse(id));

            // Delete
            var result = await _collection.DeleteOneAsync(filter);

            return result.DeletedCount;
        }
    }
}
