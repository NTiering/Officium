using Officium.CommandHandlers;
using Officium.Commands;
using Officium.Widget.Data;
using Officium.Ext;
using Officium.Widget.Commands;

namespace Officium.Widget.CommandHandlers
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
            command.AddCommandItem(context);
        }
    }
}
