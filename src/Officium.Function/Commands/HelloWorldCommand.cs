using AzureFromTheTrenches.Commanding.Abstractions;

namespace FunctionApp1.Commands
{
    public class HelloWorldCommand : ICommand<string>
    {
        public string Name { get; set; }
    }
}
