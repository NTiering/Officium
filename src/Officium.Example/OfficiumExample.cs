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
using Officium.CommandHandlers;
using Officium.Commands;
using Officium.Example.Commands;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using Officium.Ext;

namespace Officium.Example
{
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
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            var input = await GetDataInput(req);
            var command = GetCommand(req, input);
            ExecuteCommandHandler(command);

            log.LogInformation($"Processed {command.CommandRequestType.ToString()} for '{req.Path}' with ");
           
            return new OkObjectResult(command.CommandResponse.Values);
        }

        private void ExecuteCommandHandler(ICommand command)
        {
            _commandHandlerFactory.GetCommandHandler(command).Handle(command);
        }

        private ICommand GetCommand(HttpRequest req, Dictionary<string, string> input)
        {
            return _commandFactory.BuildCommand(req.GetCommandRequestType(), req.Path, input);
        }

        private static async Task<Dictionary<string, string>> GetDataInput(HttpRequest req)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var data = JsonConvert.DeserializeObject<Dictionary<string, string>>(requestBody);

            var input = new Dictionary<string, string>()
                .AddRange(req.Query)
                .AddRange(data);
            return input;
        }
    }
}
