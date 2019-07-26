using FunctionApp2.CommandHandlers;
using Officium.Commands;

namespace Officium.CommandHandlers
{
    public class NoMatchCommandHandler : ICommandHandler
    {
        public bool CanHandle(ICommand command)
        {
            return false;
        }

        public void Handle(ICommand command)
        {
            
        }
    }
}