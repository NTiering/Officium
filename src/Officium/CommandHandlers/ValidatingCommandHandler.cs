namespace Officium.CommandHandlers
{
    using Officium.CommandFilters;
    using Officium.Commands;
    using Officium.CommandValidators;
    using Officium.Ext;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    public class ValidatingCommandHandler : ICommandHandler
    {
        private readonly List<ICommandValidator> commandValidators;
        private readonly ICommandHandler commandHandler;
        private readonly List<ICommandFilter> commandFilters;

        public bool CanHandle(ICommand command) => commandHandler.CanHandle(command);
        public ValidatingCommandHandler(List<ICommandValidator> commandValidators, ICommandHandler commandHandler, List<ICommandFilter> commandFilters)
        {
            this.commandValidators = commandValidators.WithDefault(new List<ICommandValidator>());
            this.commandHandler = commandHandler.WithDefault(new NoMatchCommandHandler());
            this.commandFilters = commandFilters.WithDefault(new List<ICommandFilter>());
        }

        public void Handle(ICommand command)
        {
            RunBeforeFilters(command);
            AddValidationResults(command);
            ExecuteHandler(command);
            RunAfterFilters(command);
        }

        private void RunAfterFilters(ICommand command)
        {
            commandFilters.ForEach(x => x.AfterHandleEvent(command));
        }

        private void RunBeforeFilters(ICommand command)
        {
            commandFilters.ForEach(x => x.BeforeHandleEvent(command));
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
