using FunctionApp2.CommandHandlers;
using Officium.Commands;
using Officium.Ext;
using System.Linq;

namespace Officium.CommandHandlers
{
    public class CommandHandlerFactory
    {
        private readonly ICommandHandler[] commandHandlers;
        public CommandHandlerFactory(ICommandHandler[] commandHandlers)
        {
            this.commandHandlers = commandHandlers ?? Enumerable.Empty<ICommandHandler>().ToArray();
        }

        public ICommandHandler GetCommandHandler(ICommand command)
        {
            var rtn = commandHandlers
                .FirstOrDefault(x => command != null && x.CanHandle(command))
                .WithDefault(new NoMatchCommandHandler());
            return rtn;
        }        
    }
}
