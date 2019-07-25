using FunctionApp1.Commands;
using FunctionMonkey.Abstractions;
using FunctionMonkey.Abstractions.Builders;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace FunctionApp1
{
    public class FunctionAppConfiguration : IFunctionAppConfiguration
    {
        public void Build(IFunctionHostBuilder builder)
        {
            var t = new ClaimsPrincipal();
            t.AddIdentity(new ClaimsIdentity(new Claim[] { new Claim("Admin", "true") }));

            var xtt = t.HasClaim((x) => { return x.Type == "Admin"; });

            builder
                 .Setup((serviceCollection, commandRegistry) =>
                 {
                     commandRegistry.Register<HelloWorldCommandHandler>();
                     //           commandRegistry.Register<HelloWorldCommandHandler>();
                 })
                 .OpenApiEndpoint(openApi => openApi
                 .Title("My API Title")
                 .Version("0.0.0")
                 .UserInterface())
                 .Functions(functions => functions
                     .HttpRoute("v1/HelloWorld", route => route
                         .HttpFunction<HelloWorldCommand>()
                     )
                 );
        }
    }
}
