using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;
using Officium.Tools.Handlers;

namespace Officium.Tools.ReqRes
{
    public static class HttpRequestExtMethods
    {
        public static RequestContext MakeRequestContext(this HttpRequest httpRequest)
        {
            return new RequestContext
            {
                RequestMethod = ToRequestMethod(httpRequest.Method),
                Path = httpRequest.Path.ToString()
            };
        }
      
        private static RequestMethod ToRequestMethod(string method)
        {
            var m = method.ToUpper().Trim();
            if (m == "POST") return RequestMethod.POST;
            if (m == "DELETE") return RequestMethod.DELETE;
            if (m == "GET") return RequestMethod.GET;
            if (m == "PUT") return RequestMethod.PUT;
            return RequestMethod.NOTMAPPED;
        }
    }
}
