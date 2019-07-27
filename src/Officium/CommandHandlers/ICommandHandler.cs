namespace Officium.CommandHandlers
{
    using Officium.Commands;
    public interface ICommandHandler
    {
        bool CanHandle(ICommand command);
        void Handle(ICommand command);
    }
}
