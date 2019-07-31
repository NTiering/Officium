namespace Officium.Commands
{
    public abstract class BaseCommand : ICommand
    {
        public string RequestPath { get; set; }
        public ICommandResponse CommandResponse { get; set; }
        public CommandRequestType CommandRequestType { get; set; }  
    }
}
