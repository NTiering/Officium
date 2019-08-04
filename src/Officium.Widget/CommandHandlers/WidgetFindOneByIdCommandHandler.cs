using Officium.Attributes;
using Officium.CommandHandlers;
using Officium.Commands;
using Officium.Ext;
using Officium.Widget.Commands;
using Officium.Widget.Data;

namespace Officium.Widget.CommandHandlers
{
    public sealed class WidgetFindOneByIdCommandHandler : BaseCommandHandler<WidgetFindOneByIdCommand>
    {
        private readonly IWidgetDataContext dataContext;

        public WidgetFindOneByIdCommandHandler(IWidgetDataContext dataContext)
        {
            this.dataContext = dataContext;
        }
        protected override void HandleCommand(WidgetFindOneByIdCommand command, ICommandContext context)
        {
            var id = context.RequestPath.ValueAfter("widget");
            var item = dataContext.FindOneById(id);
            context.CommandResponse.AddValue("widget", item);
            context.CommandResponse.AddValue("id", id);
            context.CommandResponse.AddValue("found", item != null);
        }
    }
}
