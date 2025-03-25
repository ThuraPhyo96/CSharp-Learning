using System.Net;

namespace Consuming_REST_API.Handlers
{
    public class RetryHandler : DelegatingHandler
    {
        private readonly int _maxRetries;
        private readonly TimeSpan _initialDelay;

        public RetryHandler(int maxRetries = 3, int initialDelayMilliseconds = 500)
        {
            _maxRetries = maxRetries;
            _initialDelay = TimeSpan.FromMilliseconds(initialDelayMilliseconds);
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            int retryCount = 0;

            while (true)
            {
                try
                {
                    HttpResponseMessage response = await base.SendAsync(request, cancellationToken);

                    if (response.IsSuccessStatusCode)
                    {
                        return response;
                    }

                    if (!IsTransientFailure(response.StatusCode))
                    {
                        return response;
                    }
                }
                catch (HttpRequestException ex) when (retryCount < _maxRetries)
                {
                    Console.WriteLine($"Network error: {ex.Message}");
                }

                retryCount++;

                if (retryCount > _maxRetries)
                {
                    Console.WriteLine("Max retry attempts reached. Request failed.");
                    throw new HttpRequestException("Max retries exceeded.");
                }

                TimeSpan delay = GetExponentialBackoffDelay(retryCount);
                Console.WriteLine($"Retrying request ({retryCount}/{_maxRetries}) in {delay.TotalMilliseconds}ms...");
                await Task.Delay(delay, cancellationToken);
            }
        }

        private static bool IsTransientFailure(HttpStatusCode statusCode)
        {
            return statusCode == HttpStatusCode.RequestTimeout ||  // 408
                   ((int)statusCode >= 500 && (int)statusCode < 600); // 5xx
        }

        private TimeSpan GetExponentialBackoffDelay(int retryCount)
        {
            double exponentialDelay = _initialDelay.TotalMilliseconds * Math.Pow(2, retryCount);
            return TimeSpan.FromMilliseconds(Math.Min(exponentialDelay, 5000)); // Max 5 sec delay
        }
    }
}
