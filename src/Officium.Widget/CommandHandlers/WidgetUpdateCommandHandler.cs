using Officium.Attributes;
using Officium.CommandHandlers;
using Officium.Commands;
using Officium.Widget.Commands;
using Officium.Widget.Data;

namespace Officium.Widget.CommandHandlers
{
    public sealed class WidgetUpdateCommandHandler : BaseCommandHandler<WidgetUpdateCommand>
    {
        private readonly IWidgetDataContext dataContext;
        public WidgetUpdateCommandHandler(IWidgetDataContext dataContext)
        {
            this.dataContext = dataContext;
        }
        protected override void HandleCommand(WidgetUpdateCommand command, ICommandContext context)
        {
            dataContext.Update(command);
        }
    }
}
