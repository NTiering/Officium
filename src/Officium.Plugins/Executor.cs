namespace Officium.Plugins
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Officium.Plugins.Helpers;
    using System.Collections.Generic;

    /// <summary>
    /// Executes a request and routes response
    /// </summary>
    public class Executor : IExecutor
    {
        private readonly ICollection<IFunctionPlugin> allPlugins;

        public Executor(ICollection<IFunctionPlugin> allPlugins)
        {
            this.allPlugins = allPlugins;
        }

        public IActionResult ExecuteRequest(HttpRequest req, ILogger logger, IPluginContext context = null)
        {
            var executeCollection = ExecuteCollectionBuilder.Instance.MakeExecuteCollection(req, allPlugins);
            var rtn = PluginExecutor.Instance.Execute(executeCollection, req, logger, context ?? new DefaultPluginContext());
            return rtn;
        }

    }
}