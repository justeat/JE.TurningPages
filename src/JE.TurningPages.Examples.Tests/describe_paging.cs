using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using Shouldly;

namespace JE.TurningPages.Examples.Tests
{
    public class describe_paging : when_making_requests
    {
        public void when_request_is_to_simple_waiting_action_that_does_not_page()
        {
            act = () => Response = App.CreateRequest("/api/waiting").GetAsync().Result;
            it["should give 200 OK"] = () => Response.StatusCode.ShouldBe(HttpStatusCode.OK);
            it["should not have a link header"] = () => ShouldNotHaveHeader(Response, "link");
            it["should not have a pagination header"] = () => ShouldNotHaveHeader(Response, "x-pagination");
        }

        public void when_request_is_to_operation_that_pages()
        {
            act = () => Response = App.CreateRequest("/api/widgets").GetAsync().Result;
            it["should have a link header"] = () => ShouldHaveHeader(Response, "link");
            it["should have a pagination header"] = () => ShouldHaveHeader(Response, "x-pagination");
        }

        private static void ShouldNotHaveHeader(HttpResponseMessage response, string header)
        {
            response.Headers.ShouldNotContain(x => x.Key.Equals(header, StringComparison.InvariantCultureIgnoreCase), () => string.Join("\n", response.Headers.Select(y => y.Key + " " + y.Value)));
        }

        private static void ShouldHaveHeader(HttpResponseMessage message, string header)
        {
            message.Headers.ShouldContain(x => x.Key.Equals(header, StringComparison.InvariantCultureIgnoreCase), () => string.Join("\n", message.Headers.Select(y => y.Key + " " + y.Value)));
        }
    }
}
