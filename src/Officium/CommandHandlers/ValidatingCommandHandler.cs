namespace Officium.CommandHandlers
{
    using Officium.Commands;
    using Officium.CommandValidators;
    using Officium.Ext;
    using System.Collections.Generic;
    using System.Linq;
    public class ValidatingCommandHandler : ICommandHandler
    {
        private readonly List<ICommandValidator> commandValidators;
        private readonly ICommandHandler commandHandler;
        public bool CanHandle(ICommand command) => commandHandler.CanHandle(command);

        public ValidatingCommandHandler(List<ICommandValidator> commandValidators, ICommandHandler commandHandler)
        {
            this.commandValidators = commandValidators.WithDefault(new List<ICommandValidator>());
            this.commandHandler = commandHandler.WithDefault(new NoMatchCommandHandler());
        }

        public void Handle(ICommand command)
        {
            AddValidationResults(command);
            ExecuteHandler(command);
        }

        private void ExecuteHandler(ICommand command)
        {
            if (command.CommandResponse.ValidationResults.Any() == false)
            {
                commandHandler.Handle(command);
            }
        }

        private void AddValidationResults(ICommand command)
        {
            var validationResults = new List<IValidationResult>();
            commandValidators.ToList().ForEach(v =>
            {
                validationResults.AddRange(v.Validate(command));
            });
            command.CommandResponse.ValidationResults = validationResults.Where(x => x != null).ToArray();
        }
    }
}
