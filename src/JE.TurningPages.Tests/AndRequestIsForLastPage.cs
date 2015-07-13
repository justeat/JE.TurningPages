using JE.TurningPages.Contracts;
using NUnit.Framework;

namespace JE.TurningPages.Tests
{
    public class AndRequestIsForLastPage : AndRequestHasCorrectHeaders
    {
        protected override PaginationInfo GivenPaginationInfo()
        {
            return new PaginationInfo(6, 10, 57);
        }

        [Test]
        public void ShouldHavePreviousLink()
        {
            StringAssert.Contains("previous: </foo?page=5&pageSize=10>", Link);
        }

        [Test]
        public void ShouldNotHaveLastLink()
        {
            StringAssert.DoesNotContain("last:", Link);
        }

        [Test]
        public void ShouldNotHaveNextLink()
        {
            StringAssert.DoesNotContain("next:", Link);
        }
    }
}