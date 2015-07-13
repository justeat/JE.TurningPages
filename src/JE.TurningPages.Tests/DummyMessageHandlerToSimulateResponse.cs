using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace JE.TurningPages.Tests
{
    internal class DummyMessageHandlerToSimulateResponse : DelegatingHandler
    {
        private readonly HttpStatusCode _httpStatusCode;

        private readonly TaskFactory<HttpResponseMessage> _taskFactory;

        public DummyMessageHandlerToSimulateResponse(HttpStatusCode httpStatusCode)
        {
            _taskFactory = new TaskFactory<HttpResponseMessage>();
            _httpStatusCode = httpStatusCode;
        }

        protected override Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            return _taskFactory.StartNew(() => new HttpResponseMessage(_httpStatusCode), cancellationToken);
        }
    }
}