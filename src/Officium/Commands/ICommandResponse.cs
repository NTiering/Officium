namespace Officium.Commands
{
    public interface ICommandResponse
    {
        void AddValue(string name, bool allowOverwrite = false);
        bool HasValue(string name);
    }
}
