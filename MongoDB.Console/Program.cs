using MongoDB.Bson;
using MongoDB.Console.Helpers;

class Program()
{
    private const string connectionString = "mongodb://localhost:27017";
    private const string databaseName = "NewsDB";
    private const string collectionName = "NewsArticles";

    public static async Task Main()
    {
        var mongoDbHelper = new MongoDBHelper(connectionString, databaseName, collectionName);

        // Create the new document to create
        var newsArticle = new BsonDocument
        {
            { "title" , "MongoDB News Integration"},
            { "author" , "Thura Phyo"},
            { "content" , "Learning MongoDB with .NET"},
            { "published_date" , DateTime.Now},
        };

        await mongoDbHelper.InsertAsync(newsArticle);

        var articles = await mongoDbHelper.GetAllAsync();
        Console.WriteLine("News Articels \n");
        foreach (var article in articles)
        {
            Console.WriteLine(article.ToJson());
        }

        if (articles != null && articles.Count > 0)
        {
            string? id = articles[0]["_id"]?.ToString();
            var updateField = new BsonDocument { { "title", "Updated the title" } };
            var updatedDocument = await mongoDbHelper.UpdateAsync(id!, updateField);

            Console.WriteLine(updatedDocument > 0 ? "Document updated successfully." : "No document updated.");
        }

        if (articles != null && articles.Count > 0)
        {
            string? id = articles![0]["_id"].ToString();
            var deletedDocument = await mongoDbHelper.DeleteAsync(id!);

            if (deletedDocument > 0)
                Console.WriteLine("Document deleted successfully.");
            else
                Console.WriteLine("No document found.");
        }

        //await CreateManyAsync();
        //await GetAllAsyncBy();
        //await UpdateManyAsync();
        //await DeleteManyAsync();
    }

    public static async Task CreateManyAsync()
    {
        var mongoDbHelper = new MongoDBHelper(connectionString, databaseName, collectionName);

        var newsArticels = new BsonDocument[]
                {
                  new() {
                        { "title" , "MongoDB News Integration"},
                        { "author" , "Thura Phyo"},
                        { "content" , "Learning MongoDB with .NET"},
                        { "published_date" , DateTime.Now},
                  },
                  new()   {
                        { "title" , "MongoDB News Integration With Many"},
                        { "author" , "Thura Phyo"},
                        { "content" , "Learning MongoDB with .NET"},
                        { "published_date" , DateTime.Now},
                  },
                };

        await mongoDbHelper.InsertManyAsync(newsArticels);
    }

    public static async Task GetAllAsyncBy()
    {
        var mongoDbHelper = new MongoDBHelper(connectionString, databaseName, collectionName);
        MongoDBHelper.FilterDto filter = new MongoDBHelper.FilterDto("MongoDB News Integration", "", "", "");

        var articles = await mongoDbHelper.GetAllAsyncBy(filter);
        Console.WriteLine("News Articels \n");
        foreach (var article in articles)
        {
            Console.WriteLine(article.ToJson());
        }
    }

    public static async Task UpdateManyAsync()
    {
        var mongoDbHelper = new MongoDBHelper(connectionString, databaseName, collectionName);
        MongoDBHelper.FilterDto filter = new("", "", "", "");

        var articles = await mongoDbHelper.GetAllAsyncBy(filter);
        string keyword = "Thura Phyo";
        if (articles != null && articles.Count > 0)
        {
            var updateField = new BsonDocument { { "author", "Updated the author" } };
            var updatedDocument = await mongoDbHelper.UpdateManyAsync(keyword, updateField);

            Console.WriteLine(updatedDocument > 0 ? "Document updated successfully." : "No document updated.");
        }
    }

    public static async Task DeleteManyAsync()
    {
        var mongoDbHelper = new MongoDBHelper(connectionString, databaseName, collectionName);
        string keyword = "Updated the author";
        var deletedDocument = await mongoDbHelper.DeleteManyAsync(keyword);
        Console.WriteLine(deletedDocument > 0 ? "Document deleted successfully." : "No document deleted.");
    }
}