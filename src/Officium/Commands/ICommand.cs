namespace Officium.Commands
{
    public interface ICommand
    {
        ICommandRequest CommandRequest { get; set; }
        ICommandResponse CommandResponse { get; set; }
    }
}
