namespace Officium.CommandHandlers
{
    using Officium.CommandFilters;
    using Officium.Commands;
    using Officium.CommandValidators;
    using Officium.Ext;
    using System.Collections.Generic;
    using System.Linq;
    public class CommandHandlerFactory : ICommandHandlerFactory
    {
        private readonly ICommandHandler[] commandHandlers;
        private readonly ICommandValidator[] commandValidators;
        private readonly ICommandFilter[] commandFilters;

        public CommandHandlerFactory(ICommandHandler[] commandHandlers, ICommandValidator[] commandValidators, ICommandFilter[] commandFilters)
        {
            this.commandHandlers = commandHandlers.WithDefault(new ICommandHandler[0]);
            this.commandValidators = commandValidators.WithDefault(new ICommandValidator[0]);
            this.commandFilters = commandFilters.WithDefault(new ICommandFilter[0]);
        }

        public ICommandHandler GetCommandHandler(ICommand command)
        {
            var validators = commandValidators
                .Where(x => command != null && x.CanValidate(command))
                .ToList();
            validators.Add(new NoMatchCommandValidator());

            var filters = commandFilters
                .Where(x => x.CanFilter(command))
                .ToList();
            filters.Add(new NoMatchCommandFilter());

            var handler = commandHandlers
                .FirstOrDefault(x => command != null && x.CanHandle(command))
                .WithDefault(new NoMatchCommandHandler());

            var rtn = new ValidatingCommandHandler(validators, handler, filters);
            return rtn;
        }
    }    
}
