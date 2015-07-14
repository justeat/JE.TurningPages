using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using JE.TurningPages.Contracts;
using JE.TurningPages.WebApi;

namespace JE.TurningPages.Examples.WebApi.Widgets
{
    public class WidgetsController : ApiController
    {
        private const int MaxPageSizeFromImaginaryConfiguration = 3;

        [HttpGet]
        [ResponseType(typeof(IEnumerable<Widget>))]
        public Task<IHttpActionResult> Get([FromUri] PageRequest paging = null)
        {
            if (paging == null) { paging = new PageRequest(); }
            var all = FetchData();
            var historyPage = PageData(paging, all);
            var pageInfo = CreatePageInfo(paging, all);
            var result = Ok(historyPage);
            Request.Properties[PaginationInfo.PropertyKey] = pageInfo;
            return new Task<IHttpActionResult>(() => Ok(result));
        }

        private static PaginationInfo CreatePageInfo(PageRequest paging, IEnumerable<Widget> all)
        {
            return new PaginationInfo(
                paging.Page,
                paging.AllowedPageSize(MaxPageSizeFromImaginaryConfiguration),
                all.Count());
        }

        private static IList<Widget> PageData(PageRequest paging, IEnumerable<Widget> data)
        {
            return data.Page(paging.AllowedPageSize(MaxPageSizeFromImaginaryConfiguration), paging.Page).ToList();
        }

        private static IList<Widget> FetchData()
        {
            return new List<Widget>
                       {
                           new Widget { Name = "foo" },
                           new Widget { Name = "bar" },
                           new Widget { Name = "baz" },
                           new Widget { Name = "wibble" },
                           new Widget { Name = "grumpy" },
                           new Widget { Name = "sneezy" },
                           new Widget { Name = "doc" }
                       };
        }
    }
}
