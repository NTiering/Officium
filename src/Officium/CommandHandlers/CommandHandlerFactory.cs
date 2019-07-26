using FunctionApp2.CommandHandlers;
using Officium.Commands;
using Officium.CommandValidators;
using Officium.Ext;
using System.Collections.Generic;
using System.Linq;

namespace Officium.CommandHandlers
{
    public class CommandHandlerFactory
    {
        private readonly ICommandHandler[] commandHandlers;
        private readonly ICommandValidator[] commandValidators;

        public CommandHandlerFactory(ICommandHandler[] commandHandlers, ICommandValidator[] commandValidators)
        {
            this.commandHandlers = commandHandlers ?? Enumerable.Empty<ICommandHandler>().ToArray();
            this.commandValidators = commandValidators ?? Enumerable.Empty<ICommandValidator>().ToArray();
        }

        public ICommandHandler GetCommandHandler(ICommand command)
        {
            var commandValidator = commandValidators
                .Where(x => command != null && x.CanValidate(command))
                .ToList();
            commandValidator.Add(new NoMatchCommandValidator());
            

            var rtn = commandHandlers
                .FirstOrDefault(x => command != null && x.CanHandle(command))
                .WithDefault(new NoMatchCommandHandler());
            return rtn;
        }        
    }

    public class ValidatingCommandHandler : ICommandHandler
    {
        private readonly List<ICommandValidator> commandValidators;
        private readonly ICommandHandler commandHandler;

        public ValidatingCommandHandler(List<ICommandValidator> commandValidators , ICommandHandler commandHandler)
        {
            this.commandValidators = commandValidators;
            this.commandHandler = commandHandler;
        }

        public bool CanHandle(ICommand command) => commandHandler.CanHandle(command); 

        public void Handle(ICommand command)
        {
            commandValidators.ToList().ForEach(v =>
            {
                //todo Validate and collect results
            });

            commandHandler.Handle(command);
        }
    }
}
