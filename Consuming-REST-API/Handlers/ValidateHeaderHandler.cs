namespace Consuming_REST_API.Handlers
{
    public class ValidateHeaderHandler : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (request.Headers.TryGetValues("X-Api-Key", out var apiKeyValues))
            {
                string apiKey = apiKeyValues.FirstOrDefault() ?? "No API Key Provided";
                Console.WriteLine($"Validated API Key: {apiKey}");
            }
            else
            {
                Console.WriteLine("Missing API Key in the request headers.");
                throw new HttpRequestException("API Key is required.");
            }

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
