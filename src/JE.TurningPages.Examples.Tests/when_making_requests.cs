using System;
using System.Linq;
using System.Net.Http;
using JE.TurningPages.Examples.WebApi;
using Microsoft.Owin.Testing;
using Shouldly;

namespace JE.TurningPages.Examples.Tests
{
    public abstract class when_making_requests : nspec_base, IDisposable
    {
        protected TestServer App;

        protected HttpResponseMessage Response;

        public virtual void before_each()
        {
            App = TestServer.Create<Startup>();
        }

        public virtual void after_each()
        {
            if (App != null) { App.Dispose(); }
        }

        public void Dispose()
        {
            if (App != null) { App.Dispose(); }
        }

        protected static void ShouldNotHaveHeader(HttpResponseMessage response, string header)
        {
            response.Headers.ShouldNotContain(x => x.Key.Equals(header, StringComparison.InvariantCultureIgnoreCase), () => string.Join("\n", response.Headers.Select(y => y.Key + " " + y.Value)));
        }

        protected static void ShouldHaveHeader(HttpResponseMessage message, string header)
        {
            message.Headers.ShouldContain(x => x.Key.Equals(header, StringComparison.InvariantCultureIgnoreCase), () => string.Join("\n", message.Headers.Select(y => y.Key + " " + y.Value)));
        }
    }
}
