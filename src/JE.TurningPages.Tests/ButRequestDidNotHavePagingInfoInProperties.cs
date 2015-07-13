using System;
using System.Linq;
using JE.TurningPages.Contracts;
using NUnit.Framework;

namespace JE.TurningPages.Tests
{
    public class ButRequestDidNotHavePagingInfoInProperties : AndResponseWasNotAnError
    {
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