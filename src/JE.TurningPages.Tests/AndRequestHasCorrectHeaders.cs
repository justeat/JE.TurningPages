using System;
using System.Linq;
using JE.TurningPages.Contracts;
using Newtonsoft.Json;
using NUnit.Framework;
using Shouldly;

namespace JE.TurningPages.Tests
{
    public class AndRequestHasCorrectHeaders : AndRequestHasPagingInfoInProperties
    {
        protected string Link;

        private string _xpagination;

        protected override void When()
        {
            base.When();
            Link = GetHeader(PageRequest.LinkHeader);
            _xpagination = GetHeader(PageRequest.XPaginationHeader);
        }

        private string GetHeader(string headerName)
        {
            return
                Response.Headers.Single(x => x.Key.Equals(headerName, StringComparison.InvariantCultureIgnoreCase))
                    .Value.Single();
        }

        [Test]
        public void ShouldHaveJsonXPagination()
        {
            var parsed = JsonConvert.DeserializeAnonymousType(
                _xpagination,
                new { Page = 0, PageSize = 0, TotalCount = 0 });
            parsed.Page.ShouldBe(GivenPaginationInfo().Page);
            parsed.PageSize.ShouldBe(GivenPaginationInfo().PageSize);
            parsed.TotalCount.ShouldBe(GivenPaginationInfo().TotalCount);
        }
    }
}