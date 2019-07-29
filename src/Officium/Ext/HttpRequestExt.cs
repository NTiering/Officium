using Microsoft.AspNetCore.Http;
using Officium.Commands;

namespace Officium.Ext
{
    public static class HttpRequestExt
    {
        public static CommandRequestType GetCommandRequestType(this HttpRequest request)
        {
            if (string.Compare(request.Method, "post", true) == 0) return CommandRequestType.HttpPost;
            if (string.Compare(request.Method, "get", true) == 0) return CommandRequestType.HttpGet;
            if (string.Compare(request.Method, "delete", true) == 0) return CommandRequestType.HttpDelete;
            if (string.Compare(request.Method, "put", true) == 0) return CommandRequestType.HttpPut;
            return CommandRequestType.NoMatch;
        }
    }
}
