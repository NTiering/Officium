namespace Officium.Plugins.Helpers
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using System.Collections.Generic;
    using System.Linq;
    public class PluginExecutor
    {
        public static PluginExecutor Instance = new PluginExecutor();
        private PluginExecutor()
        {
        }

        public IActionResult Execute(ICollection<IFunctionPlugin> executeCollection, HttpRequest req, ILogger logger, IPluginContext context)
        {
            IActionResult result = null;
            executeCollection.OrderBy(x => x.StepOrder).ToList().ForEach(plugin =>
              {
                  if (result == null)
                  {
                      result = plugin.ExecuteRequest(req, logger, context);
                  }
              });

            return result;
        }
    }
}