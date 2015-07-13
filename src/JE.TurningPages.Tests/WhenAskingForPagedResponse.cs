using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using SpecsFor;

namespace JE.TurningPages.Tests
{
    public abstract class WhenAskingForPagedResponse : SpecsFor<PaginationLinkHeaderEnrichmentPublicForTesting>
    {
        private HttpRequestMessage _request;

        protected HttpResponseMessage Response;

        protected override void Given()
        {
            _request = GivenRequest();
            base.Given();
        }

        protected virtual HttpRequestMessage GivenRequest()
        {
            return new HttpRequestMessage(HttpMethod.Get, GivenRequestUri());
        }

        protected virtual Uri GivenRequestUri()
        {
            return new Uri("http://localhost/foo?page=3&pageSize=20");
        }

        protected override void When()
        {
            Response = SUT.SendAsyncInternal(_request, new CancellationToken()).Result;
            base.When();
        }

        protected override void InitializeClassUnderTest()
        {
            SUT = new PaginationLinkHeaderEnrichmentPublicForTesting
                      {
                          InnerHandler =
                              new DummyMessageHandlerToSimulateResponse(
                              GivenHttpResponseStatusCode())
                      };
        }

        protected abstract HttpStatusCode GivenHttpResponseStatusCode();
    }
}