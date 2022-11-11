using Microsoft.Extensions.Options;
using System.Web;

namespace NF.ExchangeRates.CurrencyLayer
{
    public class CurrencyLayerAuthorizationHandler : DelegatingHandler
    {
        private readonly CurrencyLayerOptions _options;

        public CurrencyLayerAuthorizationHandler(IOptions<CurrencyLayerOptions> options) => _options = options.Value;

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            if (request.RequestUri is null)
            {
                throw new ArgumentException("Request URI is not provided.");
            }

            var parameters = HttpUtility.ParseQueryString(request.RequestUri.Query);

            parameters.Remove(accessKey);
            parameters.Add(accessKey, _options.AccessKey);

            var tuples = parameters.Cast<string>()
                .SelectMany(key => parameters.GetValues(key), (name, value) => (name, value));

            var newQuery = string.Join("&",
                tuples.Select(tuple => tuple.name == null ? tuple.value : $"{tuple.name}={tuple.value}"));

            request.RequestUri = new Uri(request.RequestUri.GetLeftPart(UriPartial.Path) + "?" + newQuery);

            return base.SendAsync(request, cancellationToken);
        }

        private const string accessKey = "access_key";
    }
}
