﻿namespace Officium.Example
{
    using Officium.Attributes;
    using Officium.CommandHandlers;
    using Officium.Commands;
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Text.RegularExpressions;

    public static class ServiceRegisterTools
    {
        public static void RegisterAllCommandHandlers(Assembly assembly, Action<Type, Type> register)
        {
            var handlers = assembly.GetTypes()
                .Where(x => x.IsAbstract == false)
                .Where(x => x.IsClass)
                .Where(x => typeof(ICommandHandler).IsAssignableFrom(x))
                .ToList();

            handlers.ForEach(x => register(typeof(ICommandHandler), x));
        }

        public static void RegisterAllCommands(Assembly assembly, ICommandFactory commandFactory, Action<Type, Type> register)
        {
            var commandTypes = assembly.GetTypes()
                .Where(x => x.IsAbstract == false)
                .Where(x => x.IsClass)
                .Where(x => typeof(ICommand).IsAssignableFrom(x))
                .Where(x => x.GetCustomAttributes<CommandHandlerRoutingAttribute>().Any())

                .ToList();

            commandTypes.ForEach(x => 
            {
                var attrib = x.GetCustomAttributes<CommandHandlerRoutingAttribute>().First();
                commandFactory.RegisterCommandType(attrib.RequestType, new Regex(attrib.Path), x);
            });

       //     
        }
    }  
}
