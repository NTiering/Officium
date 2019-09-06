using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Linq;

namespace Officium.Plugins
{
    /// <summary>
    /// Detects and registers plugins 
    /// </summary>
    public class Detector
    {
        private Register register = new Register(null);

        public void Detect(Action<Type> onFound)
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies().ToList();
            DetectFromAssemblies(assemblies, onFound);
        }

        private static void DetectFromAssemblies(List<Assembly> assemblies, Action<Type> onFound)
        {
            assemblies.ForEach(assembly =>
                            {
                                assembly.GetTypes()
                                    .Where(x => x.IsClass)
                                    .Where(x => x.IsAbstract == false)
                                    .Where(x => typeof(IFunctionPlugin).IsAssignableFrom(x))
                                    .ToList()
                                    .ForEach(onFound);
                            });
        }
    }

    /// <summary>
    /// Holds a reference of all plugins 
    /// </summary>
    public class Register : IRegister
    {
        private readonly IServiceCollection _serviceCollection;

        public Register(IServiceCollection serviceCollection)
        {
            _serviceCollection = serviceCollection;
        }

        public void RegisterType(Type interfaceType, Type serviceType)
        {
            _serviceCollection.AddSingleton(interfaceType, serviceType);
        }
    }

    /// <summary>
    /// Executes a request and routes response
    /// </summary>
    public class Executor
    {
        private readonly ICollection<IFunctionPlugin> allPlugins;

        public Executor(ICollection<IFunctionPlugin> allPlugins)
        {
            this.allPlugins = allPlugins;
        }

        public IActionResult ExecuteRequest(HttpRequest req, ILogger logger, IPluginContext context = null)
        {
            var executeCollection = ExecuteCollectionBuilder.Instance.MakeExecuteCollection(req, allPlugins);
            var rtn = ExecuteCollectionExecutor.Instance.ExecuteCollection(executeCollection, req, logger, context);
            return rtn;
        }

    }

    public static class StringExt
    {
        public static bool Is(this string s, string compare)
        {
            if (string.IsNullOrWhiteSpace(s)) return false;
            return string.Compare(s, compare, true) == 0;
        }
    }

    public class ExecuteCollectionExecutor
    {
        public static ExecuteCollectionExecutor Instance = new ExecuteCollectionExecutor();
        private ExecuteCollectionExecutor()
        {

        }

        public IActionResult ExecuteCollection(ICollection<IFunctionPlugin> executeCollection, HttpRequest req, ILogger logger, IPluginContext context)
        {
            IActionResult result = null;
            executeCollection.OrderBy(x=>x.StepOrder).ToList().ForEach(plugin =>
            {
                if (result == null)
                {
                    result = plugin.ExecuteRequest(req, logger, context);
                }
            });

            return result;
        }
    }

    public class ExecuteCollectionBuilder
    {
        public static ExecuteCollectionBuilder Instance = new ExecuteCollectionBuilder();

        private ExecuteCollectionBuilder()
        {

        }
        public ICollection<IFunctionPlugin> MakeExecuteCollection(HttpRequest req, ICollection<IFunctionPlugin> plugins)
        {
            var steps = new List<PluginStepOrder>();
            steps.AddRange(PluginStepOrderTool.Instance.BeforeSteps);
            steps.AddRange(PluginStepOrderTool.Instance.AfterSteps);

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

    public class PluginStepOrderTool
    {
        public static PluginStepOrderTool Instance = new PluginStepOrderTool();
        public IEnumerable<PluginStepOrder> GetSteps { get; private set; }
        public IEnumerable<PluginStepOrder> PostSteps { get; private set; }
        public IEnumerable<PluginStepOrder> PutSteps { get; private set; }
        public IEnumerable<PluginStepOrder> DeleteSteps { get; private set; }
        public IEnumerable<PluginStepOrder> AfterSteps { get; private set; }
        public IEnumerable<PluginStepOrder> BeforeSteps { get; private set; }

        private PluginStepOrderTool()
        {
            GetSteps = ExtractSteps("Get");
            PostSteps = ExtractSteps("Post");
            PutSteps = ExtractSteps("Put");
            DeleteSteps = ExtractSteps("Delete");
            BeforeSteps = new[] { PluginStepOrder.AlwaysFirst, PluginStepOrder.BeforeAll }.OrderBy(x => x);
            AfterSteps = new[] { PluginStepOrder.AlwaysLast, PluginStepOrder.AfterAll }.OrderBy(x=> x);
        }

        private static IEnumerable<PluginStepOrder> ExtractSteps(string prefix)
        {
            var values = Enum.GetValues(typeof(PluginStepOrder))
               .Cast<PluginStepOrder>()
               .Where(x =>
               {
                   var s = x.ToString();
                   var rtn = s.Contains(prefix);
                   return rtn;
               })
               .OrderBy(x => (int)x);


            return values;
        }
    }

    /// <summary>
    /// Represents a single plugin
    /// </summary>
    public interface IFunctionPlugin
    {
        PluginStepOrder StepOrder { get; }

        IActionResult ExecuteRequest(HttpRequest req, ILogger logger, IPluginContext context);
    }

    public interface IPluginContext
    {

    }
}
