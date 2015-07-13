using JE.TurningPages.Contracts;
using NUnit.Framework;

namespace JE.TurningPages.Tests
{
    public class AndRequestIsForPageNearMiddle : AndRequestHasCorrectHeaders
    {
        protected override PaginationInfo GivenPaginationInfo()
        {
            return new PaginationInfo(3, 10, 57);
        }

        [Test]
        public void ShouldHaveLastLink()
        {
            StringAssert.Contains("last: </foo?page=6&pageSize=10>", Link);
        }

        [Test]
        public void ShouldHaveNextLink()
        {
            StringAssert.Contains("next: </foo?page=4&pageSize=10>", Link);
        }

        [Test]
        public void ShouldHavePreviousLink()
        {
            StringAssert.Contains("previous: </foo?page=2&pageSize=10>", Link);
        }
    }
}