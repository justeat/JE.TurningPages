using System;
using System.Net.Http;
using System.Threading.Tasks;
using Shouldly;

namespace JE.TurningPages.Examples.Tests
{
    public class describe_paging : when_making_requests
    {
        public async Task when_request_is_successful()
        {
            act = async () => Response = await App.CreateRequest("/widgets").GetAsync().ConfigureAwait(false);
            it["should have a link header"] = () => ShouldHaveHeader(Response, "link");
            it["should have a pagination header"] = () => ShouldHaveHeader(Response, "x-pagination");
        }

        private static void ShouldHaveHeader(HttpResponseMessage message, string header)
        {
            message.Headers.ShouldContain(x => x.Key.Equals(header, StringComparison.InvariantCultureIgnoreCase));
        }
    }
}
