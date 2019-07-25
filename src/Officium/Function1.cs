using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Officium.Commands;
using System.Collections.Generic;
using System.Linq;
using Officium.Ext;

namespace FunctionApp2
{
    public static class Function1
    {
        [FunctionName("Function1")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {


            log.LogInformation("C# HTTP trigger function processed a request.");

            string name = req.Query["name"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
           // dynamic data = JsonConvert.DeserializeObject(requestBody);
            var dict = JsonConvert.DeserializeObject<Dictionary<string, string>>(requestBody);
            var inputValues =  new Dictionary<string, string>();
            inputValues.AddRange(dict);
            inputValues.AddRange(req.Query);
           
            //data


            //new OfficiumCommandFactory()

            //name = name ?? data?.name;

            return name != null
                ? (ActionResult)new OkObjectResult($"Hello, {name}")
                : new BadRequestObjectResult("Please pass a name on the query string or in the request body");
        }

       

    }
}
