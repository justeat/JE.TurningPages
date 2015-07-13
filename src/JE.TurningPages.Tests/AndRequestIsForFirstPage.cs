using JE.TurningPages.Contracts;
using NUnit.Framework;

namespace JE.TurningPages.Tests
{
    public class AndRequestIsForFirstPage : AndRequestHasCorrectHeaders
    {
        protected override PaginationInfo GivenPaginationInfo()
        {
            return new PaginationInfo(1, 10, 57);
        }

        [Test]
        public void ShouldHaveLastLink()
        {
            StringAssert.Contains("last: </foo?page=6&pageSize=10>", Link);
        }

        [Test]
        public void ShouldHaveNextLink()
        {
            StringAssert.Contains("next: </foo?page=2&pageSize=10>", Link);
        }

        [Test]
        public void ShouldHaveNotPreviousLink()
        {
            StringAssert.DoesNotContain("previous:", Link);
        }
    }
}