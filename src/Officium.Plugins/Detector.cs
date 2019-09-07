namespace Officium.Plugins
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    /// Detects and registers plugins 
    /// </summary>
    public class Detector
    {       
        public void Detect(Action<Type> onFound)
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies().ToList();
            DetectFromAssemblies(assemblies, onFound);
        }

        public void Detect(IRegister register)
        {
            Detect((x)=> { register.RegisterType(typeof(IFunctionPlugin), x); });
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
}

