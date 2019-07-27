using Officium.Commands;

namespace Officium.CommandHandlers
{
    public interface ICommandHandlerFactory
    {
        ICommandHandler GetCommandHandler(ICommand command);
    }
}