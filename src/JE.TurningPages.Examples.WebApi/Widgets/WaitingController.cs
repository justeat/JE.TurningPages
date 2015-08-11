using System.Threading.Tasks;
using System.Web.Http;

namespace JE.TurningPages.Examples.WebApi.Widgets
{
    public class WaitingController : ApiController
    {
        [HttpGet]
        public async Task<IHttpActionResult> Search()
        {
            var result = await Task.Factory.StartNew(() => Task.Delay(1000));
            return Ok();
        }
    }
}