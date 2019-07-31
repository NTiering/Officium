namespace Officium.Startup
{
    using Microsoft.AspNetCore.Http;
    using Newtonsoft.Json;
    using Officium.CommandHandlers;
    using Officium.Commands;
    using Officium.Ext;
    using System.Collections.Generic;
    using System.IO;
    using System.Threading.Tasks;
    public static class AzureTools
    {
        public static async Task<Dictionary<string, string>> GetDataInput(HttpRequest req)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var data = JsonConvert.DeserializeObject<Dictionary<string, string>>(requestBody);

            var input = new Dictionary<string, string>()
                .AddRange(req.Query)
                .AddRange(data);
            return input;
        }

        public static void ExecuteCommandHandler(ICommandHandlerFactory commandHandlerFactory, ICommand command, ICommandContext context)
        {
            commandHandlerFactory.GetCommandHandler(command,context).Handle(command, context);
        }

        public static ICommand GetCommand(ICommandFactory commandFactory, ICommandContext context, Dictionary<string, string> input)
        {
            return commandFactory.BuildCommand(context, input);
        }
    }
}
