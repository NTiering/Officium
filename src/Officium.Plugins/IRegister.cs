using System;

namespace Officium.Plugins
{
    public interface IRegister
    {
        void RegisterType(Type interfaceType, Type serviceType);
    }
}