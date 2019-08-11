using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Officium.Tools.Handlers;
using System;
using System.Collections.Generic;
using System.Text;

[assembly: FunctionsStartup(typeof(Officium.Examples.Startup))]
namespace Officium.Examples
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {  
            using (var b = new Builder(builder.Services))
            {
                b.OnRequest<HelloWorldHandler>(
                    RequestMethod.GET,
                    "/api/Function1");
            }

            // builder.Services.AddHttpClient();
            //builder.Services.AddSingleton((s) => {
            //    return new CosmosClient(Environment.GetEnvironmentVariable("COSMOSDB_CONNECTIONSTRING"));
            //});
            //builder.Services.AddSingleton<ILoggerProvider, MyLoggerProvider>();
        }
    }
}
