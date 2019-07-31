using Officium.Attributes;
using Officium.CommandHandlers;
using Officium.Commands;
using Officium.Widget.Data;

namespace Officium.Widget.Commands
{
    public sealed class WidgetAddCommandHandler : BaseCommandHandler<WidgetAddCommand>
    {
        private readonly IWidgetDataContext dataContext;
        public WidgetAddCommandHandler(IWidgetDataContext dataContext)
        {
            this.dataContext = dataContext;
        }
        protected override void HandleCommand(WidgetAddCommand command, ICommandContext context)
        {
            dataContext.Add(command);
            context.CommandResponse.AddValue("Url", $"{context.RequestPath}/{command.Id}");
            context.CommandResponse.AddValue("Widget", command);
        }
    }
}
