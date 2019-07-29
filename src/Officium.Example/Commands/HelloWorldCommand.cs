using Officium.Attributes;
using Officium.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Officium.Example.Commands
{
    [CommandHandlerRouting(RequestType = CommandRequestType.HttpGet, Path = ".")]
    public class HelloWorldCommand : ICommand
    {
        public string Name { get; set; }
        public CommandRequestType CommandRequestType { get; set; }
        public ICommandResponse CommandResponse { get; set; }
    }
}
