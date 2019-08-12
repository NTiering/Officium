using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.Http;
using Officium.Tools.ReqRes;
using Officium.Tools.Handlers;

namespace Officium.Examples
{
    public class Function1
    {
        private readonly IRequestResolver requestResolver;

        public Function1(IRequestResolver requestResolver)
        {
            this.requestResolver = requestResolver;
        }

        [FunctionName("Function1")]
        public async Task<IActionResult> Run(
            [HttpTrigger(
            AuthorizationLevel.Function, "get", "post", 
            Route = null)]
            HttpRequest req,
            ILogger log)
        {
            var reqContext = req.MakeRequestContext();
            var resContext = requestResolver.Execute(reqContext);

            // log.LogInformation("C# HTTP trigger function processed a request.");

            string name = req.Query["name"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            name = name ?? data?.name;

            return name != null
                ? (ActionResult)new OkObjectResult($"Hello, {name}")
                : new BadRequestObjectResult("Please pass a name on the query string or in the request body");
        }
    }

    public class HelloWorldHandler : Officium.Tools.Handlers.IHandler
    {
        public void HandleRequest(RequestContext request, ResponseContent response)
        {
            response.Result = "Hello world";
            response.StatusCode = 200;
        }
    }
}
