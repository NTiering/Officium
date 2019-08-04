using Officium.CommandFilters;
using Officium.Commands;
using Officium.Widget.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Text;

namespace Officium.Widget.Filters
{
    class AuthFilter : ICommandFilter
    {
        public void AfterHandleEvent(ICommand command, ICommandContext context)
        {
        }

        public void BeforeHandleEvent(ICommand command, ICommandContext context)
        {
            // Authorization: Bearer <token>
            var token = GetToken(context.Headers);
            ResolveToken(context,token);
        }

        private string GetToken(IDictionary<string, string> headers)
        {
             var authKey = "Authorization";
            if (headers.ContainsKey(authKey) == false) return Guid.Empty.ToString();
            var authHeaderEntry = headers[authKey];
            if (string.IsNullOrWhiteSpace(authHeaderEntry)) return Guid.Empty.ToString();
            if (authHeaderEntry.ToLower().StartsWith("bearer ") == false) return Guid.Empty.ToString();
            var rtn = authHeaderEntry.Split(' ').Last().Trim();
            return rtn;
        }

        private void ResolveToken(ICommandContext context, string token)
        {
           // var url = "https://officiumauthproxy.blob.core.windows.net/authproxy/JosonAdmin.txt?token=" + token;
            var url = "https://officiumauthproxy.blob.core.windows.net/authproxy/JosonNonAdmin.txt?token=" + token;

            using (var client = new WebClient())
            {
                try
                {
                    var content = client.DownloadString(url);
                    var claims = Newtonsoft.Json.JsonConvert.DeserializeObject<string[]>(content);
                    foreach (var t in claims.Where(x => x.StartsWith("_id:") == false))
                    {
                        context.AuthResult.AddAllowedClaim(t);
                    }
                    foreach (var t in claims.Where(x => x.StartsWith("_id:")))
                    {
                        context.AuthResult.BearerId = t.Split(":").Last();
                    }

                }
                catch (Exception ex)
                {
                    // todo Log this 
                }              
            }



            //var t = new Dictionary<string,string>();
            //t["_id"] = "67200cb7-77bd-4a2f-b091-695be8f9c997";
            //t["WidgetUser"] = "Allow";
            //t["widgetAdminUser"] = "Deny";
            //var x = Newtonsoft.Json.JsonConvert.SerializeObject(t);
        }

        public bool CanFilter(ICommand command, ICommandContext context)
        {
            return true;
        }
    }
}
