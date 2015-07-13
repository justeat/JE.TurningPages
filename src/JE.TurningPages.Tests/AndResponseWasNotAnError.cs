using System.Net;

namespace JE.TurningPages.Tests
{
    public abstract class AndResponseWasNotAnError : WhenAskingForPagedResponse
    {
        protected override HttpStatusCode GivenHttpResponseStatusCode()
        {
            return HttpStatusCode.OK;
        }
    }
}