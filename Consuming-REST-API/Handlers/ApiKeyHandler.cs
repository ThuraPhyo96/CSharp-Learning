namespace Consuming_REST_API.Handlers
{
    public class ApiKeyHandler : DelegatingHandler
    {
        private readonly string _apiKey;
        private readonly string _userAgent;

        public ApiKeyHandler(string apiKey, string userAgent) 
        {
            _apiKey = apiKey;
            _userAgent = userAgent;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (!string.IsNullOrEmpty(_apiKey))
            {
                // Add the API key to the request headers
                request.Headers.Add("X-Api-Key", _apiKey);
                Console.WriteLine($"Added API Key: {_apiKey}");
            }

            if (!string.IsNullOrEmpty(_userAgent))
            {
                // Add the User-Agent header
                request.Headers.Add("User-Agent", _userAgent);
                Console.WriteLine($"Added User-Agent: {_userAgent}");
            }
            return await base.SendAsync(request, cancellationToken);
        }
    }
}
