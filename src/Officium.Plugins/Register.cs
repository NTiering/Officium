using Microsoft.Extensions.DependencyInjection;
using System;

namespace Officium.Plugins
{
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
}