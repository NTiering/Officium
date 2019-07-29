namespace Officium.Example.Commands
{
    using Officium.CommandHandlers;
    public class HelloWorldCommandHandler : BaseCommandHandler<HelloWorldCommand>
    {
        protected override void HandleCommand(HelloWorldCommand command)
        {
            command.CommandResponse.AddValue("greeting", $"Hello {command.Name}");
        }
    }
}
