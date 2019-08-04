using System;
using System.Text.RegularExpressions;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Officium.Commands;
using Officium.Startup;
using Officium.Widget.Data;

[assembly: FunctionsStartup(typeof(Officium.Widget.Startup))]
namespace Officium.Widget
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {           
            builder.Services.AddSingleton(typeof(IWidgetDataContext), typeof(WidgetDataContext));

            void register(Type tInterface, Type tType)
            {
                builder.Services.AddSingleton(tInterface, tType);
            }
            Officium.ServiceRegister.Register(register);
            ServiceRegisterTools.RegisterAllCommandHandlers(typeof(Startup).Assembly, register);
            ServiceRegisterTools.RegisterAllCommandValidators(typeof(Startup).Assembly, register);
            ServiceRegisterTools.RegisterAllCommandFilters(typeof(Startup).Assembly, register);

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
            ServiceRegisterTools.RegisterAllCommands(typeof(Startup).Assembly, commandFactory, register);
        }
    }
}
