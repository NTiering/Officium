using System;
using System.Text.RegularExpressions;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Officium.Commands;
using Officium.Example.Commands;
using Officium.Startup;

[assembly: FunctionsStartup(typeof(Officium.Example.Startup))]
namespace Officium.Example
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddHttpClient();
            void register(Type tInterface, Type tType)
            {
                builder.Services.AddSingleton(tInterface, tType);
            }
            Officium.ServiceRegister.Register(register);
            ServiceRegisterTools.RegisterAllCommandHandlers(typeof(Startup).Assembly, register);
            ServiceRegisterTools.RegisterAllCommandValidators(typeof(Startup).Assembly, register);
            RegisterCommands(builder);
        }

        private static void RegisterCommands(IFunctionsHostBuilder builder)
        {
            void register(Type tInterface, Type tType)
            {
                builder.Services.AddSingleton(tInterface, tType);
            }

            var sp = builder.Services.BuildServiceProvider();
            var commandFactory = sp.GetService<ICommandFactory>();
            ServiceRegisterTools.RegisterAllCommands(typeof(Startup).Assembly, commandFactory,register);
        }
    }
}
