using System;
using System.Collections.Generic;
using System.Text;
using Officium.Commands;

namespace Officium.CommandFilters
{
    public class NoMatchCommandFilter : ICommandFilter
    {
        public void AfterHandleEvent(ICommand command)
        {
           
        }

        public void BeforeHandleEvent(ICommand command)
        {
            
        }

        public bool CanFilter(ICommand command)
        {
            return false;
        }
    }
}
