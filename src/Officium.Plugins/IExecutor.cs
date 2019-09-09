using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Officium.Plugins
{
    public interface IExecutor
    {
        IActionResult ExecuteRequest(HttpRequest req, ILogger logger, IPluginContext context = null);
    }
}