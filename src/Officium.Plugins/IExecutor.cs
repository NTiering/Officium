using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace Officium.Plugins
{
    public interface IExecutor
    {
        IActionResult ExecuteRequest(HttpRequest req, ILogger logger, IPluginContext context = null);

        Action<IFunctionPlugin, HttpRequest, ILogger, IPluginContext> OnHanderExecuted { get; set; }
    }
}