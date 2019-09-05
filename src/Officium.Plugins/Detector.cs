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
    public class Register
    {
        private readonly IServiceCollection _serviceCollection;

        public Register(IServiceCollection serviceCollection )
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
        private readonly Register register = new Register(null);

        public IActionResult ExecuteRequest(HttpRequest req, ILogger logger)
        {
            return null;
        }
    }
    public class ExecuteCollectionBuilder
    {
        public ICollection<IFunctionPlugin> MakeEceuteCollection(ICollection<IFunctionPlugin> plugins)
        {

            return null;
        }
    }

    public class PluginStepOrderTool
    {
        public IEnumerable<PluginStepOrder> GetSteps { get { return ExtractSteps("Get"); } }
        public IEnumerable<PluginStepOrder> PostSteps { get { return ExtractSteps("Post"); } }
        public IEnumerable<PluginStepOrder> PutSteps { get { return ExtractSteps("Put"); } }
        public IEnumerable<PluginStepOrder> DeleteSteps { get { return ExtractSteps("Delete"); } }

        private IEnumerable<PluginStepOrder> ExtractSteps(string prefix)
        {
            var values = Enum.GetValues(typeof(PluginStepOrder))
               .Cast<PluginStepOrder>()
               .Where(x =>
               {
                   var s = x.ToString();
                   var rtn = s.Contains(prefix) || s.Contains("Always") || s.Contains("All");
                   return rtn;
               })
               .OrderBy(x => (int) x);            


            return values;
        }
    }

        /// <summary>
        /// Represents a 
        /// </summary>
        public interface IFunctionPlugin
    {
        PluginStepOrder StepOrder { get; }
    }
}
