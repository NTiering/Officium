using Officium.Attributes;
using Officium.CommandHandlers;
using Officium.Commands;
using Officium.Widget.Data;

namespace Officium.Widget.Commands
{
    public sealed class WidgetRemoveCommandHandler : BaseCommandHandler<WidgetRemoveCommand>
    {
        private readonly IWidgetDataContext dataContext;

        public WidgetRemoveCommandHandler(IWidgetDataContext dataContext)
        {
            this.dataContext = dataContext;
        }
        protected override void HandleCommand(WidgetRemoveCommand command , ICommandContext context)
        {
            dataContext.Remove(command);
        }
    }
}
