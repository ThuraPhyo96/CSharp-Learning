using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.CircuitBreaker;
using Polly.Extensions.Http;
using Polly.Fallback;
using Polly.Retry;
using System.Net;

namespace Consuming_REST_API.Handlers
{
    public class AddPoliciesToHttpClient
    {
        public static void AddPolicies(IServiceCollection services)
        {
            // Add HttpClientFactory with ApiKeyHandler
            services.AddHttpClient("NewsApi")
                .AddPolicyHandler(GetRetryPolicy())
                .AddPolicyHandler(GetCircuitBreakerPolicy())
                .AddPolicyHandler(GetFallbackPolicy())
                .AddHttpMessageHandler(() => new LoggingHandler())
                .AddHttpMessageHandler(() => new ApiKeyHandler(apiKey: "d87a2248207c4271a8bdd70cd91fb2e4", userAgent: "NewsAggregatorAPI/1.0"))
                .AddHttpMessageHandler(() => new ValidateHeaderHandler());
        }

        // Retry policy
        public static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
        {
            return HttpPolicyExtensions
              .HandleTransientHttpError()
              .Or<TimeoutException>()
              .WaitAndRetryAsync(
                  sleepDurations: new[]
                  {
                    TimeSpan.FromSeconds(1),
                    TimeSpan.FromSeconds(3),
                    TimeSpan.FromSeconds(5)
                  },
                  onRetry: (outcome, delay, retryAttempt, context) =>
                  {
                      Console.WriteLine($"Retry {retryAttempt} after {delay.TotalSeconds} seconds due to: {outcome.Exception?.Message ?? outcome.Result.StatusCode.ToString()}");
                  });
        }

        // Circuit Breaker policy
        public static IAsyncPolicy<HttpResponseMessage> GetCircuitBreakerPolicy()
        {
            return Policy<HttpResponseMessage>
                    .HandleResult(response => !response.IsSuccessStatusCode) // Handle any non-200 response
                    .CircuitBreakerAsync(2, TimeSpan.FromSeconds(30), // Break after 2 failures, reset after 30s
                        onBreak: (result, timeSpan) =>
                        {
                            Console.WriteLine($"[Circuit Breaker] Circuit broken due to: {result.Result.StatusCode}");
                        },
                        onReset: () =>
                        {
                            Console.WriteLine("[Circuit Breaker] Circuit reset.");
                        });
        }

        // Fallback policy
        public static IAsyncPolicy<HttpResponseMessage> GetFallbackPolicy()
        {
            return Policy<HttpResponseMessage>
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
        }

        public static IAsyncPolicy<HttpResponseMessage> GetCompositePolicy()
        {
            // 1. Retry Policy (innermost)
            var retryPolicy = HttpPolicyExtensions
                .HandleTransientHttpError()
                .Or<TimeoutException>()
                .WaitAndRetryAsync(
                    3,
                    retryAttempt => TimeSpan.FromMilliseconds(500 * Math.Pow(2, retryAttempt)),
                    onRetry: (outcome, delay, retryCount, context) =>
                    {
                        Console.WriteLine($"Retry attempt {retryCount} after {delay.TotalMilliseconds}ms for {outcome.Result?.StatusCode}");
                    });

            // 2. Circuit Breaker Policy (middle)
            var circuitBreakerPolicy = HttpPolicyExtensions
                .HandleTransientHttpError()
                .Or<TimeoutException>()
                .AdvancedCircuitBreakerAsync(
                    failureThreshold: 0.5,  // Break if 50% of requests fail
                    samplingDuration: TimeSpan.FromSeconds(10),
                    minimumThroughput: 2,    // Minimum number of requests in sampling duration
                    durationOfBreak: TimeSpan.FromSeconds(30),
                    onBreak: (outcome, breakDelay) =>
                    {
                        Console.WriteLine($"Circuit broken! Will not attempt for {breakDelay.TotalSeconds}s. Reason: {outcome.Exception?.Message ?? outcome.Result?.StatusCode.ToString()}");
                    },
                    onReset: () => Console.WriteLine("Circuit reset!"),
                    onHalfOpen: () => Console.WriteLine("Circuit half-open: Testing if recovered"));

            // 3. Fallback Policy (outermost)
            // 3. Fallback Policy (outermost)
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

            return Policy.WrapAsync(fallbackPolicy, circuitBreakerPolicy, retryPolicy);
        }
    }
}
