using Officium.Commands;
using Officium.Ext;
using Officium.Widget.Commands;
using System.Collections.Generic;

namespace Officium.Widget.Ext
{
    public static class ICommandExt
    {
        public static void AddCommandItemToValues(this ICommand command, ICommandContext context)
        {
            context.CommandResponse.AddValue("Uri", $"{context.RequestPath}/{command.Id}");
            context.CommandResponse.AddValue("Widget", command);
        }
        public static void AddCommandItemToValues(this ICommand command, ICommandContext context, IEnumerable<IWidget> results, PaginationRequest pageination)
        {
            context.CommandResponse.AddValue("Results", results);
            context.CommandResponse.AddValue("Pagination", pageination);
        }
    }
}
