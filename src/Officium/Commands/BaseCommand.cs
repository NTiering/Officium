namespace Officium.Commands
{
    public abstract class BaseCommand : ICommand
    {
        public ICommandResponse CommandResponse { get; set; }
        public CommandRequestType CommandRequestType { get; set; }
    }
}
