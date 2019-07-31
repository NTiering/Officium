using System;
using System.Collections.Generic;
using System.Text;
using Officium.Commands;

namespace Officium.CommandFilters
{
    public interface ICommandFilter
    {
        bool CanFilter(ICommand command, ICommandContext context);
        void BeforeHandleEvent(ICommand command, ICommandContext context);
        void AfterHandleEvent(ICommand command, ICommandContext context);
    }
}
