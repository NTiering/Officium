namespace Officium.Plugins
{
    using System;
    public interface IRegister
    {
        void RegisterType(Type interfaceType, Type serviceType);
    }
}