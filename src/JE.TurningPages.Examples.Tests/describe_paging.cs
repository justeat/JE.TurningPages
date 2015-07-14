using System;
using System.Threading.Tasks;
using Shouldly;

namespace JE.TurningPages.Examples.Tests
{
    public class describe_paging : when_making_requests
    {
        public async Task when_request_is_successful()
        {
            act = () => Response = App.CreateRequest("/widgets").GetAsync().Result;
            it["should have a link header"] = () => Response.Headers.ShouldContain(x => x.Key.Equals("link", StringComparison.InvariantCultureIgnoreCase));
            it["should have a pagination header"] = () => Response.Headers.ShouldContain(x => x.Key.Equals("x-pagination", StringComparison.InvariantCultureIgnoreCase));
        }
    }
}