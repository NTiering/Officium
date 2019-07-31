using System;
using System.Collections.Generic;
using System.Text;
using Officium.Commands;

namespace Officium.CommandFilters
{
    public class NoMatchCommandFilter : ICommandFilter
    {
        public void AfterHandleEvent(ICommand command, ICommandContext context)
        {
           
        }

        public void BeforeHandleEvent(ICommand command, ICommandContext context)
        {
            
        }

        public bool CanFilter(ICommand command, ICommandContext context)
        {
            return false;
        }
    }
}
