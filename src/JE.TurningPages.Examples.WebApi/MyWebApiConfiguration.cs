using System.Web.Http;
using JE.TurningPages.WebApi;

namespace JE.TurningPages.Examples.WebApi
{
    public class MyWebApiConfiguration : HttpConfiguration
    {
        public MyWebApiConfiguration()
        {
            ConfigureRouting();
            MessageHandlers.Add(new PaginationLinkHeaderEnrichment());
        }

        private void ConfigureRouting()
        {
            Routes.MapHttpRoute("DefaultApi", "api/{controller}/{id}", new { id = RouteParameter.Optional });
        }
    }
}