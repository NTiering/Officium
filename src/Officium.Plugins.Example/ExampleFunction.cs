using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Officium.Plugins.Example
{
    public class Example
    {
        private readonly IExecutor executor;

        public Example(IExecutor executor)
        {
            this.executor = executor;
        }

        [FunctionName("ExampleFunction")]
        public IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = "v1/{n1?}/{n2?}/{n3?}/{n4?}/{n5?}/{n6?}/{n7?}/{n8?}/{n9?}/{n10?}/{n11?}/{n12?}/{n13?}/{n14?}/")] HttpRequest req,
            ILogger log)
        {           
            return executor.ExecuteRequest(req, log);
        }
    }

    public class LoggerPlugin : IFunctionPlugin
    {
        public PluginStepOrder StepOrder => PluginStepOrder.BeforeGet; // run this before every get request

        public IActionResult ExecuteRequest(HttpRequest req, ILogger logger, IPluginContext context)
        {
            logger.LogInformation($"Request from {req.Path} processed");
            return null; 
        }
    }

    public class HelloWorldPlugin : IFunctionPlugin
    {
        public PluginStepOrder StepOrder => PluginStepOrder.OnGet; // run this on every get request

        public IActionResult ExecuteRequest(HttpRequest req, ILogger logger, IPluginContext context)
        {
            string name = req.Query["name"];
            return name != null
                ? (ActionResult)new OkObjectResult($"Hello, {name}")
                : new BadRequestObjectResult("Please pass a name on the query string or in the request body");
        }
    }
}
