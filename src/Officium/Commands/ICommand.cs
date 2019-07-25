namespace Officium.Commands
{
    public interface ICommand
    {
        ICommandResponse CommandResponse { get; set; }
    }
}
