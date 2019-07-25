using AzureFromTheTrenches.Commanding.Abstractions;
using System.Threading.Tasks;

namespace FunctionApp1.Commands
{
    internal class HelloWorldCommandHandler : ICommandHandler<HelloWorldCommand, string>
    {
        public Task<string> ExecuteAsync(HelloWorldCommand command, string previousResult)
        {
            if (string.IsNullOrWhiteSpace(command.Name))
            {
                return Task.FromResult("Hello stranger");
            }
            return Task.FromResult($"Hello {command.Name}");
        }
    }
}
