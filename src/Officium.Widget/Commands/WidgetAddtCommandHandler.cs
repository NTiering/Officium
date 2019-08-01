using Officium.CommandHandlers;
using Officium.Commands;
using Officium.Widget.Data;
using Officium.Ext;
using Officium.Widget.Ext;

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
            command.AddCommandItem(context);
        }
    }

    public sealed class WidgetFindByNameCommandHandler : BaseCommandHandler<WidgetFindAllByNameCommand>
    {
        private readonly IWidgetDataContext dataContext;
        public WidgetFindByNameCommandHandler(IWidgetDataContext dataContext)
        {
            this.dataContext = dataContext;
        }
        protected override void HandleCommand(WidgetFindAllByNameCommand command, ICommandContext context)
        {
            var pageination = context.Input.ToPaginationRequest();
            var name = context.RequestPath.ValueAfter("name");
            var results = dataContext.FindByName(name, pageination);
            command.AddCommandItemToValues(context, results, pageination);
        }       
    }
}
