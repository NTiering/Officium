namespace Officium.Plugins
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Represents a single plugin
    /// </summary>
    public interface IFunctionPlugin
    {
        PluginStepOrder StepOrder { get; }

        IActionResult ExecuteRequest(HttpRequest req, ILogger logger, IPluginContext context);
    }
}