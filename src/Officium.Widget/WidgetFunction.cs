using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Officium.CommandHandlers;
using Officium.Commands;
using Officium.Startup;
using Officium.Ext;
using System.Linq;
using System.Collections.Generic;

namespace Officium.Widget
{
    public class WidgetFunction
    {
        private readonly ICommandHandlerFactory _commandHandlerFactory;
        private readonly ICommandFactory _commandFactory;

        public WidgetFunction(ICommandHandlerFactory commandHandlerFactory, ICommandFactory commandFactory)
        {
            _commandHandlerFactory = commandHandlerFactory;
            _commandFactory = commandFactory;
        }

        [FunctionName("Widget")]
        public async Task<IActionResult> Run(
            [HttpTrigger(
                AuthorizationLevel.Function,
                "get","post","put","delete",
                Route = "v1/Widget/{n1?}/{n2?}/{n3?}/{n4?}/{n5?}/{n6?}/{n7?}/{n8?}")]
        HttpRequest req,
            ILogger log)
        {
            var input = await AzureTools.GetDataInput(req);
            var context = req.GetCommandContext(input, req.Headers.ToDictionary(x=>x.Key, x=>x.Value.FirstOrDefault()));
            var command = AzureTools.GetCommand(_commandFactory, context, input);
            AzureTools.ExecuteCommandHandler(_commandHandlerFactory, command, context);

            log.LogInformation($"Processed {context.CommandRequestType.ToString()} for '{req.Path}' with {command.GetType()}");

            return context.CommandResponse.ValidationResults.Any() ? (ObjectResult)
                new BadRequestObjectResult(context.CommandResponse.ValidationResults) :
                new OkObjectResult(context.CommandResponse.Values);
        }
    }
}
