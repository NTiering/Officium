using Microsoft.AspNetCore.Http;
using Officium.Commands;

namespace Officium.Ext
{
    public static class HttpRequestExt
    {
        public static ICommandContext GetCommandContext(this HttpRequest request)
        {
            var rtn = new CommandContext
            {
                CommandRequestType = GetCommandRequestType(request),
                CommandResponse = new CommandResponse(),
                RequestPath = request.Path.ToString().ToLower()
            };

            return rtn;
        }

        private static CommandRequestType GetCommandRequestType(HttpRequest request)
        {
            if (string.Compare(request.Method, "post", true) == 0) return CommandRequestType.HttpPost;
            if (string.Compare(request.Method, "get", true) == 0) return CommandRequestType.HttpGet;
            if (string.Compare(request.Method, "delete", true) == 0) return CommandRequestType.HttpDelete;
            if (string.Compare(request.Method, "put", true) == 0) return CommandRequestType.HttpPut;
            return CommandRequestType.NoMatch;
        }

        private class CommandContext : ICommandContext
        {
            public string RequestPath { get; set; }
            public CommandRequestType CommandRequestType { get; set; }
            public ICommandResponse CommandResponse { get; set; }
        }
    }
}
