namespace Officium.Tools.Request
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Microsoft.AspNetCore.Http;
    using Newtonsoft.Json;
    using Officium.Tools.Helpers;

    public static class HttpRequestExtMethods
    {
        public static RequestContext MakeRequestContext(this HttpRequest httpRequest)
        {
            return new RequestContext(new ValueExtractor())
            {
                HeadersParams = GetHeaderDictionary(httpRequest),
                RequestMethod = ToRequestMethod(httpRequest.Method),
                Path = httpRequest.Path.ToString(),
                QueryParams = GetQueryParams(httpRequest),
                BodyParams = GetBodyParams(httpRequest)
            };
        }

        private static Dictionary<string, string> GetHeaderDictionary(HttpRequest httpRequest)
        {
            var rtn = httpRequest.Headers.ToDictionary(x => x.Key, x => x.Value.FirstOrDefault() ?? string.Empty);
            return rtn;
        }

        private static Dictionary<string, string> GetQueryParams(HttpRequest httpRequest)
        {
            return httpRequest.Query == null ?
                new Dictionary<string, string>() :
                httpRequest.Query.ToDictionary(x => x.Key.ToLower(), x => x.Value.FirstOrDefault() ?? string.Empty);
        }

        private static Dictionary<string, string> GetBodyParams(HttpRequest httpRequest)
        {
            if (httpRequest.Body == null) return new Dictionary<string, string>();
            var requestBody = new StreamReader(httpRequest.Body).ReadToEnd();
            var data = JsonConvert.DeserializeObject<Dictionary<string, string>>(requestBody);
            return data;
        }

        private static RequestMethod ToRequestMethod(string method)
        {
            if (method.IsNullOrWhitespace()) return RequestMethod.NOTMAPPED;
            var m = method.ToUpper().Trim();
            if (m == "POST") return RequestMethod.POST;
            if (m == "DELETE") return RequestMethod.DELETE;
            if (m == "GET") return RequestMethod.GET;
            if (m == "PUT") return RequestMethod.PUT;
            if (m == "CONNECT") return RequestMethod.CONNECT;
            if (m == "OPTIONS") return RequestMethod.OPTIONS;
            if (m == "TRACE") return RequestMethod.TRACE;
            if (m == "PATCH") return RequestMethod.PATCH;
            return RequestMethod.NOTMAPPED;
        }
    }
}
