using MongoDB.Bson;
using MongoDB.Console.Helpers;

class Program()
{
    public static async Task Main()
    {
        string connectionString = "mongodb://localhost:27017";
        string databaseName = "NewsDB";
        string collectionName = "NewsArticles";

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
    }
}