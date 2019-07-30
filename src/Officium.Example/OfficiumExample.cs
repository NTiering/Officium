namespace Officium.Example
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Azure.WebJobs;
    using Microsoft.Azure.WebJobs.Extensions.Http;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Logging;
    using System.Net.Http;
    using Officium.CommandHandlers;
    using Officium.Commands;
    using System.Linq;
    using Officium.Startup;

    public class OfficiumExample
    {
        private readonly HttpClient _client;
        private readonly ICommandHandlerFactory _commandHandlerFactory;
        private readonly ICommandFactory _commandFactory;

        public OfficiumExample(IHttpClientFactory httpClientFactory, ICommandHandlerFactory commandHandlerFactory, ICommandFactory commandFactory)
        {
            _client = httpClientFactory.CreateClient();
            _commandHandlerFactory = commandHandlerFactory;
            _commandFactory = commandFactory;
        }

        [FunctionName("OfficiumExample")]
        public async Task<IActionResult> Run(
            [HttpTrigger(
                AuthorizationLevel.Function, 
                "get", "post","put","delete", 
                Route = "v1/OfficiumExample/{n1?}/{n2?}/{n3?}/{n4?}/{n5?}/{n6?}/{n7?}/{n8?}")]
        HttpRequest req,
            ILogger log)
        {
            var input = await AzureTools.GetDataInput(req);
            var command = AzureTools.GetCommand(_commandFactory, req, input);
            AzureTools.ExecuteCommandHandler(_commandHandlerFactory, command);

            log.LogInformation($"Processed {command.CommandRequestType.ToString()} for '{req.Path}' with {command.GetType()}");

            return command.CommandResponse.ValidationResults.Any() ? (ObjectResult)
                new BadRequestObjectResult(command.CommandResponse.ValidationResults) :
                new OkObjectResult(command.CommandResponse.Values);
        }

        

       
    }
}
