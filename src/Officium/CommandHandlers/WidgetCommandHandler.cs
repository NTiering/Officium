using Officium.Commands;

namespace FunctionApp2.CommandHandlers
{
    public interface ICommandHandler       
    {
        bool CanHandle(ICommand command);
        void Handle(ICommand command);
    }

    public abstract class HttpPostCommandHandler : ICommandHandler
    {
        public bool CanHandle(ICommand command)
        {
            return command.CommandRequestType == CommandRequestType.HttpPost;
        }

        public abstract void Handle(ICommand command);
    }

    public abstract class HttpDeleteCommandHandler : ICommandHandler
    {
        public bool CanHandle(ICommand command)
        {
            return command.CommandRequestType == CommandRequestType.HttpDelete;
        }

        public abstract void Handle(ICommand command);
    }

    public abstract class HttpGetCommandHandler : ICommandHandler
    {
        public bool CanHandle(ICommand command)
        {
            return command.CommandRequestType == CommandRequestType.HttpGet;
        }

        public abstract void Handle(ICommand command);
    }

    public abstract class HttpPutCommandHandler : ICommandHandler
    {
        public bool CanHandle(ICommand command)
        {
            return command.CommandRequestType == CommandRequestType.HttpPut;
        }

        public abstract void Handle(ICommand command);
    }   
}
