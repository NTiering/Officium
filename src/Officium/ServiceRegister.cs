using Officium.CommandHandlers;
using Officium.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Officium
{
    public static class ServiceRegister
    {
        public static void Register(Action<Type, Type> register)
        {
            register(typeof(ICommandHandlerFactory), typeof(CommandHandlerFactory));
            register(typeof(ICommandFactory), typeof(CommandFactory));
        }
    }
}
