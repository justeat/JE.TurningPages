using System.Net;
using Shouldly;

namespace JE.TurningPages.Examples.Tests
{
    public class does_not_deadlock : when_making_requests
    {
        public void to_simple_waiting_action_that_does_not_page()
        {
            act = () => Response = App.CreateRequest("/api/waiting").GetAsync().Result;
            it["should give 200 OK"] = () => Response.StatusCode.ShouldBe(HttpStatusCode.OK);
            it["should not have a link header"] = () => ShouldNotHaveHeader(Response, "link");
            it["should not have a pagination header"] = () => ShouldNotHaveHeader(Response, "x-pagination");
        }
    }
}