using Officium.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Officium.Ext
{
    public static class ICommandExt
    {
        public static void AddCommandItem(this ICommand command, ICommandContext context)
        {
            context.CommandResponse.AddValue("Uri", $"{context.RequestPath}/{command.Id}");
            context.CommandResponse.AddValue("Widget", command);
        }       
    }
}
