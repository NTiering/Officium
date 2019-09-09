using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Officium.Plugins.Helpers
{
    public static class IServiceCollectionExtentions
    {
        public static void AddOficuimServices(this IServiceCollection collection)
        {
            collection.AddSingleton<IExecutor, Executor>();
        }

        public static void AddPlugins(this IServiceCollection collection)
        {
            new Detector().Detect((x) => collection.AddSingleton(typeof(IFunctionPlugin), x));
        }
    }
}
