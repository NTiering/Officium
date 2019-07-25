namespace Officium.Commands
{
    public abstract class BaseCommand : ICommand
    {
        public ICommandRequest CommandRequest { get; set; }
        public ICommandResponse CommandResponse { get; set; }
    }
}
