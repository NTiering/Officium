using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Officium.Commands;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Officium.Ext
{
    public static class HttpRequestExt
    {
        public static ICommandContext GetCommandContext(this HttpRequest request, Dictionary<string, string> input = null, Dictionary<string, string> headers = null)
        {

            var rtn = new CommandContext
            {
                CommandRequestType = GetCommandRequestType(request),
                CommandResponse = new CommandResponse(),
                RequestPath = request.Path.ToString().ToLower(),
                Input = input ?? new Dictionary<string, string>(),
                Headers = headers,
                AuthResult = new AuthResult()
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
            public Dictionary<string, string> Input { get; set; }
            public IDictionary<string, string> Headers { get; set; }
            public IAuthResult AuthResult { get; set; }
        }

        private class AuthResult : IAuthResult
        {
            private List<string> _allowedClaims = new List<string>();
            private List<string> _deniedClaims = new List<string>();
            public string BearerId { get; set; }

            public void AddAllowedClaim(string name)
            {
                _allowedClaims.Add(name.ToLower().Trim());
            }

            public void AddDeniedClaim(string name)
            {
                _deniedClaims.Add(name.ToLower().Trim());
            }

            public bool HasAllowedClaim(string name)
            {
                var n = name.ToLower().Trim();
                var rtn = _allowedClaims.Contains(n) && _deniedClaims.Contains(n) == false;
                return rtn;
            }
        }
    }
}
