using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace Account.Client
{
    public class HttpAuthenticationHandler : AuthenticationHandler
    {
        protected virtual Task<AuthenticationHeaderValue> CreateAuthorizationHeaderAsync(
            HttpRequestMessage request, 
            HttpHeaderValueCollection<AuthenticationHeaderValue> challenges,  
            CancellationToken cancellationToken)
        {
            return Task.FromResult<AuthenticationHeaderValue>(null);
        }

        protected override async Task<bool> HandleUnauthorizedAsync(
            HttpRequestMessage request, HttpResponseMessage response, CancellationToken cancellationToken)
        {
            AuthenticationHeaderValue authorizationHeader =
                await CreateAuthorizationHeaderAsync(request, response.Headers.WwwAuthenticate, cancellationToken);
            if (authorizationHeader != null)
            {
                request.Headers.Authorization = authorizationHeader;
                return true;
            }
            return false;
        }

        protected override void AddAuthorizationHeader(HttpRequestMessage request)
        {
            AuthenticationHeaderValue authorizationHeader =AddAuthorization(request);
            if (authorizationHeader != null)
            {
                request.Headers.Authorization = authorizationHeader;
            }
        }

        protected virtual AuthenticationHeaderValue AddAuthorization(HttpRequestMessage request)
        {
            return null;
        }
    }
}
