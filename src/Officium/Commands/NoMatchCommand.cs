namespace Officium.Commands
{
    public sealed class NoMatchCommand : ICommand
    {        
        public ICommandResponse CommandResponse { get; set; }
        public CommandRequestType CommandRequestType { get; set; }
    }
}
