namespace Officium.Commands
{
    public abstract class BaseCommand : ICommand
    {
        public ICommandResponse CommandResponse { get; set; }
    }
}
