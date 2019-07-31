namespace Officium.Commands
{
    public sealed class NoMatchCommand : ICommand
    {
        public string RequestPath { get; set; }
        public ICommandResponse CommandResponse { get; set; }
        public CommandRequestType CommandRequestType { get; set; }
    }
}
