using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Tracing;

namespace WebApplication1.Controllers
{
    public class TestController : ApiController
    {

        [Route("api/test")]
        [HttpGet]
        public IHttpActionResult getProjects()
        {
            Configuration.Services.GetTraceWriter().Trace(Request, "Incoming Request", TraceLevel.Debug, "Update the config file");
            return Ok();
        }
    }
}
