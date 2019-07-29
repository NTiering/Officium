namespace Officium.CommandHandlers
{
    using Officium.Commands;
    using Officium.CommandValidators;
    using Officium.Ext;
    using System.Collections.Generic;
    using System.Linq;
    public class CommandHandlerFactory : ICommandHandlerFactory
    {
        private readonly ICommandHandler[] commandHandlers;
        private readonly ICommandValidator[] commandValidators;

        public CommandHandlerFactory(ICommandHandler[] commandHandlers, ICommandValidator[] commandValidators)
        {
            this.commandHandlers = commandHandlers.WithDefault(new ICommandHandler[0]);
            this.commandValidators = commandValidators.WithDefault(new ICommandValidator[0]);
        }

        public ICommandHandler GetCommandHandler(ICommand command)
        {
            var validators = commandValidators
                .Where(x => command != null && x.CanValidate(command))
                .ToList();
            validators.Add(new NoMatchCommandValidator());

            var handler = commandHandlers
                .FirstOrDefault(x => command != null && x.CanHandle(command))
                .WithDefault(new NoMatchCommandHandler());

            var rtn = new ValidatingCommandHandler(validators, handler);
            return rtn;
        }
    }    
}
