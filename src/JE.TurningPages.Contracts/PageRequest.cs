namespace JE.TurningPages.Contracts
{
    public class PageRequest
    {
        public const int DefaultPageSize = 20;

        public const string LinkHeader = "Link";

        public const string XPaginationHeader = "X-Pagination";

        // ReSharper disable RedundantArgumentDefaultValue
        public PageRequest()
            : this(1, DefaultPageSize)
            // ReSharper restore RedundantArgumentDefaultValue
        {
        }

        public PageRequest(int? page = 1, int? pageSize = DefaultPageSize)
        {
            Page = page ?? 1;
            PageSize = pageSize ?? DefaultPageSize;
        }

        public int Page { get; set; }

        public int PageSize { get; set; }

        public int AllowedPageSize(int maxSize)
        {
            return PageSize > maxSize ? maxSize : PageSize;
        }
    }
}
