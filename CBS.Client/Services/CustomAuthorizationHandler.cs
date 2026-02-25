using Blazored.LocalStorage;
using System.Net.Http.Headers;

namespace CBS.Client.Services
{
    /// <summary>
    /// DelegatingHandler that reads the JWT stored in localStorage after login
    /// and attaches it as an Authorization: Bearer header on every outgoing
    /// HTTP request made through the "cbs-api" named HttpClient.
    /// </summary>
    public class CustomAuthorizationHandler : DelegatingHandler
    {
        private readonly ILocalStorageService _localStorage;

        public CustomAuthorizationHandler(ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
        }

        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request, CancellationToken cancellationToken)
        {
            // Read the token saved by IdentityClientService after a successful login
            var token =await _localStorage.GetItemAsync<string>("access_token");

            if (!string.IsNullOrWhiteSpace(token))
            {
                request.Headers.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);
            }

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
