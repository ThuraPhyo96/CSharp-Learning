using Consuming_REST_API.Handlers;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Linq;
using System.Collections.Specialized;
using System.Reflection;
using System.Web;

class Program
{
    static async Task Main()
    {
        // Set up the DI container
        var services = new ServiceCollection();

        // Add HttpClientFactory with ApiKeyHandler
        services.AddHttpClient("NewsApi")
            .AddHttpMessageHandler(() => new RetryHandler())
            .AddHttpMessageHandler(() => new LoggingHandler())
            .AddHttpMessageHandler(() => new ApiKeyHandler(apiKey: "d87a2248207c4271a8bdd70cd91fb2e4", userAgent: "NewsAggregatorAPI/1.0"))
            .AddHttpMessageHandler(() => new ValidateHeaderHandler());

        // Add NewsApiService
        services.AddSingleton<NewsApiService>();

        // Build the service provider
        var serviceProvider = services.BuildServiceProvider();

        // Resolve the service and use it
        var newsApiService = serviceProvider.GetRequiredService<NewsApiService>();

        // Resolve IHttpClientFactory and use it
        var httpClientFactory = serviceProvider.GetRequiredService<IHttpClientFactory>();

        var client = httpClientFactory.CreateClient("NewsApi");

        FilterEverythingNews filterEverything = new()
        {
            HttpClient = client,
            Q = "bitcoin"
        };

        FilterTopheadlinesNews filterHeadlines = new()
        {
            HttpClient = client,
            Country = "us"
        };

        FilterSourcesNews filterSources = new()
        {
            HttpClient = client,
            Category = "science"
        };

        await NewsApiService.GetNewsAsync(filterEverything);
        await NewsApiService.GetSourcesAsync(filterSources);
    }

    public class NewsApiService()
    {
        private const string BaseUrl = "https://newsapi.org";

        public static async Task GetNewsAsync(FilterEverythingNews filter)
        {
            try
            {
                NameValueCollection queryParameters = ConvertToQueryParameters(filter);

                // Construct the full URL
                string url = $"{BaseUrl}{filter.Url}?{queryParameters}";

                HttpResponseMessage response = await filter.HttpClient!.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    string messageBody = await response.Content.ReadAsStringAsync();

                    JObject json = JObject.Parse(messageBody);
                    var articles = json["articles"];

                    Console.WriteLine("Top News Headlines\n");

                    foreach (var article in articles!)
                    {
                        string title = article["title"]?.ToString() ?? "No title";
                        string source = article["source"]?["name"]?.ToString() ?? "Unknown source";
                        string newsUrl = article["url"]?.ToString() ?? "#";

                        Console.WriteLine($"{title}");
                        Console.WriteLine($"Source: {source}");
                        Console.WriteLine($"Read more: {newsUrl}\n");
                    }
                }
                else
                {
                    HandleAPIError(response);
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Network Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        public static async Task GetSourcesAsync(FilterSourcesNews filter)
        {
            try
            {
                NameValueCollection queryParameters = ConvertToQueryParameters(filter);

                // Construct the full URL
                string url = $"{BaseUrl}{filter.Url}?{queryParameters}";

                HttpResponseMessage response = await filter.HttpClient!.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    string messageBody = await response.Content.ReadAsStringAsync();

                    JObject json = JObject.Parse(messageBody);
                    var articles = json["sources"];

                    Console.WriteLine("Top News Sources\n");

                    foreach (var article in articles!)
                    {
                        string title = article["name"]?.ToString() ?? "No name";
                        string source = article["description"]?.ToString() ?? "Unknown description";
                        string newsUrl = article["url"]?.ToString() ?? "#";

                        Console.WriteLine($"{title}");
                        Console.WriteLine($"Description: {source}");
                        Console.WriteLine($"Read more: {newsUrl}\n");
                    }
                }
                else
                {
                    HandleAPIError(response);
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Network Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }

    static void HandleAPIError(HttpResponseMessage responseMessage)
    {
        switch (responseMessage.StatusCode)
        {
            case System.Net.HttpStatusCode.BadRequest:
                Console.WriteLine("Bad Request: Check the API parameters.");
                break;
            case System.Net.HttpStatusCode.Unauthorized:
                Console.WriteLine("Unauthorized: Check your API key.");
                break;
            case System.Net.HttpStatusCode.Forbidden:
                Console.WriteLine("Forbidden: You might not have access.");
                break;
            case System.Net.HttpStatusCode.TooManyRequests:
                Console.WriteLine("Rate Limit Exceeded: Wait for a while and try again.");
                break;
            case System.Net.HttpStatusCode.InternalServerError:
                Console.WriteLine("Server error: Try again later.");
                break;
            default:
                Console.WriteLine($"Error {responseMessage.StatusCode}: Something went wrong.");
                break;
        }
    }

    public class FilterEverythingNews
    {
        public HttpClient? HttpClient { get; set; }
        public string Url { get; set; } = "/v2/everything";
        public string? Q { get; set; }
        public string? SearchIn { get; set; }
        public string? Sources { get; set; }
        public string? Domains { get; set; }
        public string? ExcludeDomains { get; set; }
        public string? From { get; set; }
        public string? To { get; set; }
        public string? Language { get; set; }
        public string? SortBy { get; set; }
        public string? PageSize { get; set; }
        public string? Page { get; set; }
    }

    public class FilterTopheadlinesNews
    {
        public HttpClient? HttpClient { get; set; }
        public string Url { get; set; } = "/v2/top-headlines";
        public string? Country { get; set; }
        public string? Category { get; set; }
        public string? Sources { get; set; }
        public string? Q { get; set; }
        public string? PageSize { get; set; }
        public string? Page { get; set; }
    }

    public class FilterSourcesNews
    {
        public HttpClient? HttpClient { get; set; }
        public string Url { get; set; } = "/v2/top-headlines/sources";
        public string? Country { get; set; }
        public string? Category { get; set; }
        public string? Sources { get; set; }
    }

    public static NameValueCollection ConvertToQueryParameters(object obj)
    {
        var queryParameters = HttpUtility.ParseQueryString(string.Empty);

        foreach (PropertyInfo property in obj.GetType().GetProperties())
        {
            string? value = property.GetValue(obj) as string;
            if (!string.IsNullOrEmpty(value) && property.Name != "Url") // Exclude URL from query params
            {
                queryParameters[property.Name.ToLower()] = HttpUtility.UrlEncode(value);
            }
        }

        return queryParameters;
    }
}