using System;
using System.Linq;
using System.Net.Http;
using JE.TurningPages.Contracts;
using NUnit.Framework;

namespace JE.TurningPages.Tests
{
    public class AndRequestHasPagingInfoInProperties : AndResponseWasNotAnError
    {
        protected override HttpRequestMessage GivenRequest()
        {
            var request = base.GivenRequest();
            request.Properties[PaginationInfo.PropertyKey] = GivenPaginationInfo();
            return request;
        }

        protected virtual PaginationInfo GivenPaginationInfo()
        {
            return new PaginationInfo();
        }

        [Test]
        public void ShouldHaveLinkHeader()
        {
            var isPresent =
                Response.Headers.Any(
                    x => x.Key.Equals(PageRequest.LinkHeader, StringComparison.InvariantCultureIgnoreCase));
            Assert.That(isPresent, Is.True);
        }

        [Test]
        public void ShouldHaveXPaginationHeader()
        {
            var isPresent =
                Response.Headers.Any(
                    x => x.Key.Equals(PageRequest.XPaginationHeader, StringComparison.InvariantCultureIgnoreCase));
            Assert.That(isPresent, Is.True);
        }
    }
}