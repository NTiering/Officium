using Officium.Attributes;
using Officium.CommandHandlers;
using Officium.Commands;
using Officium.Ext;
using Officium.Widget.Data;

namespace Officium.Widget.Commands
{
    public sealed class WidgetGetByIdCommandHandler : BaseCommandHandler<WidgetGetByIdCommand>
    {
        private readonly IWidgetDataContext dataContext;

        public WidgetGetByIdCommandHandler(IWidgetDataContext dataContext)
        {
            this.dataContext = dataContext;
        }
        protected override void HandleCommand(WidgetGetByIdCommand command, ICommandContext context)
        {
            var id = context.RequestPath.ValueAfter("widget");
            var item = dataContext.GetById(id);
            context.CommandResponse.AddValue("widget", item);
            context.CommandResponse.AddValue("id", id);
            context.CommandResponse.AddValue("found", item != null);
        }
    }
}
