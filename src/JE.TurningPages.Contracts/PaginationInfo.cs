using System;

namespace JE.TurningPages.Contracts
{
    public class PaginationInfo
    {
        public const int IndexOfFirstPage = 1;

        public const string PropertyKey = "PaginationInfo";

        public PaginationInfo()
        {
            Page = IndexOfFirstPage;
            PageSize = 10;
            TotalCount = 0;
        }

        public PaginationInfo(int page, int pageSize, int totalCount)
            : this()
        {
            Page = page;
            PageSize = pageSize;
            TotalCount = totalCount;
        }

        public int Page { get; private set; }

        public int PageSize { get; private set; }

        public int TotalCount { get; private set; }

        public int TotalPages
        {
            get
            {
                int remainder;
                var pages = Math.DivRem(TotalCount, PageSize, out remainder);
                return remainder == 0 ? pages : pages + 1;
            }
        }
    }
}