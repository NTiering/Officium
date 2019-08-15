﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Officium.Tools.Handlers;
using Officium.Tools.Helpers;

namespace Officium.Tools.Request
{
    public static class HttpRequestExtMethods
    {
        public static RequestContext MakeRequestContext(this HttpRequest httpRequest)
        {
            return new RequestContext
            {
                RequestMethod = ToRequestMethod(httpRequest.Method),
                Path = httpRequest.Path.ToString(),
                QueryParams = GetQueryParams(httpRequest),
                BodyParams = GetBodyParams(httpRequest)
            };
        }

        private static Dictionary<string, string> GetQueryParams(HttpRequest httpRequest)
        {

            return httpRequest.Query == null ?
                new Dictionary<string,string>():
                httpRequest.Query.ToDictionary(x => x.Key.ToLower(), x => x.Value.FirstOrDefault()?? string.Empty);
        }

        private static Dictionary<string,string> GetBodyParams(HttpRequest httpRequest)
        {
            if (httpRequest.Body == null) return new Dictionary<string, string>();
            string requestBody = new StreamReader(httpRequest.Body).ReadToEnd();
            var data = JsonConvert.DeserializeObject<Dictionary<string, string>>(requestBody);
            return data;
        }

        private static RequestMethod ToRequestMethod(string method)
        {
            if(method.IsNullOrWhitespace()) return RequestMethod.NOTMAPPED;
            var m = method.ToUpper().Trim();
            if (m == "POST") return RequestMethod.POST;
            if (m == "DELETE") return RequestMethod.DELETE;
            if (m == "GET") return RequestMethod.GET;
            if (m == "PUT") return RequestMethod.PUT;
            return RequestMethod.NOTMAPPED;
        }
    }
}
