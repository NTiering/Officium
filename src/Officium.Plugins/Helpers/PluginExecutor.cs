namespace Officium.Plugins.Helpers
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using System.Collections.Generic;
    using System.Linq;
    internal class PluginExecutor
    {
        public static PluginExecutor Instance = new PluginExecutor();
        private PluginExecutor()
        {
        }

        public IActionResult Execute(ICollection<IFunctionPlugin> executeCollection, HttpRequest req, ILogger logger, IPluginContext context, HandlerExecutedAction handlerExecutedAction)
        {
            IActionResult result = executeCollection
                .OrderBy(x => x.StepOrder)
                .Select(plugin =>
                {                    
                    IActionResult rtn = context.HaltExecution ? null : plugin.ExecuteRequest(req, logger, context);
                    handlerExecutedAction.Action(plugin, req, logger, context); 
                    return rtn;
                })
                .LastOrDefault(x => x != null);

            return result;
        }
    }



}