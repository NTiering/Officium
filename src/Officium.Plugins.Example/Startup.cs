using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Officium.Plugins.Helpers;

[assembly: FunctionsStartup(typeof(Officium.Plugins.Example.Startup))]
namespace Officium.Plugins.Example
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddPlugins();
            builder.Services.AddOficuimServices();
        }
    }
}
