namespace Officium.CommandHandlers
{
    using Officium.Commands;
    public interface ICommandHandler
    {
        bool CanHandle(ICommand command, ICommandContext context);
        void Handle(ICommand command, ICommandContext context);
    }
}
