namespace Officium.CommandHandlers
{
    using Officium.Commands;
    public interface ICommandHandlerFactory
    {
        ICommandHandler GetCommandHandler(ICommand command, ICommandContext context);
    }
}