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

        public bool CanHandle(ICommand command, ICommandContext context) => commandHandler.CanHandle(command, context);
        public ValidatingCommandHandler(List<ICommandValidator> commandValidators, ICommandHandler commandHandler, List<ICommandFilter> commandFilters)
        {
            this.commandValidators = commandValidators.WithDefault(new List<ICommandValidator>());
            this.commandHandler = commandHandler.WithDefault(new NoMatchCommandHandler());
            this.commandFilters = commandFilters.WithDefault(new List<ICommandFilter>());
        }

        public void Handle(ICommand command, ICommandContext context)
        {
            RunBeforeFilters(command, context);
            AddValidationResults(command, context);
            ExecuteHandler(command, context);
            RunAfterFilters(command, context);
        }

        private void RunAfterFilters(ICommand command, ICommandContext context)
        {
            commandFilters.ForEach(x => x.AfterHandleEvent(command, context));
        }

        private void RunBeforeFilters(ICommand command, ICommandContext context)
        {
            commandFilters.ForEach(x => x.BeforeHandleEvent(command, context));
        }

        private void ExecuteHandler(ICommand command, ICommandContext context)
        {
            if (context.CommandResponse.ValidationResults.Any() == false)
            {
                commandHandler.Handle(command, context);
            }
        }

        private void AddValidationResults(ICommand command, ICommandContext context)
        {
            var validationResults = new List<IValidationResult>();
            commandValidators.ToList().ForEach(v =>
            {
                validationResults.AddRange(v.Validate(command, context));
            });
            context.CommandResponse.ValidationResults = validationResults.Where(x => x != null).ToArray();
        }
    }
}
