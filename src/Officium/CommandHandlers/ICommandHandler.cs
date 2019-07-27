using System;
using System.Collections.Generic;
using System.Text;
using Officium.Commands;

namespace Officium.CommandHandlers
{
    public interface ICommandHandler
    {
        bool CanHandle(ICommand command);
        void Handle(ICommand command);
    }
}
