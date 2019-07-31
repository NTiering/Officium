namespace Officium.CommandHandlers
{
    using Officium.Commands;
    public class NoMatchCommandHandler : ICommandHandler
    {
        public bool CanHandle(ICommand command, ICommandContext context)
        {
            return false;
        }

        public void Handle(ICommand command, ICommandContext context)
        {
            
        }
    }
}