using System.Threading.Tasks;

namespace JE.TurningPages.Examples.Tests
{
    public class describe_paging : when_making_requests
    {
        public async Task to_operation_that_pages()
        {
            act = async () => Response = await App.CreateRequest("/api/widgets").GetAsync().ConfigureAwait(true);
            it["should have a link header"] = () => ShouldHaveHeader(Response, "link");
            it["should have a pagination header"] = () => ShouldHaveHeader(Response, "x-pagination");
        }
    }
}
