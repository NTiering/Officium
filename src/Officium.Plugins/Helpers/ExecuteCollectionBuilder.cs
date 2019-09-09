using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Officium.Plugins.Helpers
{
    public class ExecuteCollectionBuilder
    {
        public static ExecuteCollectionBuilder Instance = new ExecuteCollectionBuilder();

        private ExecuteCollectionBuilder()
        {
        }

        public ICollection<IFunctionPlugin> MakeExecuteCollection(HttpRequest req, ICollection<IFunctionPlugin> plugins)
        {
            var steps = new List<PluginStepOrder>();
            steps.AddRange(PluginStepOrderTool.Instance.BeforeAllSteps);
            steps.AddRange(PluginStepOrderTool.Instance.AfterAllSteps);

            if (req.Method.Is("Post"))
            {
                steps.AddRange(PluginStepOrderTool.Instance.PostSteps);
            }
            else if (req.Method.Is("Get"))
            {
                steps.AddRange(PluginStepOrderTool.Instance.GetSteps);
            }
            else if (req.Method.Is("Put"))
            {
                steps.AddRange(PluginStepOrderTool.Instance.PutSteps);
            }
            else if (req.Method.Is("Delete"))
            {
                steps.AddRange(PluginStepOrderTool.Instance.DeleteSteps);
            }

            var result = plugins.Where(x => steps.Contains(x.StepOrder)).OrderBy(x => x.StepOrder).ToArray();
            return result;
        }
    }
}