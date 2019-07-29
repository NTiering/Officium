namespace Officium.Example.Commands
{
    using Officium.Attributes;
    using Officium.Commands;

    [CommandHandlerRouting(RequestType = CommandRequestType.HttpGet, Path = ".")]
    public class HelloWorldCommand : ICommand
    {
        public string Name { get; set; }
        public CommandRequestType CommandRequestType { get; set; }
        public ICommandResponse CommandResponse { get; set; }
    }
}
