using System;
using System.Net.Http;
using JE.TurningPages.Examples.WebApi;
using Microsoft.Owin.Testing;
using NUnit.Framework;

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
    }
}
