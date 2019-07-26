namespace Officium.Commands
{
    public interface ICommand
    {
        CommandRequestType CommandRequestType { get; set; }
        ICommandResponse CommandResponse { get; set; }
    }
}
