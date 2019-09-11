namespace Officium.Plugins
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Logging;
    using System;

    internal class HandlerExecutedAction 
    {
        private readonly Action<IFunctionPlugin, HttpRequest, ILogger, IPluginContext> action;

        public HandlerExecutedAction(Action<IFunctionPlugin, HttpRequest, ILogger, IPluginContext> action)
        {
            this.action = action;
        }

        public void Action(IFunctionPlugin plugin, HttpRequest req, ILogger logger, IPluginContext ctx)
        {
            action?.Invoke(plugin, req, logger, ctx);
        }

    }
}