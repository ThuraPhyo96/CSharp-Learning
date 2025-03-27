using Consuming_REST_API.Handlers;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Linq;
using Polly;
using Polly.Extensions.Http;
using System.Collections.Specialized;
using System.Net;
using System.Reflection;
using System.Web;

class Program
{
    static async Task Main()
    {
        // Set up the DI container
        var services = new ServiceCollection();

        //AddPoliciesToHttpClient.AddPolicies(services);

        // Define Polly Retry Policy
        //var retryPolicy = HttpPolicyExtensions
        //    .HandleTransientHttpError() // Handles 5xx and network failures
        //    .WaitAndRetryAsync(3, retryAttempt =>
        //        TimeSpan.FromMilliseconds(500 * Math.Pow(2, retryAttempt)), // Exponential Backoff
        //        (outcome, delay, retryCount, context) =>
        //        {
        //            Console.WriteLine($"Retry {retryCount} after {delay.TotalMilliseconds}ms");
        //        });



        // Add HttpClientFactory with ApiKeyHandler
        services.AddHttpClient("NewsApi")
              //.AddHttpMessageHandler(() => new RetryHandler())
              .AddPolicyHandler(AddPoliciesToHttpClient.GetCompositePolicy())
             // .AddPolicyHandler(AddPoliciesToHttpClient.GetRetryPolicy()) // Add Polly retry policy
             //.AddPolicyHandler(AddPoliciesToHttpClient.GetCircuitBreakerPolicy())
             //.AddPolicyHandler(AddPoliciesToHttpClient.GetFallbackPolicy())
             .AddHttpMessageHandler(() => new LoggingHandler())
             .AddHttpMessageHandler(() => new ApiKeyHandler(apiKey: "d87a2248207c4271a8bdd70cd91fb2e4", userAgent: "NewsAggregatorAPI/1.0"))
             .AddHttpMessageHandler(() => new ValidateHeaderHandler())
             ;

        // Add NewsApiService
        services.AddSingleton<NewsApiService>();

        // Build the service provider
        var serviceProvider = services.BuildServiceProvider();

        // Resolve the service and use it
        serviceProvider.GetRequiredService<NewsApiService>();

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

        //await NewsApiService.GetNewsAsync(filterEverything);
        //await NewsApiService.GetSourcesAsync(filterSources);

        //var policy = AddPoliciesToHttpClient.GetFallbackPolicy();

        //var response = await policy.ExecuteAsync(() =>
        //    client.GetAsync("https://invalid-url-123456789.com")
        //);

        //await TestFallback();

        await TestCircuitBreaker();
    }

    public class NewsApiService()
    {
        // test retry handler: base url https://httpbin.org
        private const string BaseUrl = "https://httpbin.org";

        public static async Task GetNewsAsync(FilterEverythingNews filter)
        {
            try
            {
                NameValueCollection queryParameters = ConvertToQueryParameters(filter);

                // Construct the full URL
                string url = $"{BaseUrl}{filter.Url}?{queryParameters}";

                HttpResponseMessage response = await filter.HttpClient!.GetAsync("http://nonexistent-domain");

                if (response.Headers.Contains("X-Fallback-Reason"))
                {
                    var reason = response.Headers.GetValues("X-Fallback-Reason").First();
                    var content = await response.Content.ReadAsStringAsync();

                    Console.WriteLine($"\nFallback Content (Reason: {reason}) ");
                    Console.WriteLine(content);

                    // Optional: Show cached data or alternative content
                    Console.WriteLine("\nShowing cached results...");
                    return;
                    // Handle fallback scenario specifically
                }

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
        // test retry handler: url /status/500
        public HttpClient? HttpClient { get; set; }
        public string Url { get; set; } = "/status/500";
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


    private static readonly HttpClient _httpClient = new HttpClient();

    public static async Task TestFallback()
    {

        var fallbackPolicy = Policy<HttpResponseMessage>
           .Handle<HttpRequestException>() // Handle HTTP request exceptions
           .OrResult(response =>
               !response.IsSuccessStatusCode || // Handle non-success status codes
               response.StatusCode == HttpStatusCode.NotFound || // Specific status code
               response.Content == null) // Handle null content
           .FallbackAsync(
               fallbackAction: async (outcome, context, token) =>
               {
                   // Create a fallback response
                   var fallbackResponse = new HttpResponseMessage(HttpStatusCode.OK)
                   {
                       Content = new StringContent("Fallback content - Service unavailable")
                   };

                   // Add custom header to indicate this is a fallback
                   fallbackResponse.Headers.Add("X-Fallback-Reason",
                       outcome.Exception?.Message ?? outcome.Result?.StatusCode.ToString() ?? "Unknown error");

                   return await Task.FromResult(fallbackResponse);
               },
               onFallbackAsync: async (outcome, context) =>
               {
                   // This runs when the fallback is triggered
                   Console.WriteLine($"Fallback triggered due to: {outcome.Exception?.Message ?? outcome.Result?.StatusCode.ToString()}");
                   await Task.CompletedTask;
               });


        try
        {
            // Example 1: Fallback for a failed HTTP request
            var response1 = await fallbackPolicy.ExecuteAsync(() =>
                _httpClient.GetAsync("https://httpbin.org/status/500")); // This will return 500 status code

            Console.WriteLine($"Response 1 Status: {response1.StatusCode}");
            Console.WriteLine($"Response 1 Content: {await response1.Content.ReadAsStringAsync()}");
            Console.WriteLine($"Is Fallback: {response1.Headers.Contains("X-Fallback-Reason")}");

            // Example 2: Fallback for a 404 Not Found
            var response2 = await fallbackPolicy.ExecuteAsync(() =>
                _httpClient.GetAsync("https://httpbin.org/status/404")); // This will return 404 status code

            Console.WriteLine($"\nResponse 2 Status: {response2.StatusCode}");
            Console.WriteLine($"Response 2 Content: {await response2.Content.ReadAsStringAsync()}");
            Console.WriteLine($"Is Fallback: {response2.Headers.Contains("X-Fallback-Reason")}");

            // Example 3: Fallback for an exception
            var response3 = await fallbackPolicy.ExecuteAsync(() =>
                _httpClient.GetAsync("https://nonexistentdomain12345.com")); // This will throw an exception

            Console.WriteLine($"\nResponse 3 Status: {response3.StatusCode}");
            Console.WriteLine($"Response 3 Content: {await response3.Content.ReadAsStringAsync()}");
            Console.WriteLine($"Is Fallback: {response3.Headers.Contains("X-Fallback-Reason")}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Unexpected error: {ex.Message}");
        }

        Console.WriteLine("\nPress any key to exit...");
        Console.ReadKey();
    }

    public static async Task TestCircuitBreaker()
    {
        var services = new ServiceCollection();
        services.AddHttpClient("TestClient")
            .AddPolicyHandler(AddPoliciesToHttpClient.GetCompositePolicy());

        var provider = services.BuildServiceProvider();
        var factory = provider.GetRequiredService<IHttpClientFactory>();
        var client = factory.CreateClient("TestClient");

        // Test with rapid consecutive failures
        for (int i = 0; i < 5; i++)
        {
            try
            {
                Console.WriteLine($"\nAttempt {i + 1}");
                var response = await client.GetAsync("https://httpbin.org/status/500");
                Console.WriteLine($"Status: {response.StatusCode}");
                Console.WriteLine($"Is Fallback: {response.Headers.Contains("X-Fallback-Reason")}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            await Task.Delay(300); // Small delay between requests
        }
    }

}