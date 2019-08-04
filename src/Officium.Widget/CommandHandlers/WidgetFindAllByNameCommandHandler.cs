using Officium.CommandHandlers;
using Officium.Commands;
using Officium.Widget.Data;
using Officium.Ext;
using Officium.Widget.Ext;
using Officium.Widget.Commands;

namespace Officium.Widget.CommandHandlers
{
    public sealed class WidgetFindAllByNameCommandHandler : BaseCommandHandler<WidgetFindAllByNameCommand>
    {
        private readonly IWidgetDataContext dataContext;
        public WidgetFindAllByNameCommandHandler(IWidgetDataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        protected override bool CanHandleRequest(ICommand command, ICommandContext context)
        {
            var rtn = context.AuthResult.HasAllowedClaim("WidgetAdminUser");
            return rtn;
        }

        protected override void HandleCommand(WidgetFindAllByNameCommand command, ICommandContext context)
        {
            var pageination = context.Input.ToPaginationRequest();
            var name = context.RequestPath.ValueAfter("name");
            var results = dataContext.FindAllByName(name, pageination);
            command.AddCommandItemToValues(context, results, pageination);
        }       
    }
}
