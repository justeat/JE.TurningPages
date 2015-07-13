using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using JE.TurningPages.WebApi;

namespace JE.TurningPages.Tests
{
    public class PaginationLinkHeaderEnrichmentPublicForTesting : PaginationLinkHeaderEnrichment
    {
        public Task<HttpResponseMessage> SendAsyncInternal(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            return SendAsync(request, cancellationToken);
        }
    }
}
