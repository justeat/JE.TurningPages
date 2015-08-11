namespace JE.TurningPages.Examples.Tests
{
    public class describe_paging : when_making_requests
    {
        public void to_operation_that_pages()
        {
            act = () => Response = App.CreateRequest("/api/widgets").GetAsync().Result;
            it["should have a link header"] = () => ShouldHaveHeader(Response, "link");
            it["should have a pagination header"] = () => ShouldHaveHeader(Response, "x-pagination");
        }
    }
}
