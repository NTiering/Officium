using Officium.Attributes;
using Officium.CommandHandlers;
using Officium.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Officium.Example.Commands
{
    public class HelloWorldCommandHandler : BaseCommandHandler<HelloWorldCommand>
    {
        protected override void HandleCommand(HelloWorldCommand command)
        {
            command.CommandResponse.AddValue("greeting", $"Hello {command.Name}");
        }
    }
}
