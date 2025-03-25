namespace Consuming_REST_API.Handlers
{
    public class LoggingHandler : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            // Log the Request
            Console.WriteLine("=== HTTP REQUEST ===");
            Console.WriteLine($"{request.Method} {request.RequestUri}");

            foreach (var header in request.Headers)
            {
                Console.WriteLine($"Header: {header.Key} = {string.Join(", ", header.Value)}");
            }

            if (request.Content != null)
            {
                string requestBody = await request.Content.ReadAsStringAsync();
                Console.WriteLine($"Request Body: {requestBody}");
            }

            Console.WriteLine("====================\n");

            // Call the inner handler
            HttpResponseMessage response = await base.SendAsync(request, cancellationToken);

            // Log the Response
            Console.WriteLine("=== HTTP RESPONSE ===");
            Console.WriteLine($"Status Code: {response.StatusCode}");

            foreach (var header in response.Headers)
            {
                Console.WriteLine($"Header: {header.Key} = {string.Join(", ", header.Value)}");
            }

            if (response.Content != null)
            {
                string responseBody = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Response Body: {responseBody}");
            }

            Console.WriteLine("====================\n");

            return response;
        }
    }
}
