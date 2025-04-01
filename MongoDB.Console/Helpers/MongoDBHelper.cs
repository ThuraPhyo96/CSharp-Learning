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

        public async Task InsertManyAsync(BsonDocument[] documents)
        {
            await _collection.InsertManyAsync(documents);
        }

        // Read all document
        public async Task<List<BsonDocument>> GetAllAsync()
        {
            return await _collection.Find(new BsonDocument()).ToListAsync();
        }

        public async Task<List<BsonDocument>> GetAllAsyncBy(FilterDto input)
        {
            var builder = Builders<BsonDocument>.Filter;
            var filters = new List<FilterDefinition<BsonDocument>>();

            if (!string.IsNullOrEmpty(input.Title))
                filters.Add(builder.Eq("title", input.Title));

            if (!string.IsNullOrEmpty(input.Author))
                filters.Add(builder.Eq("author", input.Author.Trim()));

            if (!string.IsNullOrEmpty(input.Published_Date))
            {
                if (DateTime.TryParse(input.Published_Date, out var publishedDate))
                {
                    filters.Add(builder.Eq("published_date", publishedDate));
                }
            }

            var filter = filters.Count == 0 ? builder.Empty : builder.And(filters);
            return await _collection.Find(filter).ToListAsync();
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

        public async Task<long> UpdateManyAsync(string keyword, BsonDocument document)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("author", keyword);
            var update = Builders<BsonDocument>.Update
                                               .Set("author", document["author"])
                                               .CurrentDate("published_date");

            var result = await _collection.UpdateManyAsync(filter, update);
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

        public async Task<long> DeleteManyAsync(string keyword)
        {
            // Define the perdicate
            var filter = Builders<BsonDocument>.Filter.Eq("author", keyword);
            var result = await _collection.DeleteManyAsync(filter);
            return result.DeletedCount;
        }

        public record FilterDto(string Title, string Author, string Content, string Published_Date);
    }
}
