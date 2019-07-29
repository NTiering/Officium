using System;
using System.Collections.Generic;
using System.Text;
using Officium.Commands;

namespace Officium.CommandFilters
{
    public interface ICommandFilter
    {
        bool CanFilter(ICommand command);
        void BeforeHandleEvent(ICommand command);
        void AfterHandleEvent(ICommand command);
    }
}
