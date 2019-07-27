namespace Officium
{
    using Officium.CommandHandlers;
    using Officium.Commands;
    using System;
    public static class ServiceRegister
    {
        public static void Register(Action<Type, Type> register)
        {
            register(typeof(ICommandHandlerFactory), typeof(CommandHandlerFactory));
            register(typeof(ICommandFactory), typeof(CommandFactory));
        }
    }
}
