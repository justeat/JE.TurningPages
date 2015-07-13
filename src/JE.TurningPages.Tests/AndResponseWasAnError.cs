using System;
using System.Linq;
using System.Net;
using JE.TurningPages.Contracts;
using NUnit.Framework;

namespace JE.TurningPages.Tests
{
    public class AndResponseWasAnError : WhenAskingForPagedResponse
    {
        protected override HttpStatusCode GivenHttpResponseStatusCode()
        {
            return HttpStatusCode.Unauthorized;
        }

        [Test]
        public void ShouldNotHaveLinkHeader()
        {
            var isPresent =
                Response.Headers.Any(
                    x => x.Key.Equals(PageRequest.LinkHeader, StringComparison.InvariantCultureIgnoreCase));
            Assert.That(isPresent, Is.False);
        }

        [Test]
        public void ShouldNotHaveXPaginationHeader()
        {
            var isPresent =
                Response.Headers.Any(
                    x => x.Key.Equals(PageRequest.XPaginationHeader, StringComparison.InvariantCultureIgnoreCase));
            Assert.That(isPresent, Is.False);
        }
    }
}