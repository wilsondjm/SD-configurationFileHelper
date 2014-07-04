using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using ConfigurationFileHandler;
using System.Threading.Tasks;
using ConfigurationFileHandler.Utils;
using System.Net.Http.Headers;
using WebApplication1.Models.Impl;
using System.Web.Http.Tracing;

namespace WebApplication1.Controllers
{
    public class JobConfigurationController : ApiController
    {
        IConfigService configService;
        
        public JobConfigurationController(IConfigService _configService)
        {
            configService = _configService;
        }

       
        [Route("Configurations/{projectName}")]
        [HttpGet]
        public HttpResponseMessage Get(string projectName)
        {
            Configuration.Services.GetTraceWriter().Trace(Request, "Incoming Request", TraceLevel.Debug, "Received the fetch configuration request for " + projectName);
            ActionResult<string> result = configService.GetConfiguration(projectName);
             if(result == null)
            {
                var responseMessageBad = Request.CreateResponse<string>(HttpStatusCode.NotFound, "Failed to locate the config file");
                return responseMessageBad;
            }

            string config = result.Data;
            var responseMessage = Request.CreateResponse<string>(HttpStatusCode.OK, config);
            return responseMessage;
        }


        [Route("Configurations")]
        [HttpPost]
        public HttpResponseMessage Post(JobConfigEntity value)
        {
            var result = configService.AddorUpdateConfiguration(value.JobName, value.Configuration);
            if(result.Result)
            {
                var responseMessage = Request.CreateResponse<string>(HttpStatusCode.Created, result.Data);
                return responseMessage;
            }
            else
            {
                return Request.CreateResponse<string>(HttpStatusCode.BadRequest, "Failed to add the config file for project " + value.JobName);
            }
        }

        // PUT api/jobconfiguration/5
        [Route("Configurations")]
        [HttpPut]
        public HttpResponseMessage Put(JobConfigEntity value)
        {
            //ActionResult<string> result;
            //try
            //{
            //    result = configService.AddorUpdateConfiguration(value.JobName, value.Configuration);
            //}
            //catch (Exception e) { 
            //    return Request.CreateResponse<string>(HttpStatusCode.InternalServerError, "Failed to update the config file for project " + e.StackTrace);
            //}
            var result = configService.AddorUpdateConfiguration(value.JobName, value.Configuration);

            if (result.Result)
            {
                var responseMessage = Request.CreateResponse<string>(HttpStatusCode.OK, result.Data);
                return responseMessage;
            }
            else
            {
                return Request.CreateResponse<string>(HttpStatusCode.BadRequest, "Failed to update the config file for project " + value.JobName);
            }
        }

        [Route("Configurations/{projectName}")]
        [HttpDelete]
        public HttpResponseMessage Delete(string projectName)
        {
            var result = configService.DeleteConfiguration(projectName);
            if(result.Result)
            {
                return Request.CreateResponse(HttpStatusCode.Accepted);
            }

            return Request.CreateResponse(HttpStatusCode.BadRequest);

        }
    }
}
