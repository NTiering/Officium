using Officium.Commands;
using Officium.CommandValidators;
using Officium.Ext;
using System.Collections.Generic;
using System.Linq;

namespace Officium.CommandHandlers
{
    public class CommandHandlerFactory : ICommandHandlerFactory
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
            var validationResults = new List<IValidationResult>();
            commandValidators.ToList().ForEach(v =>
            {
                validationResults.AddRange(v.Validate(command));
            });
            command.CommandResponse.ValidationResults = validationResults.Where(x => x != null).ToArray();

            if (command.CommandResponse.ValidationResults.Any() == false)
            {
                commandHandler.Handle(command);
            }            
        }
    }
}
